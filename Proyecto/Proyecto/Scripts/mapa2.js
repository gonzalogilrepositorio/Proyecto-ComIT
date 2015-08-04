var map;
var markers = [];

function iniciar(loc) {
    // la toma -33.05450067078694, -65.61671376228333
    //SIERRA SAN LUIS -33.26873922684649, -66.27725601196289
    if (loc != "")
    {
        console.log("no null");
        var LOCATION = new google.maps.LatLng($("#lat").val(), $("#lng").val());
        var haightAshbury = LOCATION;
        var mapOptions = {
            zoom: 12,
            center: haightAshbury,
            mapTypeId: google.maps.MapTypeId.TERRAIN
        };

    }
    else
    {
        var haightAshbury = new google.maps.LatLng(-33.30208948329983, -66.33675813674927);
        $("#latLng").val("No se encontro GeoLocalizacion");
        var mapOptions = {
            zoom: 12,
            center: haightAshbury,
            mapTypeId: google.maps.MapTypeId.TERRAIN
        };

    }
   
    map = new google.maps.Map(document.getElementById('map-canvas'),
        mapOptions);

    //// This event listener will call addMarker() when the map is clicked.
    //google.maps.event.addListener(map, 'click', function (event) {
    //    deleteMarkers();
    //    haightAshbury = new google.maps.LatLng(-33.05450067078694, -65.61671376228333);
    //    console.log(haightAshbury.lati);
    //    //haightAshbury = new google.maps.LatLng($("#latLng").val());
    //    addMarker(haightAshbury);
    //    console.log("textbox:" + $("#latLng").val());

    //});
        


    //});
  

    // Adds a marker at the center of the map.
    addMarker(haightAshbury);
    console.log(loc);
    var locacion2 = new google.maps.LatLng(loc);
    addMarker(locacion2);
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

google.maps.event.addDomListener(window, 'load', iniciar($("#latLng").val()));
