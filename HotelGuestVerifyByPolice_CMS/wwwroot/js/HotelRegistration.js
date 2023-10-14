﻿
$(document).ready(function () {
    debugger;

    var mvs = $("#isMobileVerify:checked").val();
    if ((mvs == "true")) {
        $("#verifyNo").modal("hide");
        $("#verifyModel").hide();
        $("#verified").show();
    }
    $("#mobile").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 44) {
            //display error message
            //$("#err_mobile").html("केवल अंक | Digits Only").show().delay(1500).show().fadeOut('slow');
            $("#err_mobile").html("Digits Only").show().delay(1500).show().fadeOut("slow");
            return false;
        }
    });

    // For loading all Active States
    $.ajax({
        type: "post",
        url: "/States/CountryWiseStateList",
        data: { cCode: "IN" },
        datatype: "json",
        traditional: true,
        success: function (data) {
            //console.log(data);
            statelist = '<option value="">Select State</option>';
            for (var i = 0; i < data.length; i++) {
                statelist = statelist + "<option value=" + data[i].stateId + ">" + data[i].stateName + "</option>";
            }
            //district = district + '</select>';
            $("#statelist").html(statelist);
        },
    });

    // for loading District List
    $("#statelist").change(function () {
        $.ajax({
            type: "post",
            url: "/States/DistrictList",
            data: { stateID: $("#statelist").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var distlist = '<option value="-1">Select District</option>';
                for (var i = 0; i < data.length; i++) {
                    distlist = distlist + "<option value=" + data[i].distId + ">" + data[i].distName + "</option>";
                }
                $("#districtlist").html(distlist);
            },
        });
    });

    // for loading City List
    $("#districtlist").change(function () {
        $.ajax({
            type: "post",
            url: "/States/CityList",
            data: { stateID: $("#statelist").val(), distID: $("#districtlist").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var citylist = '<option value="-1">Select City</option>';
                for (var i = 0; i < data.length; i++) {
                    citylist = citylist + "<option value=" + data[i].cityId + ">" + data[i].cityName + "</option>";
                }
                $("#citylist").html(citylist);
            },
        });
    });

    // for loading Police Station List
    $("#citylist").change(function () {
        $.ajax({
            type: "post",
            url: "/States/PoliceStationList",
            data: { stateID: $("#statelist").val(), distID: $("#districtlist").val(), cityID: $("#citylist").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var pslist = '<option value="-1">Select Police Station</option>';
                for (var i = 0; i < data.length; i++) {
                    pslist = pslist + "<option value=" + data[i].stationCode + ">" + data[i].stationName + "</option>";
                }
                $("#pslist").html(pslist);
            },
        });
    });
   
});



    debugger;
    var map;
    var marker;
    var userCoordinates;

    function initMap() {
        debugger;
        // Set initial coordinates as the center and zoom level

        // Check if the Geolocation API is available
        if ("geolocation" in navigator) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    userCoordinates = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };

                    // Create a map with the specified options after obtaining user's coordinates
                    map = new google.maps.Map(document.getElementById("map"), {
                        center: userCoordinates,
                        zoom: 13,
                    });

                    // Specify the icon size using the iconSize property
                    var customIcon = {
                        url: "../../images/pin-point.png", // Specify the URL to your custom marker icon
                        scaledSize: new google.maps.Size(32, 32), // Set the size of the icon (e.g., 32x32 pixels)
                    };
                    // Create a custom marker
                    marker = new google.maps.Marker({
                        position: userCoordinates,
                        map: map,
                        icon: customIcon, // Specify the URL to your custom marker icon
                        draggable: true, // Make the marker draggable
                    });

                    // Add a listener for the marker drag event
                    google.maps.event.addListener(marker, "dragend", function () {
                        var newPosition = marker.getPosition();
                        document.getElementById("lat").value = newPosition.lat().toFixed(4);
                        document.getElementById("zlong").value = newPosition.lng().toFixed(4);
                        alert("Marker dragged to latitude: " + newPosition.lat().toFixed(4) + " and longitude: " + newPosition.lng().toFixed(4));
                    });

                    // Create map type controls for switching between Map View and Satellite View
                    var mapTypeControlDiv = document.createElement("div");
                    var mapTypeControl = new MapTypeControl(mapTypeControlDiv, map);

                    map.controls[google.maps.ControlPosition.TOP_RIGHT].push(mapTypeControlDiv);
                },
                function (error) {
                    handleGeolocationError(error);
                }
            );
        } else {
            // Geolocation is not available in this browser
            console.error("Geolocation is not available.");
        }
    }
    // Define a custom control for switching between Map View and Satellite View
    function MapTypeControl(controlDiv, map) {
        controlDiv.style.padding = "10px";

        var mapTypeControlUI = document.createElement("div");
        mapTypeControlUI.style.backgroundColor = "white";
        mapTypeControlUI.style.border = "2px solid #ccc";
        mapTypeControlUI.style.borderRadius = "3px";
        mapTypeControlUI.style.boxShadow = "0 2px 6px rgba(0,0,0,0.3)";
        mapTypeControlUI.style.cursor = "pointer";
        mapTypeControlUI.style.textAlign = "center";
        mapTypeControlUI.title = "Click to toggle Map/Satellite View";
        controlDiv.appendChild(mapTypeControlUI);

        var mapTypeControlText = document.createElement("div");
        mapTypeControlText.style.fontFamily = "Arial,sans-serif";
        mapTypeControlText.style.fontSize = "12px";
        mapTypeControlText.style.padding = "5px 10px";
        mapTypeControlText.innerHTML = "Map View";
        mapTypeControlUI.appendChild(mapTypeControlText);

        mapTypeControlUI.addEventListener("click", function () {
            toggleMapType();
        });

        function toggleMapType() {
            if (map.getMapTypeId() === google.maps.MapTypeId.ROADMAP) {
                map.setMapTypeId(google.maps.MapTypeId.HYBRID);
                mapTypeControlText.innerHTML = "Satellite View";
            } else {
                map.setMapTypeId(google.maps.MapTypeId.ROADMAP);
                mapTypeControlText.innerHTML = "Map View";
            }
        }
    }

    function handleGeolocationError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                console.error("User denied the request for Geolocation.");
                break;
            case error.POSITION_UNAVAILABLE:
                console.error("Location information is unavailable.");
                break;
            case error.TIMEOUT:
                console.error("The request to get user location timed out.");
                break;
            case error.UNKNOWN_ERROR:
            default:
                console.error("An unknown error occurred while getting user location.");
                break;
        }
    }



    function CheckHotelRegNoIsExist() {
        // For loading all Active States
        debugger;
        $.ajax({
            type: "post",
            url: "/Account/CheckHotelRegNo",
            data: { hotelregno: $("#hotelRegNo").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                json = JSON.parse(data);
                console.log(json.message);
                var myfield = $("#hotelRegNo");
                myfield.removeClass("field-validation-error");
                myfield.next("span[data-valmsg-for]").removeClass("field-validation-error").addClass("field-validation-valid").html("");

                let button = document.getElementById("hotelregexist");
                button.style.display = "block";
                if (json.status == "success") {
                    // if button color is red change it green otherwise change it to red.
                    button.style.color = "green";
                } else {
                    $("#hotelRegNo").val("")
                    button.style.color = "red";
                }
                $("#hotelregexist").text(json.message);
            },
        });
    }
    function CheckHotelUsernameExist() {
        debugger;
        $.ajax({
            type: "post",
            url: "/Account/CheckHotelUserid",
            data: { hoteluserid: $("#userId").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                json = JSON.parse(data);
                console.log(json.message);
                var myfield = $("#userId");
                myfield.removeClass("field-validation-error");
                myfield.next("span[data-valmsg-for]").removeClass("field-validation-error").addClass("field-validation-valid").html("");

                let button = document.getElementById("hoteluseridexist");
                button.style.display = "block";
                if (json.status == "success") {
                    // if button color is red change it green otherwise change it to red.
                    button.style.color = "green";
                } else {
                    $("#userId").val("")
                    button.style.color = "red";
                }
                $("#hoteluseridexist").text(json.message);
            },
        });
    }

    $("#mobile").change(function () {
        $("#vefifyLink").prop("disabled", false);
    });
    function VerifyMobileNo() {
        debugger;
        if ($("#mobile").val() == null || $("#mobile").val() == "") {
            $("#vefifyLink").prop("disabled", true);
            $("#mobilenoerror").text("Please Enter Mobile No.");
            let mobilenoerror = document.getElementById("mobilenoerror");
            mobilenoerror.style.color = "red";
        } else {
            var phoneno = /^\d{10}$/;

            if ($("#mobile").val().match(phoneno)) {
                let mobilenoerror = document.getElementById("mobilenoerror");
                mobilenoerror.style.display = "none";

                $.ajax({
                    type: "post",
                    url: "/Account/VerifyMoNo",
                    data: { mobileno: $("#mobile").val() },
                    datatype: "json",
                    traditional: true,
                    beforeSend: function () {
                        // setting a timeout
                        mobilenoerror.style.color = "black";
                        mobilenoerror.style.display = "block";
                        $("#mobilenoerror").text("Please Wait");
                    },
                    success: function (data) {
                        json = JSON.parse(data);
                        console.log(json.message);
                        document.getElementById("getotp").value = json.otp;
                       
                        let button = document.getElementById("sendotpres");
                        button.style.display = "block";
                        if (json.status == "success") {
                            // if button color is red change it green otherwise change it to red.
                            button.style.color = "green";
                            $("#verifyNo").modal({ backdrop: 'static', keyboard: false },"show");
                            mobilenoerror.style.display = "none";
                         
                        } else {
                            button.style.color = "red";
                            mobilenoerror.style.color = "red";
                            mobilenoerror.style.display = "block";
                            $("#mobilenoerror").text("Invalid Mobile Number");
                          
                        }
                        $("#sendotpres").text(json.message);
                    },
                });
            } else {
                alert("Invalid Mobile Number");
            }
        }
}

let otpValue;
document.addEventListener("DOMContentLoaded", function () {
    debugger;
    const otpForm = document.getElementById("otp-form");
    const otpInputs = otpForm.querySelectorAll(".otp");

    // Add event listeners to move focus to the next input on key press
    otpInputs.forEach(function (input, index) {
        input.addEventListener("input", function () {
            if (this.value && index < otpInputs.length - 1) {
                otpInputs[index + 1].focus();
            }
        });

        // Add event listener to move focus to the previous input on backspace key press
        input.addEventListener("keydown", function (event) {
            if (event.key === "Backspace" && index > 0 && !this.value) {
                otpInputs[index - 1].focus();
            }
        });
    });

    otpForm.addEventListener("submit", function (event) {
        event.preventDefault();

        const otpInputs = otpForm.querySelectorAll(".otp");
        const otpDigits = [];

        otpInputs.forEach(function (input) {
            otpDigits.push(input.value);
        });

        otpValue = otpDigits.join("");
        console.log("OTP Value:", otpValue);
        CheckOTP();
    });
});
    function CheckOTP() {
        debugger;
        var otp1 = document.getElementById("getotp").value;
        //var otp2 = document.getElementById("enterotp").value;
        var otp2 = otpValue;
        

        if (otp1 == otp2) {
            document.getElementById("isMobileVerify").checked = true;

            $("#verifyNo").modal("hide");
            $("#verifyModel").hide();
            $("#verified").show();

            var mymobilefield = $("#isMobileVerify");
            mymobilefield.removeClass("field-validation-error");
            mymobilefield.next("span[data-valmsg-for]").removeClass("field-validation-error").addClass("field-validation-valid").html("");
        } else {
            document.getElementById("isMobileVerify").checked = false;
            alert("otp is wrong");
        }
    }


  