var Id;
function ActiveUser() {
    $.ajax({ type: "GET", url: "/Operation/OperationUser/ActiveUser?OUserID=" + Id }).done(function (data) {
        if (data.ResultType == "success") {
            toastr[data.ResultType](data.Message);
            RefreshTable();
        }
    }).error(function () {
        RefreshTable();
    });
}
function banUser() {
    $.ajax({ type: "GET", url: "/Operation/OperationUser/banUser?OUserID=" + Id }).done(function (data) {
        if (data.ResultType == "success") {

            toastr[data.ResultType](data.Message);
            RefreshTable();
        }
    }).error(function () {

        RefreshTable();
    });
}
function DeleteUser() {
    $.ajax({ type: "GET", url: "/Operation/OperationUser/DeleteOperationUser?OUserID=" + Id }).done(function (data) {
        if (data == "Done") {

            toastr['success']("Deleted Successfully");
            RefreshTable();
        }
    }).error(function () {
        RefreshTable();
    });

}

function DetailsUser(guid) {
    location.href = "/Operation/OperationUser/ViewDetails_One?OUserID=" + guid;
}
function OpenUpdate(guid) {
    location.href = "/Operation/OperationUser/Update_One?OUserGu=" + guid;
}
function RefreshTable() {
    $('#AWBTable').load("/Operation/OperationUser/ViewOperationUserTable");
}
 