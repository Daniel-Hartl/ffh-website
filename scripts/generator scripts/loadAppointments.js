function getAppointments() {
    try {
        fetch('data/appointments.json')
            .then(response => response.json())
            .then(articles => articles.sort(compareDate))
    }
    catch (err) {
        console.error('Fehler beim Einlesen der Datei:', err.message);
    }
}

function compareDate(a, b) {
    aArr = String(a.Zeit).split("-")[0].split(",");
    bArr = String(b.Zeit).split("-")[0].split(",");
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