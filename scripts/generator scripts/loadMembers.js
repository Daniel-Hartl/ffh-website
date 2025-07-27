document.addEventListener('DOMContentLoaded', async () => {
    root = document.getElementById('board-container');
    if (!root) {
        root = document.getElementById('crew-container');
        if (root) {
            path = 'data/crew.json'
        }
    }
    else {
        path = 'data/board.json'
    }

    if (root) {
        members = await fetch(path)
            .then(response => response.json())
            
        Array.from(root.children).forEach(group => {
            if (group.hasAttribute("topic")) {
                readJson(group, group.getAttribute("topic"), members);
            }
        });
    }
});

function readJson(group, topic, members) {
    try {
        counter = 0;
        members.forEach(member => {
            if (topic == member.Id) {
                console.log(member)
                group.children[1].appendChild(generateNewMemberFrame(member));
                counter++;
            }
        });
        if (counter === 0) {
            group.className += "hidden";
        }
    }
    catch (err) {
        console.error('Fehler beim Einlesen der Datei:', err.message);
    }
}

function generateNewMemberFrame(member) {
    // Container-DIV
    const container = document.createElement('div');
    container.id = member.Id;
    container.className = 'board-member';

    // Bild
    const img = document.createElement('img');
    if (member.Bild) {
        img.src = 'data/images/' + member.Bild
    }
    else {
        img.src = 'images/noImage.png'; // Falls kein Bild vorhanden
    }
    img.alt = member.Name;

    // Name
    const name = document.createElement('div');
    name.className = 'name-tag';
    name.textContent = member.Name;

    // Funktion
    const position = document.createElement('div');
    position.className = 'function-description';
    Array.from(member.Funktion).forEach(funktion => {
        pos = document.createElement('p');
        pos.innerText = funktion;
        position.appendChild(pos);
    });


    // Alles zusammensetzen
    container.appendChild(img);
    container.appendChild(name);
    container.appendChild(position);


    // Kommentar
    if (member.Kommentar) {
        const comment = document.createElement('div');
        comment.className = 'board-member-comment';
        comment.innerText = member.Kommentar;
        container.appendChild(comment);
    }

    return container;
}