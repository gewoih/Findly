//sk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtZjF3eTA0eWIzc3IwMzBobDhrM2EifQ.b9aR71QAAte9dX5htMrGBQ
//pk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtYmFudDJxZm0zc21vcWk5azQ1ZHYifQ.Tlw2Upgs8NtKtW-f30lR8Q

this.friendsMarkers = new Map();

function drawMap(latitude, longitude) {
    mapboxgl.accessToken = 'pk.eyJ1IjoiZ2V3b2loIiwiYSI6ImNsY2RtYmFudDJxZm0zc21vcWk5azQ1ZHYifQ.Tlw2Upgs8NtKtW-f30lR8Q';
    this.map = new mapboxgl.Map({
        container: 'map', // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: 'mapbox://styles/mapbox/streets-v12', // style URL
        center: [longitude, latitude], // starting position [lng, lat]
        zoom: 15 // starting zoom
    });

    this.userMarker = new mapboxgl.Marker({
        color: '#F84C4C' // color it red
    });

    this.userMarker.setLngLat([longitude, latitude]);
    this.userMarker.addTo(this.map);

    console.log("Map created");
}

function updateUserGeolocation(latitude, longitude) {
    this.map.setCenter([longitude, latitude]);
    this.userMarker.setLngLat([longitude, latitude]);

    console.log("User geolocation updated to: ", latitude, ", ", longitude);
}

function updateFriendGeolocation(userId, latitude, longitude) {
    //Пофиксить постоянные добавления маркеров
    if (!this.friendsMarkers.has(userId)) {
        const marker = new mapboxgl.Marker({
            color: '#' + Math.floor(Math.random() * 16777215).toString(16) //random color
        });

        marker.setLngLat([longitude, latitude]);
        marker.addTo(this.map);

        this.friendsMarkers[userId] = marker;

        console.log("Friend's marker added: ", userId);
    }
    else {
        this.friendsMarkers[userId].setLngLat([longitude, latitude]);

        console.log("Friend's marker (", userId, ") updated to: ", latitude, ", ", longitude);
    }

}
