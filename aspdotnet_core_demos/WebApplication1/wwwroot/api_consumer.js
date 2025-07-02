function getEmps() {
    fetch("http://localhost:5142/api/Employees/GetAllEmps")
        .then(response => response.json())
        .then(data => {
            console.log(data)
        });
}
function getEmpsAJax() {
    $.ajax({
        url: "http://localhost:5142/api/Employees/GetAllEmps",
        type: "GET",
        success: function (response) {
            console.log(response);
        },
        error: function (errRes) {
            console.log("error:", errRes);
        }
    });
}