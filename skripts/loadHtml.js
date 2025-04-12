document.addEventListener('DOMContentLoaded', () => {
    try {

        [].forEach.call(document.getElementsByClassName('content-container'), function(root) {
        if (root.hasAttribute("src")) {
            fetch(root.getAttribute("src"))
            .then(response => response.text())
            .then(content => root.innerHTML = content);
        }});
    }
    catch {
        alert("Failed to load file");
    }
});