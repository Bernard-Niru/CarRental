document.getElementById("color-select").addEventListener("change", function () {
    const customColor = document.getElementById("custom-color-input");
    customColor.style.display = this.value === "Other" ? "block" :
    "none";
});
