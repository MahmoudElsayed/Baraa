function onDataBound() {
    $(".remove-btn").removeClass('k-button');
    $(".remove-btn span").removeClass('k-icon k-edit');
}
function onRequestEnd(e) {
    if (e.type === "update") {
        if (e.response.Errors !== undefined) {
            if (e.response.Errors === null) {
                toastr["success"]("<div style='text-align:left;'>Updated Successfully</div>");
                this.read();
                preventCloseOnSave = false;
            }
        }
        else {
            toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Update</div>");
            this.read();
        }
    }
    else if (e.type === "create") {
        var grid = $("#City").data("kendoGrid");
        grid.bind("dataBinding",
            function(e) {
                if (e.action === "sync") {
                    e.preventDefault();
                    $("#CityNameAR").val('');
                    $("#CityNameEN").val('');

                }
            });
        if (e.response !== undefined) {
            if (e.response === "Done") {
                toastr["success"]("<div style='text-align:left;'>Added Successfully</div>");
                // this.read();
            }
        }
        else {
            toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Insert</div>");
            this.read();
        }
    }
    else if (e.type === "destroy") {
        if (e.response.Errors !== undefined) {
            if (e.response.Errors === null) {
                toastr["success"]("<div style='text-align:left;'>Deleted Successfully</div>");
                //this.read();
            }
        }
        else {
            toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Deleted</div>");
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
        var grid = $("#City").data("kendoGrid");
        grid.cancelChanges();
    }
}
function customgrid(e) {
    if (e.model.isNew()) {
        $(".remove-btn").removeClass('fa fa-edit');
        $('.k-window-title').text("Add New City");
    } else {
        $(".remove-btn").removeClass('fa fa-edit');
        $('.k-window-title').text("Update City");
    }
}
function GetCountry() {
    return { CountryID: Countryvalues }
}
function RefreshCityGrid() {
    $("#City").data('kendoGrid').dataSource.read();
    $("#City").data('kendoGrid').refresh();
}
function SaveChanges() {
    var grid = $("#City").data("kendoGrid");
    grid.dataSource.read();
}
var Countryvalues = "";
$(document).ready(function () {
$('#kt_selectCountry').on('change', function () {
    Countryvalues = "";
    var $selectedOptions = $(this).find('option:selected');
    $selectedOptions.each(function () {
        Countryvalues = $(this).val();
    });
    RefreshCityGrid();
    });
});
function CancelFilter() {
    var $selectedOptions2 = $(".kt-select2").find('option:selected');
    $selectedOptions2.each(function (w) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    Countryvalues = "";
    RefreshCityGrid();
}