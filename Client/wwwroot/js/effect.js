document.getElementById("btn_ubah").addEventListener("click", function () {
  document.getElementById("konten2").style.backgroundColor = "blue";
});

document.getElementById("konten2").addEventListener("mouseenter", function () {
  document.getElementById("konten2").style.backgroundColor = "red";
});

const animals = [
  { name: "Fluffy", species: "cat", class: { name: "mamalia" } },
  { name: "Nemo", species: "fish", class: { name: "invertebrata" } },
  { name: "Garfield", species: "cat", class: { name: "mamalia" } },
  { name: "Dory", species: "fish", class: { name: "invertebrata" } },
  { name: "Camello", species: "cat", class: { name: "mamalia" } },
];

let onlyCat = [];

animals.forEach((data) => {
  if (data.species == "cat") {
    onlyCat.push(data);
  }
});

animals.forEach((data) => {
  if (data.species == "fish") {
    data.class.name = "Non-Mamalia";
  }
});

console.log(onlyCat);
console.log(animals);
