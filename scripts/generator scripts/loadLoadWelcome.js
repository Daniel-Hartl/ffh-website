document.addEventListener('DOMContentLoaded', async () => {
    const div = document.getElementsByClassName('welcome');
    
    const statistics = await fetch('data/gallery.json').then(response => response.json());
    const commander = await Array.from(fetch('data/crew.json').then(resoponse => response))
    .find(element => element.Id === 'kommandant' && element.Funktion.startsWith("1. Kommandant"));

    const chairman = await Array.from(fetch('data/crew.json').then(resoponse => response))
    .find(element => element.Id === 'vorsitz' && element.Funktion.startsWith("1. Vorsitzende"));

    generateWelcome(div, statistics, commander, chairman)
});



function generateWelcome(div, statistics, commander, chairman) {    
    div.appendChild(createPersonDiv(commander));


}

function createPersonDiv(div, person){
    const commanderDiv = Document.createElement('div');
    commanderDiv.className = 'person-div';



    return commanderDiv;
}