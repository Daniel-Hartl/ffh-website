function generateEvent(event, tab) {
    // container
    console.log(event)
    const container = document.createElement('div');
    container.className = 'event-container';

    const body = document.createElement('div');
    body.className = 'collapsible-content event-body';

    const header = document.createElement('button');
    header.className = 'collapsible event-header large';
    header.innerText = event.Folder;
    header.addEventListener('click', () => toggleCollapsed(header, body));


    Array.from(event.Content).forEach(picture => 
    {
        const imagePath = `data/images/gallery/${tab.id}/${event.Folder}/${picture}`;

        const border =  document.createElement('div');
        border.className = 'picture-div';

        const content = document.createElement('img');
        content.src = imagePath;
        content.className = 'picture';
        content.addEventListener('click', () => openDisplay(imagePath));

        border.appendChild(content);
        body.appendChild(border)
    });

    container.appendChild(header);
    container.appendChild(body);
    return container;
}

// Beispiel: JSON-Objekt

// Element erzeugen und zur Seite hinzufügen
document.addEventListener('DOMContentLoaded', async () => {
    const tabs = document.getElementsByClassName('tabcontent');
    
    const structure = await fetch('data/gallery.json').then(response => response.json());
    console.log(structure)

    Array.from(tabs).forEach(tab => {
        folder = Array.from(structure).find(element => element.Folder === tab.id)
        if (folder){
            folder.Content.sort(compareDate).forEach(
                event => tab.appendChild(generateEvent(event, tab)))
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