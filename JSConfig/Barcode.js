$(function () {
    $('#OpenBarcode').click(function (e) {


        var memberId = 'M_ID24-10001';
        JsBarcode("#barcode", memberId, { format: "CODE128" });

        $('#showBarcode').modal('show');
    });
})


function printBarcode() {
    var barcode = document.getElementById("barcode").outerHTML; // Get the SVG content
    var printWindow = window.open('', '', 'height=600,width=800'); // Create a new window
    printWindow.document.write('<html><head><title>Print Barcode</title>');
    printWindow.document.write('</head><body>');
    printWindow.document.write(barcode); // Write the barcode SVG to the new window
    printWindow.document.write('</body></html>');
    printWindow.document.close(); // Close the document
    printWindow.print(); // Trigger the print dialog
}

function previewImage(event) {
    const imagePreview = document.getElementById('imagePreview');
    imagePreview.src = URL.createObjectURL(event.target.files[0]);
    imagePreview.style.display = 'block';
}



$(function () {
    $('#showMemberSettings').click(function (e) {

        $('#MemberSettingsModal').modal('show');
    });
})

 

$(function () {
    $("#SaveStatusUpdate").click(function (event) {
        event.preventDefault();

        var password = $('#UserPassword').val();
        if (!password && $('#CheckEmptyPassword').val() === "") {
            alert('Password is required since this account is newly created.');
        } else {
            // Proceed with saving changes


            var No_ = $('#TxtNo_').text();
            //alert(No_);
            var formUserUpdate = new FormData();

            formUserUpdate.append("EntryBy", $("#EntryBy").text());
            formUserUpdate.append("No_", No_.trim());

            const password = $("#UserPassword").val().trim();

            formUserUpdate.append("Password", password);

            formUserUpdate.append("AccountStatus", $("#AccountStatus").val().trim());
            //formDataUser_.append("Department", $("#DEPARTMENT_").val().trim());

            $.ajax({
                type: "POST",
                url: "/Account/UpdateAccountStatus",
                processData: false, // Prevent jQuery from processing the data
                contentType: false, // Set content type to false for FormData
                data: formUserUpdate,
                success: function () {
                    alert('Form Updated successfully!');
                    window.location.href = '/Members/Profile_?No_=' + No_;
                },
                error: function (xhr, status, error) {
                    alert('Error occurred: ' + error);
                }
            });
        }
    });
});
