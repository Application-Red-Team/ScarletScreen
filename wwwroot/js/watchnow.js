// When the page is ready
$(document).ready(function () {
    // When the "Watch Now" button is clicked
    $("#watchNowBtn").click(function () {
        // Show the modal
        $("#streamingModal").css("display", "block");

        // Populate the modal with streaming service options (you can add this dynamically using JavaScript)
        var streamingServices = ["Netflix", "Amazon Prime", "Hulu", "Disney+", "HBO Max"];
        var streamingserviceList = $("#streamingserviceList");
        streamingServices.forEach(function (service) {
            var listItem = $("<li>").text(service);
            streamingserviceList.append(listItem);
        });
    });

    // When the user clicks on the close button or anywhere outside of the modal, close it
    $(".close, .modal").click(function () {
        $("#streamingModal").css("display", "none");
    });
});
