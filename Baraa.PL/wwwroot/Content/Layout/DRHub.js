
$(function () {
    debugger;
    // signalr js code for start hub and send receive notification
    var newdrHub = $.connection.deleviertRHub;
    $.connection.hub.start().done(function () {
        console.log("%cDR hub started !", "color: green; font-family: sans-serif; font-weight: bolder;");
    });
    newdrHub.client.notify = function (message) {
        if (message && message.toLowerCase() === "requestnoti") {
            debugger;
            updateRequestNotification();
        }
    };


    //signalr method for push server message to client



});
$(document).ready(function () {
    updateRequestNotification();
});

function updateRequestNotification() {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Home/SystemAlert"
    }).done(function (data) {
        debugger;
        $("#Alert_Count").html(data.NotiCount);
        if (data.NotiCount > 0) {
            $("#Notinoti").show();
        } else {
            $("#Notinoti").hide();
        }
        //$("#Alert_MesCount").html(data.NotiCount);
        //$("#Alert_taskCount").html(data.NotiCount);
        $("#Alert_Data").html(data.MessageRes);
        $("#Table_Data").html(data.MessageTableRes);
        $("#marquee_Val").html(data.marqueeRes);

    });
    $.ajax({
        type: "GET",
        url: "/Home/GetAllCount"
    }).done(function (data) {
        $("#TotalDeliverd").html(data.TotalDeliverd + " AWB");
        $("#TotalReturn").html(data.TotalReturn + " AWB");
        $("#totalInwrehouse").html(data.totalInwrehouse + " AWB");
        $("#totalwithcpatain").html(data.totalwithcpatain + " AWB");
        $("#PendingPickupCount").html(data.PendingPickupCount + " AWB");
        $("#PickedUpCount").html(data.PickedUpCount + " AWB");
        $("#TotalCount").html(data.total + " AWB");

    });
}
    //newdrHub.client.notify = function (message) {
    //    debugger;
    //    if (message && message.toLowerCase() === "drupdated") {
    //        $("#updatestatus").slideDown();
    //        $("#updatestatusConteant").html("تم تحديث حاله الشحنات .. اكتشف الجديد <img src='../../../assets/media/icons/svg/Code/Loading.svg' />");
    //        setTimeout(function () {
    //            $("#updatestatus").slideUp();
    //            $("#updatestatusConteant").html("لا يوجد اى تحديث جديد");
    //        }, 5000);
    //        console.log('%DR hub added ..!', "color: blue");
    //        //var locName = $(location).attr('pathname');
    //        //if (locName === "/Client/Clients/ClientTrackingAWB_One" || locName === "/DR/DeliveryRequest/Index_One") {

    //        //}  

    //    }
    //};