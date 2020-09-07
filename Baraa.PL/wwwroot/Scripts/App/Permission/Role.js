var row;
var dataItem;
var grid;
var wnd;
//var loading = new KTDialog({ 'type': 'loader', 'placement': 'top center', 'message': 'برجاء الانتظار جارى تحميل البيانات ... <img style="margin-right: 2%;" src="../../Kendo_New/css/kendo/Uniform/loading.gif" />' });
//loading.show();
//setTimeout(function () {
//    loading.hide();
//}, 1000);
function DeleteRole() {
    $.ajax({
        url: '/Permission/Role/DeleteRole?roleID=' + dataItem.Id,
        success: function (data) {
            if (data === "Done") {
                grid.removeRow(row);
                toastr["success"]("<div style='text-align:left;'>Deleted Successfully</div>");
            }
            else {
                if (!data.includes('<html')) {
                    toastr["error"](data);
                }
                else {
                    toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Update</div>");
                }
            }
        }
    });
}

function onRequestEnd(e) {

    if (e.type === "update") {
        if (e.response.Errors !== undefined) {
            if (e.response.Errors === null) {
                toastr["success"]("<div style='text-align:left;'>Updated Successfully</div>");
                this.read();
            }
        }
        else {
            toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Update</div>");
            this.read();
        }
    }
    else if (e.type === "create") {
        if (e.response.Errors !== undefined) {
            if (e.response.Errors === null) {
                toastr["success"]("<div style='text-align:left;'>Added Successfully</div>");
                this.read();
            }
        }
        else {
            toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Insert</div>");
            this.read();
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

function onDataBound() {
   

    $(".remove-btn").removeClass('k-button');
    $(".remove-btn span").removeClass('k-icon k-edit');
    
    $.ajax({
        url: '/Permission/Permissions/GetPermissions?controllerName=' + 'Role',
        success: function (data) {

            //if (!data.Insert) {
            //    $("#Roles .k-grid-toolbar").remove();
            //}

            if (!data.Update) {
                $("#Roles tbody tr .k-grid-edit").each(function () {
                    $(this).remove();
                })
            }

            if (!data.Delete) {
                $("#Roles tbody tr .k-grid-Destroy").each(function () {
                    $(this).remove();
                })
            }
        }
    });
    $("#Roles tbody tr .k-grid-Destroy").each(function () {
        var currentDataItem = $("#Roles").data("kendoGrid").dataItem($(this).closest("tr"));

        if (currentDataItem.Id == 1 || currentDataItem.Id == 2 || currentDataItem.Id == 3 || currentDataItem.Id == 4 || currentDataItem.Id == 5 || currentDataItem.Id == 6 || currentDataItem.Id == 7) {
            $(this).remove();
        }

    })
}