//'sk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtZjF3eTA0eWIzc3IwMzBobDhrM2EifQ.b9aR71QAAte9dX5htMrGBQ';
mapboxgl.accessToken = 'pk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtYmFudDJxZm0zc21vcWk5azQ1ZHYifQ.Tlw2Upgs8NtKtW-f30lR8Q';
const map = new mapboxgl.Map({
    container: 'map',
    // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
    style: 'mapbox://styles/mapbox/streets-v12',
    center: [12.550343, 55.665957],
    zoom: 8
});

// Create a default Marker and add it to the map.
const marker1 = new mapboxgl.Marker()
    .setLngLat([12.554729, 55.70651])
    .addTo(map);

// Create a default Marker, colored black, rotated 45 degrees.
const marker2 = new mapboxgl.Marker({ color: 'black', rotation: 45 })
    .setLngLat([12.65147, 55.608166])
    .addTo(map);