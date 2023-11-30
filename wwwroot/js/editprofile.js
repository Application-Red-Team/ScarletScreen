document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('profileForm');

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        // Get form data
        const formData = new FormData(form);

        // Example: Send data to a server using fetch
        fetch('https://your-backend-endpoint.com/update-profile', {
            method: 'POST',
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok.');
                }
                // Handle success response
                console.log('Profile updated successfully!');
            })
            .catch(error => {
                // Handle error
                console.error('There was a problem updating the profile:', error);
            });
    });
});
