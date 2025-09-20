//function uploadProfilePicture(input) {
//    const file = input.files[0];
//    if (file) {
//        alert("Selected file: " + file.name);
//    }
//}
function uploadProfilePicture(input) {
    const file = input.files[0];
    if (file && file.type.startsWith('image/')) {
        const reader = new FileReader();

        reader.onload = function (e) {
            // Set the image src to the uploaded image
            document.getElementById('profileImage').src = e.target.result;
        };

        reader.readAsDataURL(file); // Read file as data URL (base64)
    } else {
        alert("Please select a valid image file.");
    }
}