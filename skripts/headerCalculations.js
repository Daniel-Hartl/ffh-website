
window.addEventListener('scroll', () => {
    const banner = document.getElementById('banner');
    const logo = document.getElementById('logo');
    const navabar = document.getElementById('navbar');

    const bannerRect = banner.getBoundingClientRect();
    const navabarHeight = navabar.clientHeight;


    // calculate height of logo according to banner height
    completeHeight = 0;

    if (bannerRect.bottom <= navabarHeight) {
        completeHeight = navabarHeight;
    }
    else {
        completeHeight = bannerRect.bottom;
    }

    logo.style.height = `${getHeight(completeHeight)}px`;
    logo.style.marginTop = `${getMargin(completeHeight)}px`;
    logo.style.marginBottom = `${getMargin(completeHeight)}px`;
});

function getMargin(height) {
    return height * 0.05;
}

function getHeight(height) {
    return height * 0.9;
}