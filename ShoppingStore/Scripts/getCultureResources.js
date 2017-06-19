
window.Resources = {};

$.ajax({
    url: "resources/GetResources",
    type: "GET",
    contentType: "application/json",
    dataType: "json",
}).done(function (data) {
    localStorage.setItem("resources", data);
    Resources = JSON.parse(localStorage.getItem("resources"));
})


