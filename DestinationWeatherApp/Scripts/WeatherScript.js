
//Slider functions
var slider = document.getElementById("Range");
var output = document.getElementById("output");
output.innerHTML = slider.value; // Display the default slider value

// Update the current slider value (each time you drag the slider handle)
slider.oninput = function () {
    output.innerHTML = this.value;
}
$(function () {
    //When click Get Route button the rout will be set
    $("#GetRoute").on("click", function () {
        var fromaddress = $("#travelfrom").val();
        var toaddress = $("#travelto").val();
        var latitude = "";
        var longitude = "";
        var destination = "";
        var address = "";

        $(".error").remove();
        if (fromaddress.length < 1) {
            $("#travelfrom").after('<span class="error">This field is required</span>');
        }
        if (toaddress.length < 1) {
            $('#travelto').after('<span class="error">This field is required</span>');
        }
        else {
            Getweather();
            $.ajax({
                cache: false,
                type: "POST",
                url: "Home/GetCoordinates",
                data: { "fromaddress": fromaddress, "toaddress": toaddress },
                dataType: "json",
                success: function (result) {
                    console.log(result);
                    console.log($.parseJSON(result));

                    $.each($.parseJSON(result), function (index, value) {
                        if (index === 0) {
                            if (value.Address != null) {
                                address += "<p><b>From: </b>" + value.Address;
                                address += value.Message = ""
                                if (value.Latitude != null)
                                    address += "<b> Latidude: </b>" + value.Latitude;
                                if (value.Longitude != null)
                                    address += "<b> Longitude: </b>" + value.Longitude + "</p>";
                            }
                            else {
                                $("#travelfrom").after('<span class="error">' + value.Message + '</span>');
                            }
                        } else {
                            if (value.Address != null) {
                                address += "<p><b> To: </b>" + value.Address;
                                address += value.Message = "";
                                destination = value.City;
                                console.log(destination);

                                if (value.Latitude != null) {
                                    latitude = value.Latitude;
                                    address += "<b> Latidude: </b>" + latitude;
                                }

                                if (value.Longitude != null) {
                                    longitude = value.Longitude;
                                    address += "<b> Longitude: </b>" + longitude + "</p>";
                                }
                            }
                            else {
                                $("#travelto").after('<span class="error">' + value.Message + '</span>');
                            }
                        }
                    });
                }, error: function () {
                    alert("Error while inserting data");
                }
            });

            $("#dvLocation").empty();
            var dvLocation = document.getElementById("dvLocation");
            dvLocation.innerHTML += address;
        }       
    });
});

function Getweather() {
    $("#dvWeather").empty();
    var weather = "";
    $.ajax({
        cache: false,
        type: "GET",
        url: "Home/GetWeather",
        data: { "Destination": destination },
        dataType: "json",
        success: function (result) {
            console.log(result);
            weather = "<b> If you go now to </b>" + result.Destination;
            weather += "<b> at: </b>" + result.CurrentTime;
            weather += ". <b> It will be: </b>" + result.Weather;
            weather += ". <b> The weather: </b>" + result.Description;
            weather += ". <b> The cloudyness: </b>" + result.Cloudyness + " percent";
            weather += ". <b> The temperature is: </b>" + result.Temp + " Celsius degrees.";

            if (result.Weather === "Rain" || result.Weather === "Thunderstorm" || result.Weather === "Snow") {
                $("#dvBestWeather").show();
                $("#dvBestWeather").empty();
                GetBestWeather();
            } else {
                $("#dvBestWeather").hide();
            }

            var dvWeather = document.getElementById("dvWeather");
            dvWeather.innerHTML += weather;
            $("#dvWorstWeather").empty();
            GetWorstWeather();
        },
        error: function () {
            alert("Error while inserting data");
        }
    });
}

function GetBestWeather() {
    var daysrange = $("#Range").val();
    var forecast = "";
    $.ajax({
        cache: false,
        type: "GET",
        url: "Home/GetWeatherForecast",
        data: { "Destination": destination, "DaysRange": daysrange },
        dataType: "json",
        success: function (result) {
            forecast += "<b> The best weather you will have if you go </b>" + result.Dt;
            forecast += ". <b> The weather will be: </b>" + result.Weather;
            forecast += "<b> and it will be: </b>" + result.Description;
            forecast += ".<b> The temperature will be: </b>" + result.Temperature + " Celsius degrees.";
            var dvBestWeather = document.getElementById("dvBestWeather");
            dvBestWeather.innerHTML += forecast;
        },
        error: function () {
            alert("Error while inserting data");
        }
    });
}


function GetWorstWeather() {
    var worstweather = "";
    var daysrange = $("#Range").val();
    $.ajax({
        cache: false,
        type: "GET",
        url: "Home/GetWorstWeatherForecast",
        data: { "Destination": destination, "DaysRange": daysrange },
        dataType: "json",
        success: function (result) {
            worstweather += "<b> The worst weather you will have if you go </b>" + result.Dt;
            worstweather += ". <b> The weather will be: </b>" + result.Weather;
            worstweather += "<b> and it will be: </b>" + result.Description;
            worstweather += ".<b> The temperature will be: </b>" + result.Temperature + " Celsius degrees.";
            var dvWorstWeather = document.getElementById("dvWorstWeather");
            dvWorstWeather.innerHTML += worstweather;
        },
        error: function () {
            alert("Error while inserting data");
        }
    });
}