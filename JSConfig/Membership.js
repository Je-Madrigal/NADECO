 

$(function () {
    $('#showHistory').click(function () {

        $('#DivHistory').toggle();
    });
});

const startYear = new Date().getFullYear();
const endYear = startYear + 10; // Adjusts endYear to 10 years from the current year
const dropdown = document.getElementById('yearDropdown');

for (let year = startYear; year <= endYear; year++) {
    const option = document.createElement('option');
    option.value = year;
    option.textContent = year;
    dropdown.appendChild(option);
}


