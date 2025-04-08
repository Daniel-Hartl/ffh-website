
window.addEventListener('scroll', () => {
    const banner = document.getElementById('banner');
    const logo = document.getElementById('logo');
    const navabar = document.getElementById('navbar');

    const bannerRect = banner.getBoundingClientRect();
    const navabarHeight = navabar.clientHeight;

    // logo.style.minHeight = `calc(${navabarHeight}px - 10px)`;

    // if (bannerRect.bottom <= navabarHeight) {
    //     logo.style.height = `calc(${navabarHeight}px - 10px)`;
    //     logo.style.margin = `5px`;
    // }
    // else {
    //     logo.style.height = `${bannerRect.bottom}px`;
    // }

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