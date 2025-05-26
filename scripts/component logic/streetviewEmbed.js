let maps;
let mapsContentDiv;
let tooltipSpan;
let activity;
let isProtected;
let cookiesAgreed;

document.addEventListener('DOMContentLoaded', function () {
    cookiesAgreed = false;
    isProtected = true;
    maps = document.getElementById('maps');
    mapsContentDiv = document.getElementsByClassName('maps-content-div')[0];
    activity = document.getElementById('activity');
    tooltipSpan = document.getElementsByClassName('tooltip')[0];
});

function loadStreetView() {
    maps = document.createElement("iframe");
    maps.src = "https://www.google.com/maps/embed?pb=!4v1748256388429!6m8!1m7!1smLBiQCBd65Nlt3lBuh6L-Q!2m2!1d49.11995334992711!2d11.96438334937885!3f45.57523423695113!4f2.7963928879350277!5f0.7828376962214665";
    maps.height = "100%";
    maps.width = "100%";
    document.getElementById("placeholder").replaceWith(maps);
    document.getElementById('streetViewFooter').classList.remove('hidden');
    mapsContentDiv.addEventListener('click', () => removeProtection());
    addProtection();
}

function removeProtection() {
    maps.classList.remove('protected');
    mapsContentDiv.classList.remove('tooltipable');
    activity.innerText = "aktiv";
    activity.classList.remove('inactive');
    activity.classList.add('active');
    isProtected = false;
}

function toggleProtection() {
    if (isProtected)
        removeProtection()
    else
        addProtection();
}


function addProtection() {
    maps.classList.add('protected');
    mapsContentDiv.classList.add('tooltipable');
    activity.innerText = "inaktiv";
    activity.classList.remove('active');
    activity.classList.add('inactive');
    isProtected = true;
}


window.onmousemove = function (e) {
    var x = e.clientX,
        y = e.clientY;
    tooltipSpan.style.top = (y + 10) + 'px';
    tooltipSpan.style.left = (x + 10) + 'px';
};