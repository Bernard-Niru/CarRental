document.addEventListener('DOMContentLoaded', () => {
    const modal = document.getElementById('addCarModal');

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
});
document.addEventListener('DOMContentLoaded', () => {
    const modal = document.getElementById('addCarModal');

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
});
