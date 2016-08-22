//** Loading Messages
$(document).ready(function () {
    //** Deal Messages __________________
    $("#DmDDid").val('-');
    $("#DmSendStatus").html('');
    var DealId = getUrlParameter('DealID'); //'@Request.Params["DealID"]';
    if (DealId != null && DealId != "")
    ViewMessages(DealId);
    Loadreceiver(DealId);
    //===================================
    //** Deal History ___________________
    GetHistory(DealId);
    //===================================
});

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        var key = decodeURIComponent(sParameterName[0]);
        var value = decodeURIComponent(sParameterName[1]);

        if (key === sParam) {
            return value === undefined ? true : value;
        }
    }
};


//*******************