// For loading all Active States
$.ajax({
    type: "post",
    url: "/States/StateList",
    datatype: "json",
    traditional: true,
    success: function (data) {
        console.log(data);
        statelist = '<option value="">Select State</option>';
        for (var i = 0; i < data.length; i++) {
            statelist = statelist + '<option value=' + data[i].stateId + '>' + data[i].stateName + '</option>';
        }
        //district = district + '</select>';
        $('#statelist').html(statelist);
    }
});



// for loading District List
$('#statelist').change(function () {


    $.ajax({
        type: "post",
        url: "/States/DistrictList",
        data: { stateID: $('#statelist').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            // console.log("Selected State Id:" + $('#statelist').val());
            // console.log(data);
            var distlist = '<option value="-1">Select District</option>';
            for (var i = 0; i < data.length; i++) {
                distlist = distlist + '<option value=' + data[i].distId + '>' + data[i].distName + '</option>';
            }
            $('#districtlist').html(distlist);
        }
    });

});

// for loading City List
$('#districtlist').change(function () {


    $.ajax({
        type: "post",
        url: "/States/CityList",
        data: { 'stateID': $('#statelist').val(), 'distID': $('#districtlist').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            // console.log("Selected State Id:" + $('#statelist').val());
            // console.log(data);
            var citylist = '<option value="-1">Select City</option>';
            for (var i = 0; i < data.length; i++) {
                citylist = citylist + '<option value=' + data[i].cityId + '>' + data[i].cityName + '</option>';
            }
            $('#citylist').html(citylist);
        }
    });

});

// for loading Police Station List
$('#citylist').change(function () {


    $.ajax({
        type: "post",
        url: "/States/PoliceStationList",
        data: { 'stateID': $('#statelist').val(), 'distID': $('#districtlist').val(), 'cityID': $('#citylist').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            // console.log("Selected State Id:" + $('#statelist').val());
            // console.log(data);
            var pslist = '<option value="-1">Select Police Station</option>';
            for (var i = 0; i < data.length; i++) {
                pslist = pslist + '<option value=' + data[i].stationID + '>' + data[i].stationName + '</option>';
            }
            $('#pslist').html(pslist);
        }
    });

});

$('#hotellist').change(function () {


    $.ajax({
        type: "post",
        url: "/States/HotelList",
        data: { 'stateID': $('#hotellist').val(), 'distID': $('#districtlist').val(), 'cityID': $('#citylist').val(), 'stationID': $('#hotellist').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            // console.log("Selected State Id:" + $('#statelist').val());
            // console.log(data);
            var hlist = '<option value="-1">Select Hotel</option>';
            for (var i = 0; i < data.length; i++) {
                hlist = hlist + '<option value=' + data[i].hotelRegNo + '>' + data[i].hotelName + '</option>';
            }
            $('#pslist').html(pslist);
        }
    });

});