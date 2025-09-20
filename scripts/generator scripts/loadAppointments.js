function getAppointments() {
    try {
        return fetch('data/appointments.json')
            .then(response => response.json())
            .then(articles => articles.sort(compareDate))
    }
    catch (err) {
        console.error('Fehler beim Einlesen der Datei:', err.message);
    }
}

function compareDate(a, b) {
    aArr = String(a.Zeit).split("-")[0].split(".");
    bArr = String(b.Zeit).split("-")[0].split(".");
    if (Number(aArr[2]) < Number(bArr[2])) {
        return -1;
    }
    else if (Number(aArr[2]) > Number(bArr[2])) {
        return 1;
    }
    else if (Number(aArr[1]) < Number(bArr[1])) {
        return -1;
    }
    else if (Number(aArr[1]) > Number(bArr[1])) {
        return 1;
    }
    else if (Number(aArr[0]) < Number(bArr[0])) {
        return -1;
    }
    else if (Number(aArr[0]) > Number(bArr[0])) {
        return 1;
    }
    else {
        return 0;
    }
}