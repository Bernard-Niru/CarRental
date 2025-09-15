document.getElementById("color-select").addEventListener("change", function () {
    const customColor = document.getElementById("custom-color-input");
    customColor.style.display = this.value === "Other" ? "block" :
        "none";
  
});
document.getElementById('add-unit-btn').addEventListener('click', function () {
    const container = document.getElementById('units-container');
    const input = document.createElement('input');
    input.type = 'text';
    input.name = 'Units';  // Same name to get array on submit
    input.className = 'form-control mb-2';
    input.placeholder = 'Enter plate number';
    container.appendChild(input);
});


