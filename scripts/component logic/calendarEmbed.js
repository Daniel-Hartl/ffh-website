function loadStreetView() {
    maps = document.createElement("iframe");
    maps.src = "https://calendar.google.com/calendar/embed?src=ebe12d2c011447883173f987a7608c5ec24f4511ad317b3bd27a857e57acb8d4%40group.calendar.google.com&ctz=Europe%2FBerlin";
    maps.height = "100%";
    maps.width = "100%";
    document.getElementById("placeholder").replaceWith(maps);
}

document.addEventListener('DOMContentLoaded', async () => {
    root = document.getElementById('appointments');
    if (root) {
        const res = await getAppointments();
        res.forEach(app => root.appendChild(generateAppointment(app)));
    }
});

function generateAppointment(app) {
    const tr = document.createElement('tr');

    const date = document.createElement('td');
    date.innerText = app.Zeit+" Uhr";
    date.className = 'date';

    const desc = document.createElement('td');
    desc.innerText = app.Titel;
    desc.className = 'description';

    const location = document.createElement('td');
    location.innerText = app.Ort;
    location.className = 'location';

    const target = document.createElement('td');
    target.innerText = app.Zielgruppe;
    target.className = 'target';

    tr.appendChild(date);
    tr.appendChild(location);
    tr.appendChild(desc);
    tr.appendChild(target);

    return tr;
}