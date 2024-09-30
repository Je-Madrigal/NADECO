$(document).ready(function () {
    $('.myTable').DataTable();
})


$(document).ready(function () {
    // Move the label before the select element
    var select = $('#dt-length-0');
    var label = $('label[for="dt-length-0"]');
    label.insertBefore(select);
    // Add space between the label and select
    label.after('&nbsp;');
    // Change label text
    label.text('Entries per page:');
});




$(document).ready(function () {
    // Move the label before the select element
    var select = $('#dt-length-0');
    var label = $('label[for="dt-length-0"]');
    label.insertBefore(select);
    // Add space between the label and select
    label.after('&nbsp;');
    // Change label text
    label.text('Entries per page:');
});
