//sk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtZjF3eTA0eWIzc3IwMzBobDhrM2EifQ.b9aR71QAAte9dX5htMrGBQ
//pk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtYmFudDJxZm0zc21vcWk5azQ1ZHYifQ.Tlw2Upgs8NtKtW-f30lR8Q

function drawMap(latitude, longitude) {
    mapboxgl.accessToken = 'pk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtYmFudDJxZm0zc21vcWk5azQ1ZHYifQ.Tlw2Upgs8NtKtW-f30lR8Q';
    this.map = new mapboxgl.Map({
        container: 'map', // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: 'mapbox://styles/mapbox/streets-v12', // style URL
        center: [longitude, latitude], // starting position [lng, lat]
        zoom: 15 // starting zoom
    });

    this.marker = new mapboxgl.Marker({
        color: '#F84C4C' // color it red
    });

    this.marker.setLngLat([longitude, latitude]);
    this.marker.addTo(this.map);
}

function changeGeolocation(latitude, longitude) {

    this.map.setCenter([longitude, latitude]);
    this.marker.setLngLat([longitude, latitude]);
}
