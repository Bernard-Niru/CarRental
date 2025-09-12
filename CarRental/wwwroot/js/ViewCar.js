document.setTimeout(function () {
    var alert = document.getElementById('success-alert');
    if (alert) {
        alert.classList.add('fade-out'); // Add custom class for fade animation

        // Remove from DOM after animation completes
        setTimeout(function () {
            alert.remove();
        }, 1000); // match the duration of fade-out (1s)
    }
}, 2000); // wait 2 seconds before starting fade