
 
$(function () {
    function handleLogin() {
        if ($('#username').val() === "") {
            alert('Please input Username');
            $('#username').focus();
            $('#username').select();
            return false;
        } else if ($('#password').val() === "") {
            alert('Please input Password');
            $('#password').focus();
            $('#password').select();
            return false;
        }

        var AccountData = {
            Username: $('#username').val(),
            Password: $('#password').val(),
        };

        // Send data to the server using AJAX
        $.ajax({
            type: 'POST',
            url: '/Home/Login', // Replace with your server URL
            data: AccountData,
            success: function (response) {
                window.location.href = '/Home/Admin_Dashboard';
            },
            error: function (xhr, status, error) {
                alert('Incorrect Username or Password or your Account is INACTIVE please contact Administrator ');
            }
        });
    }

    $('#BtnLogin').click(handleLogin);

    // Handle Enter key press for username and password fields
    $('#username, #password').keypress(function (e) {
        if (e.which === 13) { // Enter key code
            e.preventDefault(); // Prevent default form submission
            handleLogin(); // Call the login function
        }
    });
});





$(function () {
    $("#BtnAddNewUser").click(function (event) {
        event.preventDefault();

        // Create FormData object
        var formDataUser_ = new FormData();

        formDataUser_.append("EntryBy", $("#EntryBy").text());
        formDataUser_.append("Name", $("#FULLNAME_").val().trim());
        formDataUser_.append("Username", $("#USERNAME_").val().trim());
        formDataUser_.append("Password", $("#PASSWORD_").val().trim());
        formDataUser_.append("Usertype", $("#USERTYPE_").val().trim());
        formDataUser_.append("Status", $("#STATUS_").val().trim());

        //formDataUser_.append("Department", $("#DEPARTMENT_").val().trim());

        $.ajax({
            type: "POST",
            url: "/Account/Create",
            processData: false, // Prevent jQuery from processing the data
            contentType: false, // Set content type to false for FormData
            data: formDataUser_,
            success: function () {
                alert('Successfully Added');
                $("#AddNewUser").modal("hide");
                $("#NewUserForm")[0].reset();
                // Reload the page and pass the active tab information
                window.location.href = window.location.pathname + "?activeTab=profile";
            },
            error: function (xhr, status, error) {
                alert('Error occurred: ' + error);
            }
        });
    });
});

$(document).ready(function () {
    // Get the query parameter value
    var urlParams = new URLSearchParams(window.location.search);
    var activeTab = urlParams.get('activeTab');

    // Set the active tab based on the query parameter
    if (activeTab === 'profile') {
        $('#nav-home-tab').removeClass('active');
        $('#nav-home').removeClass('active show');
        $('#nav-profile-tab').addClass('active');
        $('#nav-profile').addClass('active show');
    }
});


$(document).ready(function () {
    $('.edit-account').click(function (e) {
        e.preventDefault(); // Prevent default link behavior

        // Retrieve the account data from the data attribute
        var info = $(this).data('info');
        var dataArray = info.split('||');

        // Extract individual values
        var _no = dataArray[0].trim();
        var _username = dataArray[1].trim();
        var _name = dataArray[2].trim();
        var _pword = dataArray[3].trim();
        var _usertype = dataArray[4].trim();
        var _status = dataArray[5].trim();

        // Set the values in the modal
        $('#eUserNo_').val(_no.trim());
        $('#eUserName').val(_username.trim());
        $('#eFullName').val(_name.trim());
        $('#ePassword').val(_pword.trim());
        $('#eUSERTYPE_').val(_usertype.trim());
        $('#eSTATUS_').val(_status.trim());


        // Show the modal
        $('#EditNewUser').modal('show');
    });
});


$(function () {
    $("#BtnEditAccount").click(function (event) {
        event.preventDefault();

        // EDIT FormData object

        var EditUserFormData = new FormData();
        EditUserFormData.append("EntryBy", $("#EntryBy").text());
        EditUserFormData.append("No_", $('#eUserNo_').val());
        EditUserFormData.append("Username", $('#eUserName').val());
        EditUserFormData.append("Name", $('#eFullName').val());
        EditUserFormData.append("Password", $('#ePassword').val());
        EditUserFormData.append("Usertype", $('#eUSERTYPE_').val());
        EditUserFormData.append("Status", $('#eSTATUS_').val());

        $.ajax({
            type: "POST",
            url: "/Account/Update",
            processData: false, // Prevent jQuery from processing the data
            contentType: false, // Set content type to false for FormData
            data: EditUserFormData,
            success: function () {
                alert('Successfully Added');
                $("#EditNewUser").modal("hide");
                $("#UserFormEdit")[0].reset();
                // Reload the page and pass the active tab information
                window.location.href = window.location.pathname + "?activeTab=profile";
            },
            error: function (xhr, status, error) {
                alert('Error occurred: ' + error);
            }
        });

    });
});


$(document).ready(function () {
    $('.delete-button-account').click(function (e) {
        e.preventDefault(); // Prevent default link behavior

        var UserNo_ = $(this).data('no'); // Get the value of data-no attribute
        console.log('No_ value:', UserNo_); // Log the value to verify


        if (confirm("Are you sure you want to delete?")) {
            // Send an AJAX request to your controller
            $.ajax({
                url: '/Account/Delete',
                type: 'POST', // Or 'GET' depending on your controller action
                data: { "No_": UserNo_ },
                success: function (response) {
                    // Handle success response
                    alert('Account deleted successfully');
                    window.location.href = window.location.pathname + "?activeTab=profile";
                    // Optionally, you can redirect or update the UI here
                },
                error: function (xhr, status, error) {
                    // Handle error
                    console.error('Error deleting item:', error);
                }
            });

        }
    });
});