$(document).ready(function () {
    $("button[data-toggle=\"ajax-modal\"]").click(function (event) {
        var url = $(this).data("url");
        $.get(url).done(function (data) {
            $("#modal-placeholder").html(data);
            showModal();
        })
    });

    $(".close-modal").click(function () {
        closeModal();
    });

    $('.datepicker').datepicker();
    $("input[type='number']").inputSpinner();
});


function showModal() {
    $("#modal-placeholder").removeClass("hidden");
    var htmlPage = document.getElementsByTagName("html")[0];
    htmlPage.setAttribute("style", "overflow-y: hidden;");
}

function closeModal() {
    $("#modal-placeholder").addClass("hidden");
    $("#modal-placeholder").empty();
    var htmlPage = document.getElementsByTagName("html")[0];
    htmlPage.setAttribute("style", "overflow-y: scroll;");
}