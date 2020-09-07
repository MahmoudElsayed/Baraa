var row;
var dataItem;
var grid;
var wnd;
function onDataBound() {
    $(".remove-btn").removeClass('k-button');
    $(".remove-btn span").removeClass('k-icon k-edit');
    //$.ajax({
    //    url: '/HR/General/GetPermissions?controllerName=' + 'Country',
    //    success: function (data) {

    //        if (!data.Insert) {
    //            $("#Countries .k-grid-toolbar").remove();
    //        }

    //        if (!data.Update) {
    //            $("#Countries tbody tr .k-grid-edit").each(function () {
    //                $(this).remove();
    //            })
    //        }

    //        if (!data.Delete) {
    //            $("#Countries tbody tr .k-grid-Destroy").each(function () {
    //                $(this).remove();
    //            })
    //        }
    //    }
    //});

    //$("#Countries tbody tr .k-grid-edit").each(function () {
    //    var currentDataItem = $("#Countries").data("kendoGrid").dataItem($(this).closest("tr"));
    //    if (currentDataItem.CountryID === 1) {
    //        $(this).remove();
    //    }
    //})

    //$("#Countries tbody tr .k-grid-Destroy").each(function () {
    //    var currentDataItem = $("#Countries").data("kendoGrid").dataItem($(this).closest("tr"));
    //    if (currentDataItem.CountryID === 1) {
    //        $(this).remove();
    //    }
    //})
}

function onRequestEnd(e) {
    debugger;
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

        var grid = $("#Agent").data("kendoGrid");
        grid.bind("dataBinding",
            function(e) {
                if (e.action == "sync") {
                    e.preventDefault();
                    $("#AgentNameAR").val('');
                    $("#AgentNameEN").val('');

                    $(".k-icon.k-i-close.k-delete").click();
                }
            });

        if (e.response.Errors !== undefined) {
            if (e.response.Errors === null) {
                toastr["success"]("<div style='text-align:left;'>Added Successfully</div>");
                // this.read();
            }
        }
        else {
            toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Insert</div>");
            //  this.read();
        }
    }
    else if (e.type === "destroy") {
        if (e.response.Errors !== undefined) {
            if (e.response.Errors === null) {
                toastr["success"]("<div style='text-align:left;'>Deleted Successfully</div>");
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
        var grid = $("#Agent").data("kendoGrid");
        grid.cancelChanges();
    }
}
function customgrid(e) {
    if (e.model.isNew()) {
        $(".remove-btn").removeClass('fa fa-edit');
        $('.k-window-title').text("Add New Agent");
    } else {
        $(".remove-btn").removeClass('fa fa-edit');
        $('.k-window-title').text("Update Agent");
    }
}

function SaveChanges() {
    var grid = $("#Agent").data("kendoGrid");
    grid.dataSource.read();
}