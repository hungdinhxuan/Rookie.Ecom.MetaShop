// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const handleAddToCart = (isAuthenticated) => {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    });

    if (!isAuthenticated) {
        swalWithBootstrapButtons.fire({
            title: 'Error!',
            text: 'You need to login to continue',
            icon: 'error',
            showCancelButton: true,
            confirmButtonText: 'Yes, Login  Now',
            cancelButtonText: 'No, cancel!',
            reverseButtons: false
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = "/Auth/Login"
            }
        });
    } else {

    }

}


