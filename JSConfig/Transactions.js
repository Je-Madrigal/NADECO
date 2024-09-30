$(document).ready(function () {

 
    function calculateTotal() {

        var cashSales = parseFloat($('#TxtMembershipFee').val()) || 0;
        var shareCapital = parseFloat($('#TxtShares').val()) || 0;
        var timeDeposits = parseFloat($('#TxtTimeDeposits').val()) || 0;
        var total = cashSales + shareCapital + timeDeposits;
        $('#TxtTotalRegistration_').val(total.toFixed(2)); // Display total with two decimals
    }

    function validateShares() {
        var subscriptionAmount = parseFloat($('#TxtSubsription').val()) || 0;
        var sharesAmount = parseFloat($('#TxtShares').val()) || 0;

        if (sharesAmount > subscriptionAmount) {
            alert("Share Capital Subscription must be less than or equal to Subscription Amount.");
            $('#TxtShares').val(subscriptionAmount.toFixed(2)); // Set shares to subscription amount if invalid
        }
    }

    $('input[type="text"]').on('input', function () {
        this.value = this.value.replace(/[^0-9.]/g, ''); // Allow only numbers and decimals
        calculateTotal();
        validateShares();
    });
    calculateTotal();
});


$('#ProcessTransaction').click(function (e) {
    e.preventDefault(); // Prevents the default action
  
    var MembersFeeData = {
        EntryBy: $('#EntryBy').text(),
        MemberNo: $('#TxtNo_').text().trim(),
        Total: $('#TxtTotalRegistration_').val().trim(),
        MembershipFee: $('#TxtMembershipFee').val(),
        Subscription: $('#TxtSubsription').val(),
        Shares: $('#TxtShares').val(),
        TimeDeposits: $('#TxtTimeDeposits').val()
     
        // Exclude hidden fields
    };

    $.ajax({
        url: '/Transactions/PostTransaction',
        type: 'POST',
        data: MembersFeeData,
        success: function (response) {
            alert("Successfully Posted");
            window.location.href = '/Members/Profile_?No_=' + $('#TxtNo_').text().trim();

            // Handle success
        },
        error: function (xhr, status, error) {
            // Handle error
        }
    });
});

