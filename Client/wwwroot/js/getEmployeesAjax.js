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
                              <i class="fa-solid fa-file-pen"></i>
                            </button>
                            <button type="button" class="btn btn-danger d-inline mt-1" onclick="DeleteData('${row.nik}')">
                              <i class="fa-solid fa-trash-can"></i>
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
        Swal.fire({
            icon: 'success',
            title: 'Selamat',
            text: 'Data Berhasil Ditambah',
            showConfirmButton: false,
            timer: 1560,
            footer: '<a href="">Why do I have this issue?</a>'
        }).then(function () {
            window.location.reload();
        });
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Opps...',
            text: 'Data Gagal Ditambah',
            showConfirmButton: false,
            timer: 1560,
            footer: '<a href="">Why do I have this issue?</a>'
        }).then(function () {
            window.location.reload();
        });
    })
}

// function Get Data Employee By NIK
function GetDataEmployeeByNIK(inputNIK) {
    let dataEmp = {};
    $.ajax({
        type: "GET",
        url: `https://localhost:44300/api/employees/${inputNIK}`,
        async: false,
        data: {}
    }).done((result) => {
        dataEmp.nik = result.nik;
        dataEmp.firstName = result.firstName;
        dataEmp.lastName = result.lastName;
        dataEmp.phone = result.phone;
        dataEmp.birthDate = result.birthDate;
        dataEmp.salary = result.salary;
        dataEmp.gender = result.gender;
        dataEmp.email = result.email;
    }).fail((e) => {
        console.log(e);
    })
    return dataEmp;
}

// Detail Modal Update
function DetailUpdate(inputNIK) {
    let dataEmp = GetDataEmployeeByNIK(inputNIK);

    document.getElementById("updateFirstName").value = dataEmp.firstName;
    document.getElementById("updateLastName").value = dataEmp.lastName;
    document.getElementById("updatePhone").value = dataEmp.phone;
    document.getElementById("updateSalary").value = dataEmp.salary;
    document.getElementById("updateEmail").value = dataEmp.email;

    document.getElementById("modalFooterUpdate").innerHTML = `<button type="button" id="buttonUpdateDataEmployee" onclick="UpdateDataEmployee('${dataEmp.nik}')" class="btn btn-success">Update Data</button>`;
}

// Update Data
function UpdateDataEmployee(inputNIK) {
    let dataEmp = GetDataEmployeeByNIK(inputNIK);
    let data = {};
    data.nik = dataEmp.nik;
    data.firstName = $("#updateFirstName").val();
    data.lastName = $("#updateLastName").val();
    data.email = $("#updateEmail").val();
    data.phone = $("#updatePhone").val();
    data.birthDate = dataEmp.birthDate;
    data.salary = $("#updateSalary").val();
    data.gender = dataEmp.gender;

    // Ajax untuk Update Data
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "PUT",
        dataType: "json",
        url: "https://localhost:44300/api/employees",
        data: JSON.stringify(data)
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Selamat',
            text: 'Update Data Berhasil',
            showConfirmButton: false,
            timer: 1560,
            footer: '<a href="">Why do I have this issue?</a>'
        }).then(function () {
            window.location.reload();
        });
    }).fail((e) => {
        Swal.fire({
            icon: 'error',
            title: 'Opps...',
            text: 'Update Data Gagal',
            showConfirmButton: false,
            timer: 1560,
            footer: '<a href="">Why do I have this issue?</a>'
        }).then(function () {
            window.location.reload();
        });
    });
}

// Delete Data
function DeleteData(inputNIK) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success ml-2',
            cancelButton: 'btn btn-danger mr-2'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: `https://localhost:44300/api/employees/${inputNIK}`,
                data: {}
            }).done((result) => {
                swalWithBootstrapButtons.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                ).then(function () {
                    window.location.reload();
                });
            });
        } else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your imaginary file is safe :)',
                'error'
            ).then(function () {
                window.location.reload();
            });
        }
    });
}


