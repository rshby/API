// Get Ajax
/*
$.ajax({
    type: "GET",
    url: "https://swapi.dev/api/people",
    data: {}
}).done((result) => {
    let data = result.results;
    let text = "";
    $.each(data, function (index, val) {
        text += `<tr>
                    <th scope="row">${index + 1}</th>
                    <td>${val.name}</td>
                    <td>${val.height}cm</td>
                    <td>${val.mass}Kg</td>
                    <td>${val.skin_color}</td>
                    <td>${val.eye_color}</td>
                    <td>${val.birth_year}</td>
                </tr>`;
    })
    $('#tableSW').html(text);
}).fail((err) => {
    console.log(err);
})
*/

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

// Get Data Pokemon
$.ajax({
  type: "GET",
  url: "https://pokeapi.co/api/v2/pokemon/",
  data: {},
})
  .done((result) => {
    let response = result.results;
    let text = "";
    $.each(response, function (index, data) {
      text += `<tr>
                <th scope="row">${index + 1}</th>
                <td>${data.name}</td>
                <td><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalPokemon" onclick="ShowDetail('${
                  data.url
                }')">Detail</button></td>
                </tr>`;
      $("#tablePokemon").html(text);
    });
  })
  .fail((e) => {
    console.log(e);
  });

function ShowDetail(url) {
  $.ajax({
    type: "GET",
    url: url,
    data: {},
  })
    .done((result) => {
      let hpValue = result.stats[0].base_stat;
      let attackValue = result.stats[1].base_stat;
      let defenseValue = result.stats[2].base_stat;
      let text = "";
      text += `<h3>${result.name}</h3>
      <img class="gambar_pokemon" src="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/${result.id}.png" alt="">
      <div class="row text-center">
          <div class="col">
              <h5 class="mt-1">Hp</h5>
              <div class="progress">
                  <div class="progress-bar progress-bar-striped" role="progressbar" style="width: ${result.stats[0].base_stat}%;" aria-valuenow="${result.stats[0].base_stat}" aria-valuemin="0" aria-valuemax="100">${result.stats[0].base_stat}%</div>
              </div>
          </div>
          <div class="col">
              <h5>Attack</h5>
              <div class="progress">
                  <div class="progress-bar progress-bar-striped bg-danger" role="progressbar" style="width: ${result.stats[1].base_stat}%;" aria-valuenow="${result.stats[1].base_stat}" aria-valuemin="0" aria-valuemax="100">${result.stats[1].base_stat}%</div>
              </div>
          </div>
          <div class="col">
              <h5>Defense</h5>
              <div class="progress">
                  <div class="progress-bar progress-bar-striped bg-success" role="progressbar" style="width: ${result.stats[2].base_stat}%;" aria-valuenow="${result.stats[2].base_stat}" aria-valuemin="0" aria-valuemax="100">${result.stats[2].base_stat}%</div>
              </div>
          </div>
      </div>`;

      $("#modalPoke_kiri").html(text);

      // cek log
      console.log(result.height);
      console.log(
        `${result.stats[0].stat.name} = ${result.stats[0].base_stat}`
      );
      console.log(
        `${result.stats[2].stat.name} = ${result.stats[2].base_stat}`
      );
    })
    .fail((e) => {
      console.log(e);
    });
  console.log(url);
}
