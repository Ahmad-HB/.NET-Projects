$(document).ready(function () {
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});

function DeleteUser(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.value) {
            window.location.href = `/Admin/Accounts/DeleteUser?id=${id}`;
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}

Edit = (id, name, email, imageuser, activeuser, rolename, PathImageUser) => {
    document.getElementById("title").innerHTML = "Edit User";
    document.getElementById("btnSave").value = "Edit";
    document.getElementById("userId").value = id;
    document.getElementById("userName").value = name;
    document.getElementById("userEmail").value = email;
    document.getElementById("userRoleName").value = rolename;
    var active = document.getElementById("userActive")
    if (active) {  // Ensure the element exists
        if (active.checked === true) {
            active.checked = false;  // No need to call it as a function
        } else {
            active.checked = true;
        }
    }
    // `/Images/Users/`
    $('#grPassword').hide();
    $('#grcomPassword').hide();
    document.getElementById("userPassword").value = "$$$$$$$$";
    document.getElementById("userCompare").value = "$$$$$$$$";
    document.getElementById("image").hidden = false;
    document.getElementById("image").src = PathImageUser;
    document.getElementById("ImageHide").value = imageuser;

}

Reset = () => {
    document.getElementById("title").innerHTML = "Edit User";
    document.getElementById("btnSave").value = "save";
    document.getElementById("userId").value = "";
    document.getElementById("userName").value = "";
    document.getElementById("userEmail").value = "";
    document.getElementById("userRoleName").value = "";
    document.getElementById("userActive").checked = false;
    $('#grPassword').show();
    $('#grcomPassword').show();
    document.getElementById("userPassword").value = "";
    document.getElementById("userCompare").value = "";
    document.getElementById("image").hidden = true;
}

function ChangePassword(id) {
    document.getElementById("userPassId").value = id;
}