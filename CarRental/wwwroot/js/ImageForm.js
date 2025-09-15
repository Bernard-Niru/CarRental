document.addEventListener('DOMContentLoaded', () => {
    // Existing Car Images Modal code here ...
    const carImagesModal = document.getElementById('carImagesModal');
    const carNameModal = document.getElementById('carNameModal');
    const imageContainer = document.getElementById('carImagesContainer');
    const imageCarIdInput = document.getElementById('ImageCarID');

    if (carImagesModal) {
        carImagesModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget;  // button that triggered the modal
            const carId = button.getAttribute('data-carid');
            const carName = button.getAttribute('data-carname');

            // Set car name title
            if (carNameModal && carName) {
                carNameModal.textContent = carName;
            }

            // Set hidden input for upload form
            if (imageCarIdInput && carId) {
                imageCarIdInput.value = carId;
            }

            // Load and inject existing images for this car
            if (carId && imageContainer) {
                const hiddenDiv = document.getElementById(`images-container-${carId}`);
                imageContainer.innerHTML = hiddenDiv ? hiddenDiv.innerHTML : '<p>No images available.</p>';
            }
        });

        carImagesModal.addEventListener('hidden.bs.modal', () => {
            // Clear images and form inputs on close
            if (imageContainer) {
                imageContainer.innerHTML = '';
            }
            if (imageCarIdInput) {
                imageCarIdInput.value = '';
            }
            const fileInput = document.getElementById('ImageFiles');
            if (fileInput) {
                fileInput.value = '';
            }

            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.remove('blur-backdrop');
        });

        carImagesModal.addEventListener('shown.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.add('blur-backdrop');
        });
    });
});

document.addEventListener('DOMContentLoaded', () => {
    const modal = document.getElementById('carUnitsModal');

    const carIdInput = document.getElementById('CarID');
    const carNameInput = document.getElementById('CarName');
    const unitsTableBody = document.getElementById('unitsTableBody');

    // Handle blur effect
    if (modal) {
        modal.addEventListener('shown.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.add('blur-backdrop');
        });

        modal.addEventListener('hidden.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.remove('blur-backdrop');
            if (unitsTableBody) unitsTableBody.innerHTML = ''; // clear old data
        });
    }

    // Click handler for each button
    const viewButtons = document.querySelectorAll('.view-units-btn');

    const unitsModal = document.getElementById('carUnitsModal');

    if (unitsModal) {
        unitsModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget; // The button that triggered the modal
            const carId = button.getAttribute('data-carid');
            const carName = button.getAttribute('data-carname');

            const carIdInput = document.getElementById('CarID');
            const carNameInput = document.getElementById('CarName');
            const unitsTableBody = document.getElementById('unitsTableBody');

            if (carIdInput) carIdInput.value = carId;
            if (carNameInput) carNameInput.value = carName;

            const unitDivs = button.querySelectorAll('.unit-data .unit');
            if (unitsTableBody) {
                unitsTableBody.innerHTML = ''; // Clear previous rows

                unitDivs.forEach(unit => {
                    const plate = unit.getAttribute('data-plate');
                    const available = unit.getAttribute('data-available');

                    const row = document.createElement('tr');
                    row.innerHTML = `
                    <td>${plate}</td>
                    <td>${available}</td>
                `;
                    unitsTableBody.appendChild(row);
                });
            }
        });

        unitsModal.addEventListener('hidden.bs.modal', () => {
            const unitsTableBody = document.getElementById('unitsTableBody');
            if (unitsTableBody) unitsTableBody.innerHTML = ''; // clean up
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.remove('blur-backdrop');
        });

        unitsModal.addEventListener('shown.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.add('blur-backdrop');
        });
    }
});

    