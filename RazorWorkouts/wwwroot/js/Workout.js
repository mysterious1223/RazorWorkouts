function AddItem(id)
{
    //'/api/Workout?id ='+${

    var url = "/api/Workout?id=" + id;


    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                location.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}

function DeleteWorkoutSet(id) {
    //'/api/Workout?id ='+${

    var url = "/api/Workout?id=" + id;


    $.ajax({
        type: "DELETE",
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                location.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}