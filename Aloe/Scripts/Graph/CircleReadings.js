//** Circle Readings

function GetCircleReadings() {
    var GraphPanel = $("#CircleReadings");
    var LinkCircle = '/Deal/GetCircleReadings';
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: LinkCircle,
        data: {},
        success: function (data) {
            PrepareGraph(GraphPanel, data);
        },
        failure: function (data) {
        }
    });
}
function PrepareGraph(Ele, Readings) {
    var html = "";
   $(Ele).html('');
    var Clients = 0; var Completed = 0;
    Clients = Readings[0].Clients;
    Completed = Readings[0].Completed;
    html = '<span class="chartC" data-percent=' + Clients + ' id="Ct1"> <span class="percentC"></span> </span>';
    html += '<span class="chartCO" data-percent=' + Completed + ' id="Ct2"> <span class="percentCO"></span> </span>';
   $(Ele).html(html);
     var ProjectClients = document.querySelector("#Ct1");
     var ProjClientOptions = { easing: 'easeOutElastic', delay: 3000, barColor: '#11972c', trackColor: '#eef2f3', scaleColor: false, lineWidth: 7, trackWidth:7, lineCap: 'round', onStep: function (from, to, per) { this.el.children[0].innerHTML = Math.round(per); } }
     var PClientchart = new EasyPieChart(ProjectClients, ProjClientOptions);
     var ProjectCompleted = document.querySelector("#Ct2");
     var ProjCompletedOptions = { easing: 'easeOutElastic', delay: 3000, barColor: '#fc783f', trackColor: '#eef2f3', scaleColor: false, lineWidth: 7, trackWidth: 7, lineCap: 'round', onStep: function (from, to, per) { this.el.children[0].innerHTML = Math.round(per); } }
     var PCompletedchart = new EasyPieChart(ProjectCompleted, ProjCompletedOptions);
 }
