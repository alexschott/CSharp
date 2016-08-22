 


// Message Modal
function ShowDMessageDialog() {
    //$("#DMDialog").modal('show');
    //$("#DmMessage").val('');
    //$('[data-dismiss="popover"]').popover('hide');
    $('.popover').removeClass('in');
    $('.popover').remove();
}
//****************************
function SendMessage(DID) {
    var Msg = $("#DmMessage").val();
    var DlID = DID;
    var Receiver = $("#DmReciever").val();
    var SendingMessage = '/Deal/SendMessage';
    if (Msg != "-" && Msg != "nil" && Msg!=null && Msg!="" && Msg!="\n") {
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: SendingMessage,
            data: { message: Msg, Did: DlID, receiver: Receiver },
            success: function (data) {
                $("#DmSendStatus").html("Message is sent");
                $("#DmSendStatus").attr("style", "color: green !important; opacity:1");
                $("#DmSendStatus").stop();   // reset fadeout
                $("#DmSendStatus").fadeOut(7000, function () {
                    $("#DmSendStatus").html("");
                });
                $("#DmMessage").val('');
            },
            failure: function (data) {
                $("#DmSendStatus").html("Invalid attempt.");
            }
        });
    }
    else {
       
        $("#DmSendStatus").html("Type message");
        $("#DmSendStatus").attr("style", "color: red !important");
        $("#DmSendStatus").stop();// reset fadeout
        $("#DmSendStatus").fadeOut(5000,function () {
            $("#DmSendStatus").html("");
        });
        if (Msg == "\n") $("#DmMessage").val('');

       // $("#DmSendStatus").fadeOut(4000);
       // setTimeout(function () { $("#DmSendStatus").html(""); }, 4000);
        //$("#DmSendStatus").hide().html("Please type message").fadeIn('slow').delay(3000).html("chiii");
    }
    
}
function ViewMessages(dlID)
{
    var MsgPanelHeader = $("#DMessagePanelHeader");
    var MsgPanelList = $("#DMessagelist");  
    var ViewingMessage = '/Deal/ShowMessage';
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: ViewingMessage,
        data: { Did: dlID },
        success: function (data) {
            prepareMsglist(MsgPanelHeader, MsgPanelList, data);
        },
        failure: function (data) {
        }
    });
   
}
function prepareMsglist(MsgHeader, MsgList, DmList)
{
    $(MsgHeader).html(''); $(MsgList).html('');
    var html = "";
    var MsgCount = 0; if (DmList.length > 0) MsgCount = DmList.length;
    var TClientCehck = $("#ClientT").val();
    html = 'Messages';
    if (MsgCount > 0) html += '<span>(' + MsgCount + ')</span>';
    if (TClientCehck=="False") {}
    else{
        html += '<h6><a href="#" class="new_message" onclick="ShowDMessageDialog();" id="DMsgBox" data-toggle="collapse" data-target="#demo">  New Message</a></h6>';
    }
    $(MsgHeader).html(html);
    html = "";
    $.each(DmList, function (i) {
        var msg = DmList[i].Message;
        if (msg.length > 27) msg = msg.substring(0, 28) + "..";
        var TTitle = 'From : ' + DmList[i].SenderName + '';
        html += '<a href="#"  class="read_message" data-container="body" data-toggle="popover" data-placement="left" data-content="' + DmList[i].Message + '" title="From: ' + DmList[i].SenderName + '">' + msg + '</a>';
        });
    $(MsgList).html(html);
    //  Bootstrap Popover/Model
    $.fn.extend({
        popoverClosable: function (options) {
            var defaults = {
                template:
                    '<div class="popover" >\
                     <div class="arrow"></div>\
                     <div class="popover-header">\
                     <a class="close" data-dismiss="popover" aria-hidden="true" style="margin: 5px 7px 0 0;">&times;</a>\
                     <h3 class="popover-title"></h3>\
                     </div>\
                     <div class="popover-content" style="word-wrap: break-word; min-width:161px;"></div>\
                     </div>'
            };

            options = $.extend({}, defaults, options);
            var $popover_togglers = this;
            $popover_togglers.popover(options);
            $popover_togglers.on('click', function (e) {
              
                    e.preventDefault();
                    $popover_togglers.not(this).popover('hide');
                //$(this).popoverClosable();
            });
            
            $('html').on('click', '[data-dismiss="popover"]', function (e) {
                $(this).parents('.popover').removeClass('in');
                $(this).parents('.popover').remove();
                //$popover_togglers.popover('hide');
            });
            
         
        }
         
    });
     $('[data-toggle="popover"]').popoverClosable();
    //$(function () {
    //    $('[data-toggle="popover"]').popoverClosable();
    //});
    //$('[data-toggle="popover"]').popover();
    var hideAllPopovers = function () {
        $("[data-toggle = 'popover']").each(function () {
            $(this).popover('hide');
        });
    };
}
function Loadreceiver(deID)
{
    var LoadRec = '/Deal/LoadReceiver';
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: LoadRec,
            data: { dlID: deID },
            success: function (data) {
                if (data != null) {
                    var htselection = "";
                    $.each(data, function (i) {
                        htselection += "<option value=" + data[i].ID + "> " + data[i].Receiver + "</option>";
                    });
                    $("#DmReciever").html(htselection);
                }
                else { $("#DmReciever") }
            },
            failure: function (data) {
            }
        });
}
function GetMessage(dmid)
{
     var DMsgBodyTitle = $("#DmModelTile").html('');
     var DMsgBody = $("#DmModelBody").html('');
    var ViewingMessage = '/Deal/GetDMessage';
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: ViewingMessage,
        data: { DmID: dmid },
        success: function (data) {
            if (data != null) {
                DMsgBodyTitle.html("From : " + data.SenderName);
                DMsgBody.html("<p>" + data.Message + "</p>")
                
                //return true;
            }
        },
        failure: function (data) {
        }
    });


    //$('#popupLeft').popover({
    //    html: true,
    //    content: function () {
    //        var content = $(this).data('content');
    //        try { content = $(content).html('fffffffff') } catch (e) {/* Ignore */ }
    //        return content;
    //    }
    //});
}

//****************  Deal History
function GetHistory(dlID) {
    var HistoryPanel = $("#DHistoryPanel"); 
    var ViewingHistory = '/Deal/GetDealHistory';  
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: ViewingHistory,
        data: { DlID: dlID},
        success: function (data) {
            prepareHistorylist(HistoryPanel, data);
        },
        failure: function (data) {
        }
    });
}
function prepareHistorylist(Ele, DHList)
{
    var html = "";
    $(Ele).html('');
    var MsgCount = 0; if (DHList.length > 0) MsgCount = DHList.length;
    if (MsgCount > 0) {
        //html = 'History <span>(' + MsgCount + ')</span>';
        html = 'History ';
        $.each(DHList, function (i) {
            html += '<a href="#" class="history_text">' + DHList[i].DHMsg + '</a>';
        });
        $(Ele).html(html);
    }
}
    
 










