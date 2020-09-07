var OperationUservalues = "";
function yesConfirm() {
    var d = "";
    if ($("#AgentListID").val() != null) {
        d = $("#AgentListID").val().toString();
    }
    $.ajax({
        url: '/Setting/OperationAgent/AddUpdateOperationAgent',
        data: {
            OUserID: OperationUservalues, AgentCheck: d
        },
        success: function (data) {
            if (data.ResultType === "success") {
                toastr.success(data.Message);
                //$("#Agent").data('kendoGrid').dataSource.read();
                //$("#Agent").data('kendoGrid').refresh();
                $("#AgentCheck").val("");
                $("#btn_ConfirmCreate").hide();
                $("#dev_danger").hide();
                $("#dev_success").html("<strong>" + data.Message + "</strong>");
                $("#dev_success").show();
                GetSelectAgent();
            }
            else {
                if (data.ResultType === "error") {
                    toastr.error(data.Message);
                    $("#dev_success").hide();
                    $("#dev_danger").html("<strong>" + data.Message + "</strong>");
                    $("#dev_danger").show();
                }
                else {
                    toastr["error"]("<div style='text-align:left;'>You Are Not Authorized To Add</div>");
                    $("#dev_success").hide();
                    $("#dev_danger").html("<strong>You Are Not Authorized To Add</strong>");
                    $("#dev_danger").show();
                }
            }
        }
    });
   // wndConfirm.close();
};
function GetOperation() {
    return {
        OUserID: OperationUservalues,
        text: ""/*$("#AgentListID").data("kendoMultiSelect").input.val()*/
    };
}
//function GetSelectedItems() {
//    AgentCheck = "";
//    $("table tbody").find('tr').each(
//        function () {
//            var id = $(this).find('#hfID').val();
            
//            var IsAdd = $(this).hasClass('k-state-selected');
//            if (IsAdd == true) {
//                AgentCheck = AgentCheck + id + ',';
//            }
//        }
//    );
//    $("#AgentCheck").val(AgentCheck);
//    if (OperationUservalues != "" && OperationUservalues != "null") {
//            $("#btn_ConfirmCreate").show();
//    }
//    else {
//        $("#btn_ConfirmCreate").hide();
//    }
//}
//function check(e) {
//    var state = $(e).is(':checked');
//    if (state === true) {
//        $(e).closest("tr").removeClass("k-state-selected");
//        $(e).prop('checked', true);
//        $(e).closest("tr").toggleClass("k-state-selected");
//    }
//    else {
//        $(e).not(":disabled").prop('checked', false);
//        $(e).not(":disabled").closest("tr").toggleClass("k-state-selected");
//        $(e).not(":disabled").closest("tr").removeClass("k-state-selected");
//    }
//    GetSelectedItems();
//}
//function checkAll(ele) {
//    $('.checkbox').prop('checked', false);
//    var state = $(ele).is(':checked');
//    var grid = $('#grid').data('kendoGrid');
//    if (state == true) {
//        $('.checkbox').closest("tr").removeClass("k-state-selected");
//        $('.checkbox').prop('checked', true);
//        $('.checkbox').closest("tr").toggleClass("k-state-selected");
//    }
//    else {
//        $('.checkbox').prop('checked', false);
//        $('.checkbox').closest("tr").toggleClass("k-state-selected");
//        $('.checkbox').closest("tr").removeClass("k-state-selected");
//    }
//    GetSelectedItems();
//};
function ChangeAgentList() {
    if ($("#AgentListID").val() != "") {
        $("#btn_ConfirmCreate").show();
    }
    else {
        $("#btn_ConfirmCreate").hide();
    }
}
function GetSelectAgent() {
    if (OperationUservalues !== "" && OperationUservalues != "null") {
        //$('.checkbox').prop('checked', false);
        //$('.checkbox').closest("tr").toggleClass("k-state-selected");
        //$('.checkbox').closest("tr").removeClass("k-state-selected");
        $.ajax({
            url: '/Setting/OperationAgent/GetAgentIntByOUser',
            data: {
                OUserID: OperationUservalues,
            },
            success: function (data) {
                if (data != "") {
                    $("#btn_ConfirmCreate").show();
                    $("#AgentListID").data("kendoMultiSelect").value(data);
                }
                else {
                    $("#btn_ConfirmCreate").hide();
                    $("#AgentListID").data("kendoMultiSelect").value("");
                }
            }
        });
    }
    else {
        $("#btn_ConfirmCreate").hide();
        $("#AgentListID").data("kendoMultiSelect").value("");
    }
    //GetSelectedItems();
}
//function CheckAgentByOUser(data) {
//    $(data).each(function () {
//        $('input:checkbox[value="' + this.AgentID + '"]').prop('checked', true);
//    });
//}
$(document).ready(function () {
    $('#kt_selectOperationUser').on('change', function () {
        OperationUservalues = "";
        var $selectedOptions = $(this).find('option:selected');
        $selectedOptions.each(function () {
            OperationUservalues = $(this).val();
        });
        GetSelectAgent();
    });
});