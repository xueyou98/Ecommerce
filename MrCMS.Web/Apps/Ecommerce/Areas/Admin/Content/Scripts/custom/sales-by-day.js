﻿$(document).ready(function () {

    $(window).resize(updateCharts);
    
    function updateCharts() {
        generateLineChart();
    };

    generateLineChart();
});

var lineChartData = {
    labels: [],
    datasets: [
        {
            fillColor: "rgba(52, 152, 219,0.5)",
            strokeColor: "rgba(52, 152, 219,1.0)",
            pointColor: "rgba(52, 152, 219,1.0)",
            pointStrokeColor: "rgba(52, 152, 219,1.0)",
            data: []
        },
        {
            fillColor: "rgba(43, 191, 105,0.5)",
            strokeColor: "rgba(43, 191, 105,1.0)",
            pointColor: "rgba(43, 191, 105,1.0)",
            pointStrokeColor: "rgba(43, 191, 105,1.0)",
            data: []
        },
        {
            fillColor: "rgba(231, 76, 60,0.5)",
            strokeColor: "rgba(231, 76, 60,1.0)",
            pointColor: "rgba(231, 76, 60,1.0)",
            pointStrokeColor: "rgba(231, 76, 60,1.0)",
            data: []
        }
    ]
};

var lineDataUrl = "/Admin/Apps/Ecommerce/Report/SalesByDay";
var lineChart = $("#line-chart").get(0).getContext("2d");
var lineChartContainer = $("#line-chart").parent();

function generateLineChart() {
    $.post(lineDataUrl, {
        from: $("#From").val(),
        to: $("#To").val()
    }, function (result) {
        lineChartData.labels = result.ChartLabels;
        $.each(result.MultiChartData, function (index, value) {
            lineChartData.datasets[index].data = value;
        });
        var nc = $("#line-chart").attr('width', $(lineChartContainer).width());
        new Chart(lineChart).Line(lineChartData, lineChartOptions);
    });
};