const options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
};

let latitude = 0;
let longitude = 0;

getGeolocation = async () => {
    return [latitude, longitude];
};

function onPositionUpdate(pos) {
    latitude = pos.coords.latitude;
    longitude = pos.coords.longitude;
}

function onError(err) {
    console.error(`ERROR(${err.code}): ${err.message}`);
}

watchGeolocation = async () => {
    id = navigator.geolocation.watchPosition(onPositionUpdate, onError, options);
}