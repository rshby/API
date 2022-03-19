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
                <td><button type="button" class="btn btn-warning" data-toggle="modal" data-target="#modalPokemon" onclick="ShowDetail('${
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

      let isiTab1 = `<div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                      <h6 class="mt-3">${result.id}</h6> <br>
                     <p><span><br></span>Id</p>
                    </div>
                    <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                        <h6 class="mt-3">${result.name}</h6> <br>
                        <p><span><br></span>Name</p>
                    </div>
                    <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                        <h6 class="mt-3">${result.base_experience}</h6> <br>
                        <p>Base Experience</p>
                    </div>
                    <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                        <h6 class="mt-3">${result.height}</h6> <br>
                        <p>Height</p>
                    </div>
                    <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                        <h6 class="mt-3">${result.weight}</h6> <br>
                        <p>Weight</p>
                    </div>`;
      let isiAbilities = ``;
      $.each(result.abilities, function (index, data) {
        isiAbilities += `<div class="col-3 m-2">
                          <div class="border rounded-circle shadow" id="lingkaran_abl" style="height: 107px; width: 107px;">
                              <p class="text_abilities font-weight-bold">${data.ability.name}</p>
                          </div>
                         </div>`;
      });

      let spAttack = `<div class="progress-bar progress-bar-striped bg-danger" role="progressbar" style="width: ${result.stats[3].base_stat}%;" aria-valuenow="${result.stats[3].base_stat}" aria-valuemin="0" aria-valuemax="100">${result.stats[3].base_stat}%</div>`;
      let spDefense = `<div class="progress-bar progress-bar-striped bg-success" role="progressbar" style="width: ${result.stats[4].base_stat}%;" aria-valuenow="${result.stats[4].base_stat}" aria-valuemin="0" aria-valuemax="100">${result.stats[4].base_stat}%</div>`;
      let spSpeed = `<div class="progress-bar progress-bar-striped bg-warning" role="progressbar" style="width: ${result.stats[5].base_stat}%;" aria-valuenow="${result.stats[5].base_stat}" aria-valuemin="0" aria-valuemax="100">${result.stats[5].base_stat}%</div>`;

      $("#modalPoke_kiri").html(text);
      $("#isiTab1").html(isiTab1);
      getSpecies(result.species.url);
      $("#isiAbilities").html(isiAbilities);
      $("#spAttack").html(spAttack);
      $("#spDefense").html(spDefense);
      $("#spSpeed").html(spSpeed);

      // cek log
      console.log(result.species.url);
    })
    .fail((e) => {
      console.log(e);
    });
}

function getSpecies(inputUrl) {
  $.ajax({
    type: "GET",
    url: inputUrl,
    data: {},
  })
    .done((result) => {
      let tabSpecies = `<div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                            <h6 class="mt-1 mb-4">${result.base_happiness}</h6>
                            <p>Base Hapinness</p>
                        </div>
                        <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                            <h6 class="mt-1 mb-4">${result.capture_rate}</h6>
                            <p>Capture Rate</p>
                        </div>
                        <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                            <h6 class="mt-1 mb-4">${result.color.name}</h6>
                            <p><span><br></span>Color</p>
                        </div>
                        <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                            <h6 class="mt-1 mb-4">${result.habitat.name}</h6>
                            <p><span><br></span>Habitat</p>
                        </div>
                        <div class="col-3 border rounded shadow m-2 p-2 kotak_tab">
                            <h6 class="mt-1 mb-4">${result.growth_rate.name}</h6>
                            <p>Growth Rate</p>
                        </div>`;
      let tabEggGroups = `<span class="badge badge-pill badge-danger p-2 mr-1">${result.egg_groups[0].name}</span>
                          <span class="badge badge-pill badge-warning p-2 ml-1">${result.egg_groups[1].name}</span>`;

      $("#tabSpecies").html(tabSpecies);
      $("#tabEggGroups").html(tabEggGroups);
    })
    .fail((e) => {
      console.log(e);
    });
}
