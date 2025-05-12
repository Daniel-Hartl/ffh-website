function generateNewArticleFrame(article) {
    // Container-DIV
    const container = document.createElement('div');
    container.type = 'button';
    container.className = 'article';
    container.addEventListener('click', () => loadArticle(article));

    // Bild
    const img = document.createElement('img');
    if (article.Bild) {
        img.src = 'data/images/articles/' + article.Bild
    }
    else {
        img.src = 'header/heitzenhofen.png'; // Falls kein Bild vorhanden
    }
    img.alt = article.Title;
    img.className = 'article-img'

    // Caption
    const date = document.createElement('td');
    date.className = 'tcR';
    date.innerText = article.Datum;

    const name = document.createElement('td');
    name.className = 'tcF';
    name.innerHTML = `<b>${article.Titel}</b>`;

    const row = document.createElement('tr');
    row.appendChild(name);
    row.appendChild(date);
    const table = document.createElement('table');
    table.className = 'subtitle';
    table.appendChild(row);

    // Alles zusammensetzen
    container.appendChild(img);
    container.appendChild(table);

    return container;
}

// Beispiel: JSON-Objekt

// Element erzeugen und zur Seite hinzufügen
document.addEventListener('DOMContentLoaded', () => {
    root = document.getElementById('article-list');

    if (root) {
        readJson(root);
    }
});

function readJson(root) {
    try {
        fetch('data/articles.json')
            .then(response => response.json())
            .then(articles => articles.sort(compareDate))
            .then(arts => arts.forEach(article => {

                root.appendChild(generateNewArticleFrame(article));
            }));
    }
    catch (err) {
        console.error('Fehler beim Einlesen der Datei:', err.message);
    }
}

function compareDate(a, b) {
    aArr = String(a.Datum).split(",");
    bArr = String(b.Datum).split(",");
    if (Number(a[2]) < Number(b[2]))
        return 1;
    else if (Number(a[2]) > Number(b[2]))
        return -1;
    else if (Number(a[1]) < Number(b[1]))
        return 1;
    else if (Number(a[1]) > Number(b[1]))
        return -1;
    else if (Number(a[0]) < Number(b[0]))
        return 1;
    else if (Number(a[0]) > Number(b[0]))
        return -1;
    else
        return 0;
}

function loadArticle(article) {
    list = document.getElementById('list').hidden = true;

    articleDiv = document.getElementById('article-display');
    articleDiv.innerHTML = '';

    const date = document.createElement('p');
    date.className = 'right-aligned';
    date.innerText = article.Datum;
    articleDiv.appendChild(date);

    const header = document.createElement('h1');
    header.innerText = article.Titel;
    articleDiv.appendChild(header);

    if (article.Autor) {
        const author = document.createElement('p');
        author.innerText = 'Autor: ' + article.Autor;
        author.className = 'footnote';
        articleDiv.appendChild(author);
    }

    if (article.Bild) {
        const imgDiv = document.createElement('div');
        imgDiv.className = 'image-full'

        const image = document.createElement('img');
        image.src = 'data/images/articles/' + article.Bild
        image.alt = article.Title;
        imgDiv.appendChild(image);

        if (article.Bildquelle) {
            const imgSrc = document.createElement('p');
            imgSrc.innerText = 'Quelle: ' + article.Bildquelle;
            imgSrc.className = 'footnote';
            imgDiv.appendChild(imgSrc);
        }

        articleDiv.appendChild(imgDiv);
    }

    const content = document.createElement('div');
    content.innerHTML = article.Inhalt;
    content.className = 'content';
    articleDiv.appendChild(content);

    articleDiv = document.getElementById('article').hidden = false;
}