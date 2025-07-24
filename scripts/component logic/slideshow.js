let slideIndex = 0;
let timer;
let canTimerRun = true;
let timerRunning = false;
let pauseButton;


document.addEventListener('DOMContentLoaded', () => {
    //console.log("dom loaded")
    showSlides(slideIndex);
    pauseButton = document.getElementsByClassName("pause-button")[0]
});

function plusSlides(n) {
    //   console.log("plusSlides called")
    resetTimer();
    showSlides(slideIndex + n);
}

function currentSlide(n) {
    console.log("currentSlide called " + n)
    pauseTimer();
    showSlides(n);
}

function showSlides(n) {
    //   console.log("showSlides called")
    let i;
    let slides = document.getElementsByClassName("mySlides");
    slides = Array.from(slides).filter((item) => { return !item.className.includes("pc") || window.matchMedia("(width > 1000px)").matches  });
    
    let dots = document.getElementsByClassName("dot");
    dots = Array.from(dots).filter((item) => { return !item.className.includes("pc") || window.matchMedia("(width > 1000px)").matches  });

    let descriptions = document.getElementsByClassName("description");
    slideIndex = n % slides.length;
    if (slideIndex < 0)
        slideIndex += slides.length;
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
        if (descriptions.length > i)
            descriptions[i].style.display = "none";
        dots[i].className = dots[i].className.replace(" active", "");
    }

    slides[slideIndex].style.display = "block";
    dots[slideIndex].className += " active";
    if (descriptions.length > slideIndex)
        descriptions[slideIndex].style.display = " block";
    setTimer();
}

function setTimer() {
    //console.log("setTimer called")
    if (canTimerRun && !timerRunning) {
        timer = setInterval(timerElapsed, 5000);
        timerRunning = true;
    }
}

function timerElapsed() {
    //  console.log("timerElapsed called")
    clearInterval(timer);
    if (canTimerRun) {
        timerRunning = false;
        showSlides(slideIndex + 1)
    }
}

function startTimer() {
    setTimer();
    canTimerRun = true;
    pauseButton.className = pauseButton.className.replace("paused", "running");
    pauseButton.innerHTML = `<svg width="1.2rem" height="1.2rem" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M21.4086 9.35258C23.5305 10.5065 23.5305 13.4935 21.4086 14.6474L8.59662 21.6145C6.53435 22.736 4 21.2763 4 18.9671L4 5.0329C4 2.72368 6.53435 1.26402 8.59661 2.38548L21.4086 9.35258Z" fill="#FFFFFF"/>
        </svg>`
}

function resetTimer() {
    clearInterval(timer);
    timerRunning = false;
    startTimer();
}

function pauseTimer() {
    //console.log("pauseTimer called")
    clearInterval(timer);
    timerRunning = false;
    canTimerRun = false;
    pauseButton.className = pauseButton.className.replace("running", "paused");
    pauseButton.innerHTML = `<svg width="1.2rem" height="1.2rem" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M2 6C2 4.11438 2 3.17157 2.58579 2.58579C3.17157 2 4.11438 2 6 2C7.88562 2 8.82843 2 9.41421 2.58579C10 3.17157 10 4.11438 10 6V18C10 19.8856 10 20.8284 9.41421 21.4142C8.82843 22 7.88562 22 6 22C4.11438 22 3.17157 22 2.58579 21.4142C2 20.8284 2 19.8856 2 18V6Z" fill="#FFFFFF"/>
        <path d="M14 6C14 4.11438 14 3.17157 14.5858 2.58579C15.1716 2 16.1144 2 18 2C19.8856 2 20.8284 2 21.4142 2.58579C22 3.17157 22 4.11438 22 6V18C22 19.8856 22 20.8284 21.4142 21.4142C20.8284 22 19.8856 22 18 22C16.1144 22 15.1716 22 14.5858 21.4142C14 20.8284 14 19.8856 14 18V6Z" fill="#FFFFFF"/>
        </svg>`;
}

function togglePause() {
    //console.log("called togglePause")
    if (timerRunning) {
        pauseTimer();
    }
    else {
        startTimer();
    }
}