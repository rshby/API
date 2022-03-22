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
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    },
                    {
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    }
                ]
            }
        ],
        "ajax": {
            "url": "https://localhost:44300/api/employees/master",
            "dataSrc": ""
        },
        "columns": [
            {
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
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

// Menampilkan Universities Pada Form Insert
$.ajax({
    type: "GET",
    url: "https://localhost:44300/api/universities",
    data: {}
}).done((result) => {
    let pilihanUniv = ``;
    $.each(result, function (index, data) {
        pilihanUniv += `<option value="${data.id}">${data.name}</option>`;
    });
    $("#inputUniversity").html(pilihanUniv);
}).fail((e) => {
    console.log(e);
})

// Function Insert Data Employee
function InsertDataEmployee() {
    let obj = new Object();
    obj.firstName = $('#inputFirstName').val();
    obj.lastName = $('#inputLastName').val();
    obj.phone = $('#inputPhone').val();
    obj.birthDate = $('#inputBirthDate').val();
    obj.salary = parseInt($('#inputSalary').val());
    obj.email = $('#inputEmail').val();
    obj.gender = parseInt($('#inputGender').val());
    obj.password = $('#inputPassword').val();
    obj.degree = $('#inputDegree').val();
    obj.gpa = $('#inputGPA').val();
    obj.university_id = parseInt($('#inputUniversity').val());

    // Insert Menggunakan Ajax
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "POST",
        url: "https://localhost:44300/api/accounts/register",
        dataType: "json",
        data: JSON.stringify(obj)
    }).done((result) => {
        alert(`Data Employee Berhasil Ditambah`);
        window.location.reload();
    }).fail((error) => {
        alert(`Data Gagal Ditambah`);
        console.log(error);
    })
}


