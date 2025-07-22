let maps;
let mapsContentDiv;
let tooltipSpan;
let activity;
let isProtected;
let cookiesAgreed;
let streetViewExists;

document.addEventListener('DOMContentLoaded', function () {
    cookiesAgreed = false;
    isProtected = true;
    streetViewExists = false;
    // maps = document.getElementById('maps');
    // mapsContentDiv = document.getElementsByClassName('maps-content-div')[0];
    // activity = document.getElementById('activity');
    // tooltipSpan = document.getElementsByClassName('tooltip')[0];
});

function loadStreetView() {
    loadMap();

    // maps = document.createElement("iframe");
    // maps.src = "https://www.google.com/maps/embed?pb=!4v1748256388429!6m8!1m7!1smLBiQCBd65Nlt3lBuh6L-Q!2m2!1d49.11995334992711!2d11.96438334937885!3f45.57523423695113!4f2.7963928879350277!5f0.7828376962214665";
    // maps.height = "100%";
    // maps.width = "100%";
    // document.getElementById("placeholder").replaceWith(maps);
    // document.getElementById('streetViewFooter').classList.remove('hidden');
    // mapsContentDiv.addEventListener('click', () => removeProtection());
    // streetViewExists = true;
    // addProtection();
}

function removeProtection() {
    // maps.classList.remove('protected');
    // mapsContentDiv.classList.remove('tooltipable');
    // activity.innerText = "aktiv";
    // activity.classList.remove('inactive');
    // activity.classList.add('active');
    // isProtected = false;
}

function toggleProtection() {
    // if (isProtected)
    //     removeProtection()
    // else
    //     addProtection();
}


function addProtection() {
    // maps.classList.add('protected');
    // mapsContentDiv.classList.add('tooltipable');
    // activity.innerText = "inaktiv";
    // activity.classList.remove('active');
    // activity.classList.add('inactive');
    // isProtected = true;
}


window.onmousemove = function (e) {
    if (streetViewExists) {
        var x = e.clientX,
            y = e.clientY;
        tooltipSpan.style.top = (y + 10) + 'px';
        tooltipSpan.style.left = (x + 10) + 'px';
    }
};

function loadMap() {
    maps = document.createElement("iframe");
    maps.src = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3437.649741495344!2d11.965218635296182!3d49.1205799398521!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x479f9526208dfa17%3A0x9f3780c152a4a3c8!2sFfw%20Heizenhofen!5e1!3m2!1sde!2sde!4v1753204314819!5m2!1sde!2sde";

    maps.loading = "lazy";
    document.getElementById("map-placeholder").replaceWith(maps);
}