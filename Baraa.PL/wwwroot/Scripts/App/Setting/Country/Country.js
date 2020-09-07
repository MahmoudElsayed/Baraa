var row;
var dataItem;
var grid;
var wnd;
function onDataBound() {
    $(".remove-btn").removeClass('k-button');
    $(".remove-btn span").removeClass('k-icon k-edit');
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
        var grid = $("#Country").data("kendoGrid");
        grid.bind("dataBinding", function (e) {
            if (e.action == "sync") {
                e.preventDefault();
                $("#CountryNameAR").val('');
                $("#CountryNameEN").val('');
                $("#Extension").val('');
                $("#FlagImg").val('');
                $(".k-icon.k-i-close.k-delete").click();      
            }
        })

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
        var grid = $("#Country").data("kendoGrid");
        grid.cancelChanges();
    }
}
function onUpload(e) {
    var files = e.files;

    $.each(files, function () {
        var ext = [".jpeg", ".jpg", ".gif", ".png", ".bmp"];
        if (ext.indexOf(this.extension.toLowerCase()) === -1) {
            toastr.error("هذه الصيغة غير مدعمة");
            e.preventDefault();
        }

        if (this.size / 1024 / 1024 > 2) {
            toastr.error("الحد الاقصى المسموح به 2 ميجابايت");
            e.preventDefault();
        }
    });
}
function onSuccess(e) {
    if (e.operation === "upload") {

        $("#FlagImg").val(e.response.file); // Bind Uploaded Image Name to EmpViewModel.EmpImage

    }
    else if (e.operation === "remove") {
        $("#FlagImg").val('');
    }
}
function onError(e) {
    if (e.operation === "upload") {
        toastr.error("حدث خطأ أثناء رفع الصورة");
    }
    else if (e.operation === "remove") {
        toastr.error("حدث خطأ أثناء حذف الصورة");
    }
}
function customgrid(e) {
    if (e.model.isNew()) {
        $(".remove-btn").removeClass('fa fa-edit');
        $('.k-window-title').text("Add New Country");
    } else {
        $(".remove-btn").removeClass('fa fa-edit');
        $('.k-window-title').text("Update Country");
    }
}
function GetImg() {
    return { FlagImg: $("#FlagImg").val() }
}

function SaveChanges() {
    var grid = $("#Country").data("kendoGrid");
    grid.dataSource.read();
}