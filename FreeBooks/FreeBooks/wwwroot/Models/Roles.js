$(document).ready(function () {
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});

function DeleteRole(id) {
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
            window.location.href = `/Admin/Accounts/DeleteRole?id=${id}`;
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}

Edit = (id,name) =>{
    document.getElementById("roleId").innerHTML = "Edit User";
    document.getElementById("btnSave").value = "Edit";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
    
}

Reset = () =>{
    document.getElementById("roleId").innerHTML = "Edit User";
    document.getElementById("btnSave").value = "save";
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
}