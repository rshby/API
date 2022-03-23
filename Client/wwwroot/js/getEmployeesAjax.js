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
        "scrollX": true,
        dom: "Bftrip",
        buttons: [
            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-files-o"></i>',
                titleAttr: 'Copy',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i>',
                titleAttr: 'Excel',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o"></i>',
                titleAttr: 'CSV',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"></i>',
                titleAttr: 'PDF',
                orientation: 'landscape',
                pageSize: 'A4',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
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
            { "data": "birthDate" },
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
                    return `<button type="button" class="btn btn-warning d-inline" data-toggle="modal" data-target="#modalUpdate" onclick="DetailUpdate('${row.nik}')">
                              Update
                            </button>
                            <button type="button" class="btn btn-danger d-inline">
                              Delete
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

// Detail Modal Update
function DetailUpdate(inputURL) {
    $.ajax({
        type: "GET",
        url: `https://localhost:44300/api/employees/${inputURL}`,
        data: {}
    }).done((result) => {
        let isiModal = `<div class="row">
                            <!-- Form kiri -->
                            <div class="col">
                                <div class="form-group">
                                    <label>First Name</label>
                                    <input type="text" class="form-control" id="updateFirstName" value="${result.firstName}" required>
                                    <div class="invalid-tooltip">
                                        First Name Harus Diisi!
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Last Name</label>
                                    <input type="text" class="form-control" id="updateLastName" value="${result.lastName}" required>
                                    <div class="invalid-tooltip">
                                        Last Name Harus Diisi!
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>First Name</label>
                                    <input type="text" class="form-control" id="updatePhone" value="${result.phone}" required>
                                    <div class="invalid-tooltip">
                                        Phone Harus Diisi!
                                    </div>
                                </div>
                            </div>

                            <!-- Form Kanan -->
                            <div class="col">
                                <div class="form-group">
                                    <label>Salary</label>
                                    <input type="text" class="form-control" id="updateSalary" value="${result.salary}" required>
                                    <div class="invalid-tooltip">
                                        Salary Harus Diisi!
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Email</label>
                                    <input type="email" class="form-control" id="updateEmail" value="${result.email}" required>
                                    <div class="invalid-tooltip">
                                        Email Harus Diisi!
                                    </div>
                                </div>
                            </div>
                        </div>`;
        
        $("#formUpdateDataEmployee").html(isiModal);
    }).fail((e) => {
        console.log(e);
    });
}

