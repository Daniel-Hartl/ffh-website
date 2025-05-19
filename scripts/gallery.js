let display;
let displayContent;
let displayOpen = 0;

document.addEventListener('DOMContentLoaded', function () {
    display = document.getElementById("display");
    displayContent = document.getElementById("display-content");
});

function openDisplay(image) {
    displayContent.src = image;
    display.style.display = "flex";
    displayOpen = 2;
}

window.onclick = (event) => {
    if (displayOpen === 2) {
        displayOpen = 1;
    }
    else if (displayOpen === 1) {
        display.style.display = "none";
        displayOpen = 0;
    }
};