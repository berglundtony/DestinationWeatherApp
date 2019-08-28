var source, destination;
var directionsDisplay = new google.maps.DirectionsRenderer();
var directionsService = new google.maps.DirectionsService();

google.maps.event.addDomListener(window, 'load', initMap);

function initMap() {
    google.maps.event.addDomListener(window, 'load', function () {
        directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
    });

    var map;
    var myLatlng = new google.maps.LatLng(18.9750, 72.8258);
    var mapOptions = {
        zoom: 12,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        mapTypeControl: false
    };

    map = new google.maps.Map(document.getElementById('map'), mapOptions);
    directionsDisplay.setMap(map);
    directionsDisplay.setPanel(document.getElementById('dvPanel'));
    infoWindow = new google.maps.InfoWindow;

    // Here the map will be located to the place were you are
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            console.log(pos);
            // Here we set a marker at the place were we are
            var marker = new google.maps.Marker({
                position: pos,
                map: map,
                title: 'Marker'
            });
            marker.setMap(map);

            infoWindow.setPosition(pos);
            map.setCenter(pos);
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

function GetRoute() {
    //********* DIRECTIONS AND ROUTE **********************//
    $("#dvLocation").empty();
    calculateAndDisplayRoute(directionsService, directionsDisplay);
    $("input[name='travelMode']").on('click', function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    });
}
//This function will get the routs between two places
function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    source = document.getElementById("travelfrom").value;
    destination = document.getElementById("travelto").value;
    var selectedMode = $('input[name="travelMode"]:checked').val();

    var request = {
        origin: source,
        destination: destination,
        travelMode: google.maps.TravelMode[selectedMode]
    };
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        } else {
            console.log('Directions request failed due to ' + status);
        }
    });
    //********* DISTANCE AND DURATION **********************//
    var service = new google.maps.DistanceMatrixService();
    var selectedMode = $('input[name="travelMode"]:checked').val();

    service.getDistanceMatrix({
        origins: [source],
        destinations: [destination],
        travelMode: google.maps.TravelMode[selectedMode],
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, function (response, status) {
        if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS") {
            var distance = response.rows[0].elements[0].distance.text;
            var duration = response.rows[0].elements[0].duration.text;
            var dvDistance = document.getElementById("dvDistance");
            dvDistance.innerHTML = "";
            dvDistance.innerHTML += "<b> Distance: </b>" + distance + "<br />";
            dvDistance.innerHTML += "<b> Duration: </b>" + duration;

        } else {
            console.log("Unable to find the distance via road.");
        }
    });
};

$(function () {
    //This make the texbox writeble
    $("#travelfrom").on('click focusin', function () {
        this.value = '';
        $(".error").remove();
    });
    $("#travelto").on('click focusin', function () {
        $(".error").remove();
    });
    //When click Get Location button the latitude and longitude will be set in the Travel From textbox
    $("#showMe").on("click", function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var geolocation = new google.maps.LatLng(
                    position.coords.latitude, position.coords.longitude);
                $("#travelfrom").val(position.coords.latitude + "," + position.coords.longitude);
            })
        }
    });
});