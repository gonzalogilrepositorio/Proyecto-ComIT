var map;
var markers = [];

function initialize() {
    //{G: -33.26873922684649, K: -66.27725601196289}
    var haightAshbury = new google.maps.LatLng(-33.30208948329983, -66.33675813674927);
    var mapOptions = {
        zoom: 12,
        center: haightAshbury,
        mapTypeId: google.maps.MapTypeId.TERRAIN
    };
    map = new google.maps.Map(document.getElementById('map-canvas'),
        mapOptions);

    // This event listener will call addMarker() when the map is clicked.
    google.maps.event.addListener(map, 'click', function (event) {
        clearMarkers();
        addMarker(event.latLng);
        $("#geoLat").val(event.latLng.lat());
        $("#geoLng").val(event.latLng.lng());
        console.log($("#geoLat").val())
        
        

    });

    // Adds a marker at the center of the map.
    addMarker(haightAshbury);
}

// Add a marker to the map and push to the array.
function addMarker(location) {
    var marker = new google.maps.Marker({
        position: location,
        map: map
    });
    markers.push(marker);
}

// Sets the map on all markers in the array.
function setAllMap(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setAllMap(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setAllMap(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}

google.maps.event.addDomListener(window, 'load', initialize);
      