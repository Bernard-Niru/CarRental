document.addEventListener('DOMContentLoaded', () => {
    const modal = document.getElementById('addCarModal');

    // Add blur effect to backdrop when modal is shown
    if (modal) {
        modal.addEventListener('shown.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.add('blur-backdrop');
        });

        modal.addEventListener('hidden.bs.modal', () => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) backdrop.classList.remove('blur-backdrop');
        });
    }

    // Populate modal fields when request button is clicked
    const requestButtons = document.querySelectorAll('.request-btn');
    const carIdInput = document.getElementById('CarID'); // new hidden input
    const carNameInput = document.getElementById('CarName');
    const rentalRateInput = document.getElementById('RentalRate');
    const brandRateInput = document.getElementById('BrandName');

    requestButtons.forEach(button => {
        button.addEventListener('click', () => {
            const carId = button.getAttribute('data-carid'); // get car id from button
            const carName = button.getAttribute('data-carname');
            const rentalRate = button.getAttribute('data-rentalrate');
            const brand = button.getAttribute('data-brand');

            if (brandRateInput) brandRateInput.value = brand;
            if (carIdInput) carIdInput.value = carId;  // set hidden input value
            if (carNameInput) carNameInput.value = carName;
            if (rentalRateInput) rentalRateInput.value = rentalRate;
        });
    });
});
