function GetProdGraphReadings() {
    var GraphPanel = $("#DealProdGraphPanel");
    var LinkCircle = '/Deal/GetProductivityGraph';
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: LinkCircle,
        data: {},
        success: function (data) {
            PrepareProGraph(GraphPanel, data);
        },
        failure: function (data) {
        }
    });
}
function PrepareProGraph(Ele, Readings) {
    var PCompleted = 0; var PNew = 0; var PWaiting = 0; var PCancelled = 0; var Productivity = 0;
    PCompleted = Readings[0].CompletedDeals;
    PNew = Readings[0].NewDeals;
    PWaiting = Readings[0].DealsWaiting;
    PCancelled = Readings[0].CancelledDeals;
    Productivity = Readings[0].Productivity;
    //var labels = new Array('Completed Deals', 'New Deals', 'Deals Waiting on Clients', 'Cancelled Deals', 'Productivity');
    //var data = new Array(PCompleted, PNew, PWaiting, PCancelled, Productivity);
    var labels = new Array('Completed Deals', 'New Deals', 'Deals Waiting on Clients', 'Cancelled Deals');
    var data = new Array(PCompleted, PNew, PWaiting, PCancelled);
    var myVar = $(Ele).polygonalGraphWidget(
            {
                labels: labels,
                grid: true,
                data: data,
                max_val: 99
            });
}
