function generateNewMemberFrame(mitglied) {
    // Container-DIV
    const container = document.createElement('div');
    container.id = mitglied.Id;
    container.className = 'board-member';

    // Bild
    const img = document.createElement('img');
    img.src = mitglied.Bild || 'images/noImage.png'; // Falls kein Bild vorhanden
    img.alt = mitglied.Name;

    // Name
    const name = document.createElement('div');
    name.className = 'name-tag';
    name.textContent = mitglied.Name;

    // Funktion
    const position = document.createElement('div');
    position.className = 'function-description';
    position.textContent = mitglied.Funktion;

    // Alles zusammensetzen
    container.appendChild(img);
    container.appendChild(name);
    container.appendChild(position);

    return container;
}

// Beispiel: JSON-Objekt

// Element erzeugen und zur Seite hinzufügen
document.addEventListener('DOMContentLoaded', () => {
    root = document.getElementById('board-container');
    if (!root) {
        root = document.getElementById('crew-container');
        if (root){
            path = 'data/crew.json'
        }
    }
    else {
        path = 'data/board.json'
    }

    if (root) {
        Array.from(root.children).forEach(group => {
            if(group.hasAttribute("topic")){
                readJson(group, group.getAttribute("topic"), path);
            }
        });
    }
});

function readJson(group, topic, path){
    try {
        fetch(path)
        .then(response => response.json())
        .then(mitglieder => {
            mitglieder.forEach(member => {
                if (topic == member.Id){
                    group.appendChild(generateNewMemberFrame(member));
                }
            }); 
        });
    }
    catch (err) {
        console.error('Fehler beim Einlesen der Datei:', err.message);
    }
}