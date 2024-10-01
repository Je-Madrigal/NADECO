$(document).ready(function () {
    window.onload = function () {
        var btn = document.getElementById('BtnEmployeeSubmit');
        var txtStat = document.getElementById('TxtStat');

        if (txtStat && txtStat.innerText === 'Update') {
            btn.innerText = "Save Changes";
        } else {
            btn.innerText = "Submit";
            $('#h3FullName').hide();
        }


        //HIDING SEND FOR APPROVAL BUTTOn
        var Regstatus = document.getElementById('TxtRegStatus');
        if (Regstatus && Regstatus.innerText.trim() === 'POSTED' || txtStat && txtStat.innerText === 'New') {
            $("#BtnSendforRegistration").hide();
        }
    };
});

 




$(document).ready(function () {
    $('#BtnAddNewMember').click(function (e) {

        e.preventDefault(); // Prevent the default button action
        var confirmation = confirm("Are you sure you want to create a new member?");
        if (confirmation) {
            window.location.href = '/Members/Profile_'; // Redirect if confirmed
        }
    });
});


$(document).ready(function () {
    $('#BtnEmployeeSubmit').click(function (event) {

        event.preventDefault(); // Prevent the default form submission
 
        // Validate FirstName
        if ($('#FirstName_').val() === "") {
            alert('Please input FirstName');$('#FirstName_').focus().select();$('#FirstName_').css('border-color', 'red'); // Set border color to red
            return false;
        } else { $('#FirstName_').css('border-color', ''); }     

        
        if ($('#LastName_').val() === "") {
            alert('Please input LastName'); $('#LastName_').focus().select(); $('#LastName_').css('border-color', 'red');
            return false;
        } else {
            $('#LastName_').css('border-color', ''); // Remove border color if not empty
        }

        if ($('#UserName_').val() === "") {
            alert('Please input UserName'); $('#UserName_').focus().select(); $('#UserName_').css('border-color', 'red');
            return false;
        } else {
            $('#UserName_').css('border-color', ''); // Remove border color if not empty
        }
 
        if ($('#City_').val() === "") {
            alert('Please input Municipality'); $('#City_').focus().select(); $('#City_').css('border-color', 'red');
            return false;
        } else {
            $('#City_').css('border-color', ''); // Remove border color if not empty
        }
        if ($('#Barangay_').val() === "") {
            alert('Please input Barangay'); $('#Barangay_').focus().select(); $('#Barangay_').css('border-color', 'red');
            return false;
        } else {
            $('#Barangay_').css('border-color', ''); // Remove border color if not empty
        }

 
        var No_ = $('#TxtNo_').text();
        //alert(No_);


        var formData = {
            EntryBy: $('#EntryBy').text(),
            firstName: $('#FirstName_').val(),
            middleName: $('#MiddleName_').val(),
            lastName: $('#LastName_').val(),
            No_: No_.trim(),
            memberId: $('#No_').val(),
            userName: $('#UserName_').val(),
            userEmail: $('#userEmail_').val(),
            phoneNumber: $('#PhoneNumber_').val(),
            birthDate: $('#BirthDate_').val(),
            nationality: $('#Nationality_').val(),
            gender: $('#Gender_').val(),
            civilStatus: $('#CivilStatus_').val(),
            province: $('#Province_').val(),
            city: $('#City_').val(),
            CityNo: $('#CityNo_').val(),
            barangay: $('#Barangay_').val(),
            BarangayNo: $('#BarangayNo').val(),
            zipCode: $('#ZipCode').val(),
            employeeStatus: $('#EmployeeStatus_').val(),
            membershipDate: $('#MembershipDate_').val(),
            employeeNo: $('#EmployeeNo_').val(),
            employer: $('#Employer_').val(),
            tinNo: $('#TINNo_').val(),
            occupation: $('#Occupation_').val(),
            education: $('#Education_').val(),
            salary: $('#Salary_').val(),
            otherSource: $('#OtherSource_').val(),
            dependents: $('#Dependents_').val(),
            dependentName: $('#DependentName_').val(),
            dependentAge: $('#DependentAge_').val(),
            relationship: $('#Relationship_').val()
        };
       
        if ($('#TxtStat').text().trim() == 'New') {
            $.ajax({
                type: 'POST',
                url: '/Members/Create',
                data: formData,
                success: function (response) {
                    alert('Form submitted successfully!');
                    window.location.href = '/Members/Index';

                    // Handle success response
                },
                error: function (xhr, status, error) {
                    alert('Error submitting form: ' + error);
                    // Handle error response
                }
            });

        }

        else {
    
            $.ajax({
                type: 'POST',
                url: '/Members/Update',
                data: formData,
                success: function (response) {
                    alert('Form Updated successfully!');
                    window.location.href = '/Members/Profile_?No_=' + No_;

                    // Handle success response
                },
                error: function (xhr, status, error) {
                    alert('Error submitting form: ' + error);
                    // Handle error response
                }
            });
        }
    });
});




$(document).ready(function () {
    $('#BtnSendforRegistration').click(function (e) {

        e.preventDefault(); // Prevent the default button action
        var confirmation = confirm("Are you sure you want to POST this member?");
        if (confirmation) {

            $('#FullName_Modal').val($("#h3FullName").text().trim());
            $('#TransactionModal').modal('show');

        }
    });
});



