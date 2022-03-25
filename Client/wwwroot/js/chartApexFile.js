
// Line Chart
let barOptions = {
    chart: {
        type: 'bar'
    },
    plotOptions: {
        bar: {
            horizontal: false
        }
    },
    series: [{
        data: [{
            x: 'UGM',
            y: 10
        }, {
            x: 'UI',
            y: 18,
            fillColor: '#EB8C87',
            strokeColor: '#000'
        }, {
            x: 'ITB',
            y: 13
        }]
    }]
}

var barChart = new ApexCharts(document.querySelector("#barChartUniv"), barOptions);
barChart.render();

// Pie Chart Jenis Kelamin
let pieOptions = {
    chart: {
        type: 'donut'
    },
    plotOptions: {
        pie: {
            donut: {
                size: '65%',
            }
        }
    },
    series: [21, 23],
    labels: ['Laki-Laki', 'Perempuan']
}

var pieChart = new ApexCharts(document.querySelector("#pieChartGender"), pieOptions);
pieChart.render();