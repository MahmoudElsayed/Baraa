function DisplayUserMessage(data) {
    toastr[data.ResultType](data.Message);

    if (data.ResultType === "success") {
        $("#SpanMessage").html(data.Message)
        $("#DivSpanMessage").slideDown(800);
        setTimeout(function () {
            location.reload();
        }, 6000)
    }
}

function chhoseHijriID() {
    $("#DivOUserIDExpiryDateChrist").hide("fast");
    $("#DivOUserIDExpiryDateHijri").show("fast");
}
function chhoseChristID() {
    $("#DivOUserIDExpiryDateHijri").hide("fast");
    $("#DivOUserIDExpiryDateChrist").show("fast");
}
function chhoseHijriPassport() {
    $("#DivOUserPassportExpiryDateChrist").hide("fast");
    $("#DivOUserPassportExpiryDateHijri").show("fast");

}
function chhoseChristPassport() {
    $("#DivOUserPassportExpiryDateHijri").hide("fast");
    $("#DivOUserPassportExpiryDateChrist").show("fast");

}

$(document).ready(function () {
    $('#OUserPassportExpiryDate').calendarsPicker({ calendar: $.calendars.instance('ummalqura', 'ar') });
    $('#OUserPassportExpiryDate').calendarsPicker('option', 'localNumbers', false);
    $('#OUserIDExpiryDate').calendarsPicker({ calendar: $.calendars.instance('ummalqura', 'ar') });
    $('#OUserIDExpiryDate').calendarsPicker('option', 'localNumbers', false);
    $(".calendars-rtl").css("width", "257px");
    $(".calendars-rtl").css("width", "257px");

 
    $("#OUserMobileNo").inputmask({ "mask": "999999999" });
});
function GetCountryFlag(e) {
    var drbVal = $("#countryCode").data("kendoDropDownList").value();
    if (drbVal != "") {

    $.ajax({ type: "Get", url: "/Operation/OperationUser/GetCountryFlag?CountryID=" + drbVal  }).done(function(data) {
        $("#DivFlag").slideDown();
        $("#flagImg").attr("src","../../"+ data.Flag);
    }).error(function(data) {
        $("#DivFlag").slideUp();

    })
    } else {
        $("#DivFlag").slideUp();
    }

}

function GetcountryID() {
    return {
        countryID: $("#countryCode").val()
    }
}
function GetCityID() {
    return {
         CityID: $("#CityID").data("kendoDropDownList").value(),
        text: $("#BlockID").data("kendoDropDownList").filterInput.val()
    }
}
function Next_tab_data() {
    $('#li_tab_Evidence').find('a').trigger('click');
    $('html, body').animate({
        scrollTop: $("#li_tab_Evidence").offset().top - 150
    }, 1000);
}
function GetCountry() {
    return {
        CountryID: $("#countryCode").data("kendoDropDownList").value(),
        text: $("#CityID").data("kendoDropDownList").filterInput.val()
    };
}

