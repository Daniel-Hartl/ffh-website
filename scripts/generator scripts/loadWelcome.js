document.addEventListener('DOMContentLoaded', async () => {
    const div = document.getElementById('welcome');
    const statistics = await fetch('data/statistics.json').then(response => response.json());
    let commander = Array.from(await fetch('data/crew.json').then(response => response.json()));
    commander = commander.find(element => element.Id === 'kommandant' && element.Funktion.startsWith("1. Kommandant"));

    let chairman = Array.from(await fetch('data/board.json').then(response => response.json()))
    chairman = chairman.find(element => element.Id === 'vorsitz' && element.Funktion.startsWith("1. Vorsitzende"));

    generateWelcome(div, statistics, commander, chairman)
});



function generateWelcome(div, statistics, commander, chairman) {
    if (commander.Bild !== '')
        div.appendChild(createPersonDiv(commander));

    const textDiv = document.createElement('div');
    textDiv.className = "text-div";
    const h1 = document.createElement('h1');
    h1.innerText = "Herzlich Wilkommen bei der FF Heitzenhofen";
    const h2 = document.createElement('h2');
    h2.innerText = `Wir sind eine Feuerwehr der Gemeinde Duggendorf mit derzeit ${statistics.Mitglieder} Mitgliedern und einer aktiven Mannschaft aus ${statistics.Aktive} Einsatzkräften. Im Jahr ${new Date().getFullYear()} rückten wir zu ${statistics.Einsätze} Einsätzen aus.`;

    textDiv.appendChild(h1);
    textDiv.appendChild(h2);
    div.appendChild(textDiv);

    if (chairman.Bild !== '')
        div.appendChild(createPersonDiv(chairman));
}

function createPersonDiv(person) {
    const personDiv = document.createElement('div');
    personDiv.className = 'person-div';

    const captionDiv = document.createElement('div');

    const name = document.createElement('h3');
    name.innerText = person.Name;
    captionDiv.appendChild(name);

    const title = document.createElement('p');
    title.innerText = person.Funktion;
    captionDiv.appendChild(title);

    const image = document.createElement('img');
    image.src = 'data/images/' + person.Bild;

    personDiv.appendChild(image);
    personDiv.appendChild(captionDiv);

    return personDiv;
}