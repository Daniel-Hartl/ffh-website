window.addEventListener('scroll', () => {
    calculateLogo();
});

calculateLogo();

function calculateLogo() {
    const banner = document.getElementsByClassName('banner');
    const logo = document.getElementById('logo');
    const navbar = document.getElementsByClassName('menubar');

    const bannerRect = banner[0].getBoundingClientRect();
    const navbarHeight = navbar[0].clientHeight;

    completeHeight = 0;
    if (bannerRect.bottom <= navbarHeight) {
        completeHeight = navbarHeight;
    }
    else {
        const maxHeight = document.documentElement.clientHeight * 0.3;
        const bannerBottom = bannerRect.bottom > maxHeight ? maxHeight : bannerRect.bottom;

        completeHeight = bannerBottom;
    }
    logo.style.height = `${getHeight(completeHeight)}px`;
    logo.style.width = `${getWidth(completeHeight)}px`;
    logo.style.marginTop = `${getMargin(completeHeight)}px`;
    logo.style.marginBottom = `${getMargin(completeHeight)}px`;

}

function getMargin(height) {
    return height * 0.05;
}

function getHeight(height) {
    return height * 0.9;
}

function getWidth(height) {
    return getHeight(height) / 1325 * 1028;
}