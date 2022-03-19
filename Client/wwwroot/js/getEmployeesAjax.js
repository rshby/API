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
