function acceptCookies(component) {
    // Cookie setzen (einfacher Weg, du kannst auch localStorage verwenden)
    document.cookie = "googleConsent=true; path=/";
    document.getElementById("placeholder").style.display = "none";

    if(component === "streetView")
        loadStreetView();
    else if (component === "calendar")
        loadCalendar();
}

function hasConsent() {
    return document.cookie.split("; ").includes("googleConsent=true");
}





// Beim Laden prüfen, ob bereits Zustimmung erteilt wurde
window.onload = function () {
    if (hasConsent()) {
        element = document.getElementById("placeholder");
        element.style.display = "none";
        loadStreetView(element.className);
    }
}