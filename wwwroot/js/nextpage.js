var pageNumber = 1; // Keep track of the current page number

$('#nextButton').on('click', function () {
    // Increment the page number
    pageNumber++;

    // Make an AJAX request to fetch the next set of movie data
    $.ajax({
        url: '@Url.Action("GetNextMovies", "Movie")', // Replace with your controller action
        type: 'GET',
        data: { page: pageNumber }, // Send the page number as a parameter
        success: function (data) {
            // Append the new movies to the existing movies
            $('.row').append(data); // Assuming data contains HTML for new movies
        },
        error: function (error) {
            console.log(error);
        }
    });
});