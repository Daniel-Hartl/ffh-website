root = document.getElementsByTagName('marquee')[0];
if (root) {
    const res = getAppointments()
        .then(x => {
            console.log(x);
             root.innerHTML = x.map((y) => `${y.Zeit} <b>${y.Titel}</b> am ${y.Ort}`).join("\xa0\xa0. : .\xa0\xa0");
             //root.innerText = x.map((y) => y.Ort).join("   . : .   ");
    });
}
