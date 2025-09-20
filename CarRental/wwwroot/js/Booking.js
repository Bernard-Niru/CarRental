document.addEventListener('DOMContentLoaded', () => {
    const modal = document.getElementById('bookingModal');

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
    const bookingButtons = document.querySelectorAll('.booking-btn');
    const bookingIdInput = document.getElementById('BookingID'); // new hidden input
    const carIdInput = document.getElementById('CarID'); // new hidden input
    const userIdInput = document.getElementById('UserID'); // new hidden input
    const userNameInput = document.getElementById('Username');
    const carNameInput = document.getElementById('CarName');
    const pickupDateInput = document.getElementById('PickupDate');
    const returnDateInput = document.getElementById('ReturnDate'); // new hidden input
    const conditionInput = document.getElementById('Condition');
    const rentalAmountInput = document.getElementById('RentalAmount');
    const additionalChargesInput = document.getElementById('AdditionalCharges')
    const discountInput = document.getElementById('Discount')

    bookingButtons.forEach(button => {
        button.addEventListener('click', () => {
            const bookingId = button.getAttribute('data-bookingid');
            const carId = button.getAttribute('data-carid');
            const userId = button.getAttribute('data-userid');
            const userName = button.getAttribute('data-username'); // get car id from button
            const carName = button.getAttribute('data-carname');
            const pickupDate = button.getAttribute('data-pickupdate');
            const returnDate = button.getAttribute('data-returndate');
            const rentalAmount = button.getAttribute('data-rentalrate');

            if (bookingIdInput) bookingIdInput.value = bookingId;
            if (carIdInput) carIdInput.value = carId;
            if (userIdInput) userIdInput.value = userId;
            if (userNameInput) userNameInput.value = userName;  // set hidden input value
            if (carNameInput) carNameInput.value = carName;
            if (pickupDateInput) pickupDateInput.value = pickupDate;
            if (returnDateInput) returnDateInput.value = returnDate;
 
          

            const pickup = new Date(pickupDate);
            const ret = new Date(returnDate);
            const days = Math.ceil((ret - pickup) / (1000 * 60 * 60 * 24)); // ms to days

            const amount = parseFloat(rentalAmount);
            if (rentalAmountInput) rentalAmountInput.value = (1 + days) * amount ;

            
            
        });
    });
});
