
// Get Ajax
$.ajax({
    type: "GET",
    url: "https://swapi.dev/api/people",
    data: {}
}).done((result) => {
    let data = result.results;
    let text = "";
    $.each(data, function (index, val) {
        text += `<tr>
                    <th scope="row">${index+1}</th>
                    <td>${val.name}</td>
                    <td>${val.height}</td>
                    <td>${val.mass}</td>
                    <td>${val.skin_color}</td>
                    <td>${val.eye_color}</td>
                    <td>${val.birth_year}</td>
                </tr>`;
    })
    $('#tableSW').html(text);
}).fail((err) => {
    console.log(err);
})


// DataTable
/*
$(document).ready(function () {
    $('#tableSW').DataTable({
        "ajax": "https://swapi.dev/api/people",
        "columns": [
            { "results": "name"},
            { "results": "name" },
            { "results": "height" },
            { "results": "mass" },
            { "results": "skin_color" },
            { "results": "eye_color" },
            { "results": "birth_year" }
        ]
    });
});
*/
