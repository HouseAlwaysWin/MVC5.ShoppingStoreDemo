
window.Resources = JSON.parse(localStorage.getItem("resources"));

$.ajax({
    url: "api/resources/GetResources",
    type: "GET",
    contentType: "application/json",
    dataType: "json"
}).done(function (data) {
    localStorage.setItem("resources", data);
    Resources = JSON.parse(localStorage.getItem("resources"));
});


