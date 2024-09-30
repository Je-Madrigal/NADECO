$(function () {
    LoadBarangay();
    LoadMunicipality();
});


function LoadBarangay() {
    $('#Barangay_').autocomplete({

        minLength: 0,
        source: function (request, response) {
            var SPName = 'SP_BARANGAY';
            var Param = $('#Barangay_').val();
            var CityNo = $('#CityNo_').val();

            $.ajax({
                url: ' ../Features/LookUpWebService.asmx/FetchBarangay',
                method: 'post',
                dataType: 'json',
                data: {
                    Param: Param,
                    SPName: SPName,
                    CityNo: CityNo,
                },
                success: function (data) {
                    response($.map(data, function (el) {
                        return {
                            label: el.Name,
                            No_: el.No_
                        };
                    }));
                },
                error: function (xhr, status, error) {
                    console.error('AJAX Error:', status, error);
                }
            });
        },
        select: function (event, ui) {

            console.log('Selected:', ui.item);
            $('#Barangay_').val(ui.item.label); 
            $('#BarangayNo').val(ui.item.No_);
         
            console.log('Selected:', ui.item); // Debugging: Check select event
            return false; // Prevent the default behavior of the autocomplete plugin
        },
        change: function (event, ui) {
            if (!(ui.item)) {
                console.log('Selected:', ui.item);
                event.target.value = "";
                $('#Barangay_').val('');
                $('#BarangayNo').val('');

            }

            console.log('Change event:', ui.item);
        },
       // appendTo: "#AddInsurance" // Replace with your modal's ID or selector
    }).focus(function () {
        $(this).data("uiAutocomplete").search($(this).val());
    });
}




function LoadMunicipality() {

    $('#City_').autocomplete({

        minLength: 0,
        source: function (request, response) {
            var SPName = 'SP_MUNICIPALITY';
            var Param = $('#City_').val();

            $.ajax({
                url: ' ../Features/LookUpWebService.asmx/FetchCity',
                method: 'post',
                dataType: 'json',
                data: {
                    Param: Param,
                    SPName: SPName,
                },
                success: function (data) {
                    response($.map(data, function (el) {
                        return {
                            label: el.Name,
                            prov: el.Province,
                            zip: el.ZipCode,
                            No_: el.No_
                        };
                    }));
                },
                error: function (xhr, status, error) {
                    console.error('AJAX Error:', status, error);
                }
            });
        },
        select: function (event, ui) {

            console.log('Selected:', ui.item);
            $('#City_').val(ui.item.label);
            $('#CityNo_').val(ui.item.No_);
            $('#Province_').val(ui.item.prov);
            $('#ZipCode').val(ui.item.zip);
   
            return false; // Prevent the default behavior of the autocomplete plugin
        },
        change: function (event, ui) {
            if (!(ui.item)) {
                console.log('Selected:', ui.item);
                event.target.value = "";
                $('#City_').val('');
                $('#CityNo_').val('');
                $('#Barangay_').val('');
                $('#BarangayNo').val('');
            }

            console.log('Change event:', ui.item);
        },
        // appendTo: "#AddInsurance" // Replace with your modal's ID or selector
    }).focus(function () {
        $(this).data("uiAutocomplete").search($(this).val());
    });
}