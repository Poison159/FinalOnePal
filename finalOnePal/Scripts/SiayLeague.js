function createChartQualifyVsDisqualify(qualify, disqualify) {

    $('#divChartOnlineVsOfflineAgents').empty();
    var newCanvas = $('<canvas/>', { 'id': 'ChartOnlineVsOfflineAgents' }).width(400).height(300);
    $('#divChartOnlineVsOfflineAgents').append(newCanvas);
    
    var ctx = document.getElementById("ChartOnlineVsOfflineAgents");
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ["Qualify", "DisQualify"],
            datasets: [{
                data: [qualify, disqualify],
                backgroundColor: [
                    'rgba(1, 170, 173,1)',
                    'rgba(255, 153, 0, 1)'
                ]
            }]
        },
        options: {
            maintainAspectRatio: true,
            animation: {
                duration: 1000
            },
            title: {
                display: true,
                text: 'Qualify VS DisQualify Teams'
            },
            legend:
            {
                display: true
            }
        }
    });
}

function RefreshDash(teams) {

    var qualify = 0;
    $.each(teams,
        function (i, item) {
            if (item.points > 10) {
                qualify++;
            }
        });
    var disQualify = 0;
    $.each(teams,
        function (i, item) {
            if (item.points <= 10) {
                disQualify++;
            }
        });

}