
$('a.btn-danger').on('click', ConfirmOnDelete);

function ConfirmOnDelete() {
    if (confirm("Are you sure?") == true)
        return true;
    else
        return false;
}