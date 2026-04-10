namespace FFH_Website_Manager.Popups;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using FFH_Website_Manager.Classes.Enums;
using FFH_Website_Manager.Classes.Model;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

/// <summary>
/// Interaction logic for EditArticle.xaml
/// </summary>
public partial class EditArticle : Window, IDisposable, INotifyPropertyChanged
{
    private string uploadImagePath;
    private bool imageHasChanged;
    private bool oldImageCorrupted;
    private bool isColorizing = false;
    public event PropertyChangedEventHandler? PropertyChanged;

    internal EditArticle(Article article)
    {
        this.DataContext = article;
        this.Article = article;
        if (this.Article.HasImange)
        {
            try
            {
                using MemoryStream ms = new();
                App.DataProvider.DownloadFile(GetSftpUrl(article.Bild), ms);
                ms.Position = 0;
                Bmp = BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                Bmp.Freeze();
            }
            catch
            {
                oldImageCorrupted = true;
                MessageBox.Show(
                    "Das Bild muss neu hochgeladen werden.",
                    "Fehler beim Laden des Bildes",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        InitializeComponent();
        
        // Load article content into RichTextBox
        if (!string.IsNullOrEmpty(article.Inhalt))
        {
            LoadContentIntoRichTextBox(article.Inhalt);
        }
        
        // Attach event handlers
        articleBox.TextChanged += ArticleBox_TextChanged;
        
        this.OnPropChanged(nameof(Bmp));
    }

    public BitmapFrame Bmp { get; set; }

    internal Article Article { get; set; }

    public bool SaveData { get; set; }

    private static string GetSftpUrl(string fileName)
        => App.DataProvider.BuildPath(Appsettings.Instance.RootDirectory, PathFragmentCollection.ArticlesImageDirectory, fileName);

    private void AddPicture(object sender, RoutedEventArgs e)
    {
        if (FileDialogTemplates.SelectSingleImage(out string path))
        {
            // keep path for later upload
            uploadImagePath = path;

            // Load image into memory with CacheOption OnLoad and freeze it
            // so the original file is not locked by the application.
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                ms.Position = 0;
                Bmp = BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                Bmp.Freeze();
            }

            this.imageHasChanged = true;
            this.OnPropChanged(nameof(Bmp));
        }
    }

    private void RemovePicture(object sender, RoutedEventArgs e)
    {
        Bmp = null;
        uploadImagePath = string.Empty;
        this.imageHasChanged = true;
        this.OnPropChanged(nameof(Bmp));
    }

    private void Save(object sender, RoutedEventArgs e)
    {
        this.SaveData = true;
        if (this.imageHasChanged && !this.oldImageCorrupted && !string.IsNullOrEmpty(this.Article.Bild))
            App.DataProvider.DeleteFile(GetSftpUrl(this.Article.Bild));

        if (Bmp == null)
        {
            this.Article.Bild = string.Empty;
        }
        else if (!string.IsNullOrEmpty(this.uploadImagePath))
        {
            this.Article.Bild = this.Article.Titel + Path.GetExtension(this.uploadImagePath);
            App.DataProvider.UploadFileFromPath(this.uploadImagePath, GetSftpUrl(this.Article.Bild));
        }

        // Save RichTextBox content back to Article.Inhalt
        this.Article.Inhalt = SerializeRichTextBoxContent();

        this.Close();
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void Dispose()
    {
        this.Close();
    }
    private void OnPropChanged([CallerMemberName] string? src = null) => this.PropertyChanged?.Invoke(this, new(src));

    private void OpenImage(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ClickCount >= 2)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "websiteManager", "imgPreview", Guid.NewGuid().ToString() + ".jpg");
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath));

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(Bmp);

            using (var fileStream = new FileStream(tempPath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = tempPath,
                UseShellExecute = true
            });
        }
    }

    private void ButtonHeadlineClick(object sender, RoutedEventArgs e)
    {
        ApplyTextDecoration(FontDecoration.Headline);
    }

    private void ButtonBoldClick(object sender, RoutedEventArgs e)
    {
        ApplyTextDecoration(FontDecoration.Bold);
    }

    private void ButtonItalicClick(object sender, RoutedEventArgs e)
    {
        ApplyTextDecoration(FontDecoration.Italic);
    }

    private void ApplyTextDecoration(FontDecoration decoration)
    {
        string tag = decoration switch
        {
            FontDecoration.Headline => "h2",
            FontDecoration.Bold => "b",
            FontDecoration.Italic => "i",
            _ => null
        };

        if (tag is null) return;

        // Get current selection
        TextRange selection = this.articleBox.Selection;
        string selectedText = selection.Text;

        // If no text is selected, insert empty tags at cursor
        if (string.IsNullOrEmpty(selectedText))
        {
            // Insert tags at cursor position
            TextPointer insertPoint = selection.Start;
            Paragraph paragraph = insertPoint.Paragraph;
            
            if (paragraph == null) return;

            // Check if cursor is inside an existing tag
            if (IsInsideTagAtPointer(insertPoint, tag, out TextPointer tagStart, out TextPointer tagEnd))
            {
                // Remove the tag
                RemoveTagAtPointers(tagStart, tagEnd);
            }
            else
            {
                // Insert empty tags at cursor
                Run openTag = new Run($"<{tag}>") { Foreground = Brushes.LightGray };
                Run closeTag = new Run($"</{tag}>") { Foreground = Brushes.LightGray };
                
                // Find the inline element at the insertion point
                Inline insertBefore = null;
                foreach (Inline inline in paragraph.Inlines)
                {
                    if (inline.ContentStart.CompareTo(insertPoint) >= 0)
                    {
                        insertBefore = inline;
                        break;
                    }
                }
                
                if (insertBefore != null)
                {
                    paragraph.Inlines.InsertBefore(insertBefore, openTag);
                    paragraph.Inlines.InsertAfter(openTag, closeTag);
                }
                else
                {
                    paragraph.Inlines.Add(openTag);
                    paragraph.Inlines.Add(closeTag);
                }
            }
        }
        else
        {
            // Text is selected - wrap it with tags or remove existing tags
            string contentBefore = new TextRange(this.articleBox.Document.ContentStart, selection.Start).Text;
            string contentAfter = new TextRange(selection.End, this.articleBox.Document.ContentEnd).Text;
            
            // Remove any tags from selected text first
            selectedText = RemoveAllTags(selectedText);
            
            // Check if we should wrap with new tag
            string newContent = contentBefore + $"<{tag}>{selectedText}</{tag}>" + contentAfter;
            
            LoadContentIntoRichTextBox(newContent);
        }

        ColorizeHtmlTags();
    }

    private bool IsInsideTagAtPointer(TextPointer pointer, string targetTag, out TextPointer tagStart, out TextPointer tagEnd)
    {
        tagStart = null;
        tagEnd = null;

        try
        {
            string content = GetRichTextBoxContent();
            int pointerOffset = GetOffsetFromPointer(pointer);
            
            string openTag = $"<{targetTag}>";
            string closeTag = $"</{targetTag}>";
            
            // Find the most recent opening tag before this position
            int lastOpenIndex = content.LastIndexOf(openTag, Math.Max(0, pointerOffset - 1));
            if (lastOpenIndex == -1) return false;
            
            int correspondingCloseIndex = content.IndexOf(closeTag, lastOpenIndex);
            if (correspondingCloseIndex == -1) return false;
            
            if (lastOpenIndex <= pointerOffset && pointerOffset <= correspondingCloseIndex + closeTag.Length)
            {
                tagStart = GetPointerFromOffset(lastOpenIndex);
                tagEnd = GetPointerFromOffset(correspondingCloseIndex + closeTag.Length);
                return true;
            }
        }
        catch { }

        return false;
    }

    private void RemoveTagAtPointers(TextPointer tagStart, TextPointer tagEnd)
    {
        if (tagStart == null || tagEnd == null) return;

        try
        {
            TextRange tagRange = new(tagStart, tagEnd);
            string tagContent = tagRange.Text;
            
            // Remove the opening and closing tags while preserving inner content
            string innerContent = RemoveAllTags(tagContent);
            
            tagRange.Text = innerContent;
        }
        catch { }
    }

    private string RemoveAllTags(string text)
    {
        string[] tagsToRemove = { "h2", "b", "i" };
        foreach (var tag in tagsToRemove)
        {
            string pattern = $"</?{tag}>";
            text = Regex.Replace(text, pattern, "");
        }
        return text;
    }

    private int GetOffsetFromPointer(TextPointer pointer)
    {
        int offset = 0;
        TextPointer current = this.articleBox.Document.ContentStart;

        while (current.CompareTo(pointer) < 0)
        {
            TextPointerContext context = current.GetPointerContext(LogicalDirection.Forward);
            if (context == TextPointerContext.Text)
            {
                offset += current.GetTextRunLength(LogicalDirection.Forward);
            }
            current = current.GetNextContextPosition(LogicalDirection.Forward);
        }

        return offset;
    }

    private TextPointer GetPointerFromOffset(int offset)
    {
        TextPointer current = this.articleBox.Document.ContentStart;
        int currentOffset = 0;

        while (currentOffset < offset && current != null)
        {
            TextPointerContext context = current.GetPointerContext(LogicalDirection.Forward);
            if (context == TextPointerContext.Text)
            {
                int runLength = current.GetTextRunLength(LogicalDirection.Forward);
                if (currentOffset + runLength >= offset)
                {
                    return current.GetPositionAtOffset(offset - currentOffset);
                }
                currentOffset += runLength;
            }
            current = current.GetNextContextPosition(LogicalDirection.Forward);
        }

        return current ?? this.articleBox.Document.ContentEnd;
    }

    private void LoadContentIntoRichTextBox(string htmlContent)
    {
        // Detach TextChanged event to prevent recursion during document modification
        articleBox.TextChanged -= ArticleBox_TextChanged;
        
        try
        {
            this.articleBox.Document.Blocks.Clear();
            
            if (string.IsNullOrEmpty(htmlContent))
            {
                this.articleBox.Document.Blocks.Add(new Paragraph());
                return;
            }

            // Parse HTML content and create paragraphs/runs
            Paragraph paragraph = new();
            int currentIndex = 0;

            while (currentIndex < htmlContent.Length)
            {
                int tagStart = htmlContent.IndexOf('<', currentIndex);
                
                if (tagStart == -1)
                {
                    // No more tags, add remaining text
                    if (currentIndex < htmlContent.Length)
                    {
                        string remainingText = htmlContent.Substring(currentIndex);
                        Run run = new(remainingText);
                        paragraph.Inlines.Add(run);
                    }
                    break;
                }

                // Add text before tag
                if (tagStart > currentIndex)
                {
                    string textBefore = htmlContent.Substring(currentIndex, tagStart - currentIndex);
                    Run textRun = new(textBefore);
                    paragraph.Inlines.Add(textRun);
                }

                // Extract tag
                int tagEnd = htmlContent.IndexOf('>', tagStart);
                if (tagEnd == -1) break;

                string tag = htmlContent.Substring(tagStart, tagEnd - tagStart + 1);
                
                // Add tag as a run with light gray color
                Run tagRun = new(tag) { Foreground = Brushes.LightGray };
                paragraph.Inlines.Add(tagRun);

                currentIndex = tagEnd + 1;
            }

            if (paragraph.Inlines.Count == 0)
            {
                paragraph.Inlines.Add(new Run());
            }

            this.articleBox.Document.Blocks.Add(paragraph);
            ColorizeHtmlTags();
        }
        finally
        {
            // Reattach TextChanged event
            articleBox.TextChanged += ArticleBox_TextChanged;
        }
    }

    private void ColorizeHtmlTags()
    {
        // Prevent recursive calls to avoid stack overflow
        if (isColorizing) return;
        
        try
        {
            isColorizing = true;
            
            TextRange documentRange = new(this.articleBox.Document.ContentStart, this.articleBox.Document.ContentEnd);
            string content = documentRange.Text;

            // First, reset all text to black
            documentRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);

            // Pattern to find HTML tags
            string pattern = @"</?(?:h2|b|i)(?:\s[^>]*)?>|</(?:h2|b|i)>";
            MatchCollection matches = Regex.Matches(content, pattern, RegexOptions.IgnoreCase);

            // Apply light gray to each tag
            foreach (Match match in matches)
            {
                try
                {
                    TextPointer startPointer = GetPointerFromOffset(match.Index);
                    TextPointer endPointer = GetPointerFromOffset(match.Index + match.Length);

                    if (startPointer != null && endPointer != null)
                    {
                        TextRange range = new(startPointer, endPointer);
                        range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.LightGray);
                    }
                }
                catch
                {
                    // Ignore errors in formatting
                }
            }
        }
        finally
        {
            isColorizing = false;
        }
    }

    private string GetRichTextBoxContent()
    {
        TextRange range = new(this.articleBox.Document.ContentStart, this.articleBox.Document.ContentEnd);
        return range.Text.TrimEnd('\r', '\n');
    }

    private string SerializeRichTextBoxContent()
    {
        StringBuilder sb = new();
        foreach (Block block in this.articleBox.Document.Blocks)
        {
            if (block is Paragraph paragraph)
            {
                foreach (Inline inline in paragraph.Inlines)
                {
                    if (inline is Run run)
                    {
                        sb.Append(run.Text);
                    }
                }
            }
        }
        return sb.ToString();
    }

    private void ArticleBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Colorize tags after any text change
        ColorizeHtmlTags();
    }

}
