function generateEvent(event) {
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
document.addEventListener('DOMContentLoaded', async () => {
    const tabs = document.getElementsByClassName('tabcontent');
    
    const structure = await fetch('data/galery.json').then(response => response.json());
    console.log(structure)

    Array.from(tabs).forEach(tab => {
        folder = Array.from(structure).find(element => element.Folder === tab.id)
        if (folder){
            folder.Content.sort(compareDate).forEach(event => generateEvent(event))
        }
    });
});



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