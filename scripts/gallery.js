const display = document.getElementsByClassName("display")[0];
const displayContent = document.getElementsByClassName("display-content")[0];
const galleryImages = Array.from(document.querySelectorAll(".tabcontent img")).map(a => a.src);

let currentIndex = -1;

function openDisplay(image) {
    console.log(displayContent)
    console.log(display)
    displayContent.src = image;
    currentIndex = galleryImages.indexOf(image);
    display.style.display = "flex";
}

function openNextIndex(index) {
    currentIndex = index;
    modalImg.src = galleryImages[currentIndex].src;
    modal.style.display = "flex";
}

function closeDisplay() {
    display.style.display = "none";
    currentIndex = -1;
}

window.onclick = (event) => {
    if (event.target === display) closeDisplay();
};

// Keyboard navigation
document.addEventListener("keydown", (e) => {
    if (display.style.display !== "flex") return;

    switch (e.key) {
        case "ArrowRight":
            if (currentIndex < galleryImages.length - 1) {
                openDisplay(currentIndex + 1);
            }
            break;
        case "ArrowLeft":
            if (currentIndex > 0) {
                openDisplay(currentIndex - 1);
            }
            break;
        case "Escape":
            closeDisplay();
            break;
    }
});