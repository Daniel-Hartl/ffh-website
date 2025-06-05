document.addEventListener('DOMContentLoaded', async () => {
    const tabs = document.getElementsByClassName('tabcontent');
    
    const statistics = await fetch('data/gallery.json').then(response => response.json());

});