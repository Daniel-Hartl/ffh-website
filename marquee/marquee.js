const tab = "\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0";

root = document.getElementById('marquee').children[0];
console.log(root);
if (root) {
    const res = getAppointments()
        .then(x => root.innerHTML = "Anstehende Termine:" + tab + x.map((y) => `${y.Zeit} <b>${y.Titel}</b> am ${y.Ort}`).join(tab + "+++" + tab));
}
