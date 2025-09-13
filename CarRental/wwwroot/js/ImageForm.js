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
    }

    // New: Units Modal Elements
    const carUnitsModal = document.getElementById('carUnitsModal');
    const carNameUnitsModal = document.getElementById('carNameUnitsModal');
    const unitsList = document.getElementById('carUnitsList');
    const unitCarIdInput = document.getElementById('UnitCarID');
    const addUnitForm = document.getElementById('addUnitForm');
    const newPlateInput = document.getElementById('newPlateNumber');

    if (carUnitsModal) {
        carUnitsModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget;  // button that triggered the modal
            const carId = button.getAttribute('data-carid');
            const carName = button.getAttribute('data-carname');

            // Set car name
            if (carNameUnitsModal && carName) {
                carNameUnitsModal.textContent = carName;
            }

            // Set hidden input
            if (unitCarIdInput && carId) {
                unitCarIdInput.value = carId;
            }

            // Clear old units list
            if (unitsList) {
                unitsList.innerHTML = '';
            }

            // TODO: Load existing units from backend, for now let's simulate
            if (carId && unitsList) {
                // Replace this with real AJAX call to get units for carId
                // Example simulated units:
                const exampleUnits = ['ABC-123', 'XYZ-789']; // Dummy data
                exampleUnits.forEach(unit => {
                    const li = document.createElement('li');
                    li.className = 'list-group-item';
                    li.textContent = unit;
                    unitsList.appendChild(li);
                });
            }
        });

        carUnitsModal.addEventListener('hidden.bs.modal', () => {
            // Clear units list and form inputs on close
            if (unitsList) {
                unitsList.innerHTML = '';
            }
            if (unitCarIdInput) {
                unitCarIdInput.value = '';
            }
            if (newPlateInput) {
                newPlateInput.value = '';
            }
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.remove('blur-backdrop');
        });

        carUnitsModal.addEventListener('shown.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.add('blur-backdrop');
        });

        // Handle adding a new plate number unit
        if (addUnitForm) {
            addUnitForm.addEventListener('submit', e => {
                e.preventDefault();

                const plateNumber = newPlateInput.value.trim();
                const carId = unitCarIdInput.value;

                if (!plateNumber || !carId) return;



                // For demo, we'll just append without backend call:
                const li = document.createElement('li');
                li.className = 'list-group-item';
                li.textContent = plateNumber;
                unitsList.appendChild(li);
                newPlateInput.value = '';
            });
        }
    }
});
