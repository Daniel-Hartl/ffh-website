window.addEventListener('scroll', () => {
    calculateLogo();
});

function calculateLogo() {
    const banner = document.getElementsByClassName('banner');
    const logo = document.getElementById('logo');
    const navbar = document.getElementsByClassName('menubar');

    const bannerRect = banner[0].getBoundingClientRect();
    const navbarHeight = navbar[0].clientHeight;

    // calculate height of logo according to banner height
    completeHeight = 0;
    if (bannerRect.bottom <= navbarHeight) {
        completeHeight = navbarHeight;
    }
    else {
        completeHeight = bannerRect.bottom;
    }

    logo.style.height = `${getHeight(completeHeight)}px`;
    logo.style.marginTop = `${getMargin(completeHeight)}px`;
    logo.style.marginBottom = `${getMargin(completeHeight)}px`;
}

function getMargin(height) {
    return height * 0.05;
}

function getHeight(height) {
    return height * 0.9;
}