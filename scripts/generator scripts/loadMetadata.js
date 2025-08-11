document.addEventListener('DOMContentLoaded', () => {
    // set metadata
    setMetadata()

    // apply icon
    setIcon();

    // apply title
    setTitle();

    // import poppins font
    importPoppins();

    // add default stylesheet
    addStyle();
});

function importPoppins() {
    const poppinsGoogle = document.createElement('link');
    poppinsGoogle.rel = 'preconnect';
    poppinsGoogle.href = 'https://fonts.googleapis.com';
    document.head.appendChild(poppinsGoogle);

    const poppinsGstatic = document.createElement('link');
    poppinsGstatic.rel = 'preconnect';
    poppinsGstatic.href = 'https://fonts.gstatic.com';
    poppinsGstatic.crossOrigin = '';
    document.head.appendChild(poppinsGstatic);

    const poppinsOnlineStylesheet = document.createElement('link');
    poppinsOnlineStylesheet.href = 'https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap';
    poppinsOnlineStylesheet.rel = 'stylesheet';
    document.head.appendChild(poppinsOnlineStylesheet);

    const poppinsLocalStylesheet = document.createElement('link');
    poppinsLocalStylesheet.rel = 'stylesheet';
    poppinsLocalStylesheet.href = 'fonts/poppins.css';
    document.head.appendChild(poppinsLocalStylesheet);
}

function setTitle() {
    const title = document.createElement('title');
    title.innerText = 'FF Heitzenhofen';
    document.head.appendChild(title);
}

function setIcon() {
    const icon = document.createElement('link');
    icon.href = 'images/favicon.png';
    icon.rel = 'icon';
    icon.type = 'image/png';
    document.head.appendChild(icon);
}

function setMetadata() {
    const viewport = document.createElement('meta');
    viewport.name = 'viewport';
    viewport.content = 'width=device-width, initial-scale=1.0';
    document.head.appendChild(viewport);

    const description = document.createElement('meta');
    description.name = 'description';
    description.content = 'Die Freiwillige Feuerwehr Heitzenhofen ist eine Feuerwehr der Gemeinde Duggendorf,' +
            ' mit Einsatzgebiet im östlichen Gemeindegebiet. Ihr Gerätehaus befindet sich im Gemeindeteil Judenberg.';
    document.head.appendChild(description);
}

function addStyle() {
    const style = document.createElement('link');
    style.rel = 'stylesheet';
    style.href = 'styles/style.css';
    document.head.appendChild(style);
}