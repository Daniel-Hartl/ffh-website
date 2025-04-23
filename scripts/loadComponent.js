document.addEventListener('DOMContentLoaded', () => {
    try {

        [].forEach.call(document.getElementsByClassName('dynamic-content'), function (root) {
            if (root.hasAttribute("src")) {
                fetch(root.getAttribute("src"))
                    .then(response => response.text())
                    .then(content => {
                        root.innerHTML = content;
                        const scripts = root.querySelectorAll('script');
                        scripts.forEach(oldScript => {
                            const newScript = document.createElement('script');
                            // Append script
                            if (oldScript.src) {
                                newScript.src = oldScript.src;
                            } 
                            else {
                                newScript.textContent = oldScript.textContent;
                            }

                            document.body.appendChild(newScript);
                        });
                    });
            }
        });
    }
    catch {
        alert("Failed to load file");
    }
});