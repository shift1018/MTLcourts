// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var map = L.map("map").setView([45.5355, -73.6286], 12);

L.tileLayer("http://{s}.tile.osm.org/{z}/{x}/{y}.png", {
  attribution:
    '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
}).addTo(map);

// L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
//   maxZoom: 19,
//   attribution:
//     '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
// }).addTo(map);

// var marker = L.marker([45.5355, -73.6286]).addTo(map);

var marker = L.marker([45.5355, -73.6286]).bindPopup("Jarry Park").addTo(map);

// var marker = L.marker([45.5355, -73.6286], { icon: greenIcon }).bindPopup("I am the " + item.branchName +" leaf.").addTo(map);
