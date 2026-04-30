const tab = "\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0";

root = document.getElementById('marquee').children[0];
console.log(root);
if (root) {
    const res = getAppointments()
        .then(x => {
            root.innerHTML = "Anstehende Termine:" + tab + x.map((y) => `${y.Zeit} <b>${y.Titel}</b> am ${y.Ort}`).join(tab + "+++" + tab);
            
            setTimeout(() => {
                const scrollWidth = root.scrollWidth;
                const viewportWidth = window.innerWidth;
                const duration = (scrollWidth / 100) + 2;
                root.style.setProperty('--animation-duration', duration + 's');
            }, 0);
        });
}
