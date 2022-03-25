
function GetMasterEmployees() {
    let data = {}
        $.ajax({
            type: "GET",
            url: "https://localhost:44300/api/employees/master",
            async: false,
            data: {}
        }).done((result) => {
            data = result;
        }).fail((e) => {
            console.log(e);
        })
    return data;
}

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
    series: [
        GetMasterEmployees().filter(a => a.gender == "Male").length,  // jumlah data yang gendernya laki
        GetMasterEmployees().filter(a => a.gender == "Female").length // jumlah data yang gendernya perempuan
    ],
    labels: ['Laki-Laki', 'Perempuan']
}

var pieChart = new ApexCharts(document.querySelector("#pieChartGender"), pieOptions);
pieChart.render();

// Cek Data
console.log(GetMasterEmployees().filter(a => a.universityName == "Universitas Indonesia")[0].universityName)