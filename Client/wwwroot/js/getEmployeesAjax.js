/*
$.ajax({
  type: "GET",
  url: "https://localhost:44300/api/employees/master",
  data: {},
})
  .done((result) => {
    let tableEmployees = ``;
    $.each(result, function (index, data) {
      tableEmployees += `<tr>
                            <th scope="row">${data.nik}</th>
                            <td>${data.fullName}</td>
                            <td>${data.phone}</td>
                            <td>${data.gender}</td>
                            <td>${data.email}</td>
                            <td>${data.birthDate}</td>
                            <td>${data.salary}</td>
                            <td>${data.universityName}</td>
                            <td>${data.degree}</td>
                            <td>${data.gpa}</td>
                        </tr>`;
    });

    $("#tableEmployees").html(tableEmployees);

    // cek log
    console.log(result);
  })
  .fail((e) => {
    console.log(e);
  });
  */

// Menampilkan Data Menggunakan DataTable
$(document).ready(function () {
    $("#tableEmployees").DataTable({
        dom: "Bftrip",
        buttons: [
            'pdf'
        ],
        "ajax": {
            "url": "https://localhost:44300/api/employees/master",
            "dataSrc": ""
        },
        "columns": [
            {
                render: function (data, type, row) {
                    return row.nik.substring(6);
                }
            },
            { "data": "nik" },
            { "data": "fullName" },
            {
                render: function (data, type, row) {
                    return `+62${row.phone.substring(1)}`
                }
            },
            { "data": "gender" },
            { "data": "email" },
            {
                render: function (data, type, row) {
                    return `Rp${row.salary.toString().substring(0, 1)}.${row.salary.toString().substring(1, 4)}.${row.salary.toString().substring(4)}`;
                }
            },
            { "data": "universityName" },
            { "data": "degree" },
            { "data": "gpa" },
            {
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-warning" data-toggle="modal" data-target="#exampleModal">
                              Update
                            </button>`;
                }
            }
        ]
    });
});



