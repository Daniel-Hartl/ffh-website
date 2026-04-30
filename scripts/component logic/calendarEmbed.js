function loadStreetView() {
    maps = document.createElement("iframe");
    maps.src = "https://calendar.google.com/calendar/embed?src=ebe12d2c011447883173f987a7608c5ec24f4511ad317b3bd27a857e57acb8d4%40group.calendar.google.com&ctz=Europe%2FBerlin";
    maps.height = "100%";
    maps.width = "100%";
    document.getElementById("placeholder").replaceWith(maps);
}

let appointmentRows = [];
let appointmentRoot = null;

async function loadAppointmentsTable() {
    appointmentRoot = document.getElementById('appointments');
    if (!appointmentRoot) {
        return;
    }

    const appointments = await getAppointments() || [];
    appointmentRows = appointments.map(app => ({
        data: app,
        row: generateAppointment(app)
    }));

    appointmentRows.forEach(entry => appointmentRoot.appendChild(entry.row));
    setupAppointmentFilters();
}

document.addEventListener('DOMContentLoaded', loadAppointmentsTable);

function setupAppointmentFilters() {
    const filters = document.querySelectorAll('input[name="appointmentFilter"]');
    filters.forEach(filter => filter.addEventListener('change', () => {
        const value = document.querySelector('input[name="appointmentFilter"]:checked')?.value || 'Alle';
        filterAppointments(value);
    }));
}

function filterAppointments(filterValue) {
    appointmentRows.forEach(entry => {
        const matches = filterValue === 'Alle' || entry.data.Zielgruppe === filterValue;
        entry.row.style.display = matches ? '' : 'none';
    });
}

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