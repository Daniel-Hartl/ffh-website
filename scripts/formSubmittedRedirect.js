document.addEventListener('DOMContentLoaded', () => {
    var form = document.getElementById("contact-form")
    if (form) {
        // redirect to form-submitted.html
        const redirectInput = document.createElement('input');
        redirectInput.type = 'hidden';
        redirectInput.name = 'redirect';
        redirectInput.value = window.location.origin + '/form-submitted.html';
        form.appendChild(redirectInput);
    }
});


