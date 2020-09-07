var row;
var dataItem;
var grid;
var wnd;
var TempInterval;
var TempIntervalCount;

$(document).ready(function () {

    // Begin EmpSearch Partial View Functions    

    $("#EmpSearchMenu").click(function () {
        ClearEmpData();
        $("#RolesDiv").slideUp("fast");
    });

    $("#BtnSearchByName").click(function () {
        GetEmpByName($('#EmpName').val());
        LoadUserRoles();
        return false;
    });

    $("#BtnSearchByID").click(function () {
        GetEmpByID($('#EmpID').val());
        LoadUserRoles();
        return false;
    });

    $("#BtnSearchByNationalID").click(function () {
        GetEmpByNationalId($('#NationalID').val());
        LoadUserRoles();
        return false;
    });

    $("#BtnSearchByCode").click(function () {
        GetEmpByCode($('#Code').val());
        LoadUserRoles();
        return false;
    });

    // End EmpSearch Partial View Functions    


});

function DeleteUserRole() {
    $.ajax({
        url: '/Permission/Permissions/UserRoleDelete?roleName=' + dataItem.RoleName + '&userID=' + dataItem.UserID,
        success: function (data) {
            if (data === "Done") {
                grid.removeRow(row);
                toastr["success"]("<div style='text-align:left;'>Deleted Successfully</div>");
            }
            else {
                toastr["error"](data);
            }
        }
    });
}
function onRequestEnd(e) {
    if (e.type === "create") {
        if (e.response.Errors === null) {
            toastr["success"]("<div style='text-align:left;'>Added Successfully</div>");
            this.read();
        }
    }
    else if (e.type === "destroy") {
        if (e.response.Errors === null) {
            toastr["success"]("<div style='text-align:left;'>Deleted Successfully</div>");
        }
    }
}

function error_handler(e) {
    if (e.errors) {
        var message = "";
        // Create a message containing all errors.
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += "<br/>" + this;
                });
            }
        });
        // Display the message
        toastr["error"](message.substr(5, message.length - 5));
        // Cancel the changes
        var grid = $("#Roles").data("kendoGrid");
        grid.cancelChanges();
    }
}

function customgrid(e) {
    if (e.model.isNew()) {
        $('.k-window-title').text("Add New Role");
    } else {
        $('.k-window-title').text("Update Role");
    }
}

function dataBound() {
    $(".remove-btn").removeClass('k-button');
    this.table.find(".k-grid-edit").hide();
}

function SetUserIDInModel(e) {
    e.model.set("UserID", Uservalues);
}

function GetUserID() {
    return {
        UserId: Uservalues
    };
}
function changeUserID() {
    $("#Roles").data('kendoGrid').dataSource.read();
    $("#Roles").data('kendoGrid').refresh();
}
var Uservalues = "";
$(document).ready(function () {
    $('#kt_selectUser').on('change', function () {
        Uservalues = "";
        var $selectedOptions = $(this).find('option:selected');
        $selectedOptions.each(function () {
            Uservalues = $(this).val();
        });
        changeUserID();
    });
});
function CancelFilter() {
    var $selectedOptions2 = $(".kt-select2").find('option:selected');
    $selectedOptions2.each(function (w) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    Uservalues = "";
    changeUserID();
}