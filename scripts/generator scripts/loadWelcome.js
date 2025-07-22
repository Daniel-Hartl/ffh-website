document.addEventListener('DOMContentLoaded', async () => {
    const div = document.getElementById('welcome');
    const statistics = await fetch('data/statistics.json').then(response => response.json());

    generateWelcome(div, statistics)
});



function generateWelcome(div, statistics) {

    const h2 = document.createElement('h2');
    h2.innerText = `Wir sind eine Feuerwehr der Gemeinde Duggendorf mit derzeit ${statistics.Mitglieder} Mitgliedern und einer aktiven Mannschaft aus ${statistics.Aktive} Einsatzkräften. Im Jahr ${new Date().getFullYear()} rückten wir zu ${statistics.Einsätze} Einsätzen aus.`;
    div.appendChild(h2);
}