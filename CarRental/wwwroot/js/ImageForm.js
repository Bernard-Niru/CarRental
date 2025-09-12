document.addEventListener('DOMContentLoaded', () => {
    const imageModal = document.getElementById('AddImage');
    const imageCarIdInput = document.getElementById('ImageCarID');

    if (imageModal) {
        imageModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget;
            const carId = button.getAttribute('data-carid');

            if (imageCarIdInput && carId) {
                imageCarIdInput.value = carId;
            }
        });

        imageModal.addEventListener('hidden.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.remove('blur-backdrop');
        });

        imageModal.addEventListener('shown.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.add('blur-backdrop');
        });
    }
});
