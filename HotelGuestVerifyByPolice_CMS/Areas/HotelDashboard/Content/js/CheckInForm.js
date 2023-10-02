
$(document).ready(function () {
    clock();
    function clock() {

        var now = new Date();
        var date = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();
        var TwentyFourHour = now.getHours();
        var hour = now.getHours();
        var min = now.getMinutes();
        var sec = now.getSeconds();
        var mid = 'pm';
        if (sec < 10) {
            sec = "0" + sec;
        }
        if (min < 10) {
            min = "0" + min;
        }
        if (hour > 12) {
            hour = hour - 12;
        }
        if (hour < 10) {
            hour = "0" + hour;
        }
        if (hour == 0) {
            hour = 12;
        }
        if (TwentyFourHour < 12) {
            mid = 'am';
        }
        var dateTime = date + ' ' + hour + ':' + min + ' ' + mid;
        // setTimeout(clock, 1000);
        document.getElementById("currentTime").value = dateTime;
    }

    // For loading all Visit Purpose List
    $.ajax({
        type: "post",
        url: "/States/VisitPurposeList",
        datatype: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            vplist = '<option value="">Select Visit Purpose</option>';
            for (var i = 0; i < data.length; i++) {
                vplist = vplist + "<option value=" + data[i].id + ">" + data[i].purpose + "</option>";
            }
            //district = district + '</select>';
            $("#visitMain").html(vplist);
        },
    });

    // For loading all ID Proof Type List
    $.ajax({
        type: "post",
        url: "/States/IdProofTypeList",
        datatype: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            idprooflist = '<option value="">Select ID Proof</option>';
            for (var i = 0; i < data.length; i++) {
                idprooflist = idprooflist + "<option value=" + data[i].id + ">" + data[i].idProofType + "</option>";
            }
            //district = district + '</select>';
            $("#idtypeMain").html(idprooflist);
        },
    });

    // For loading all Active Country
    $.ajax({
        type: "post",
        url: "/States/CountryList",
        datatype: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            countrylist = '<option value="">Select Country</option>';
            for (var i = 0; i < data.length; i++) {
                countrylist = countrylist + "<option value=" + data[i].countryCode + ">" + data[i].countryName + "</option>";
            }
            //district = district + '</select>';
            $("#countryMain").html(countrylist);
        },
    });

    // for loading Country Wise State List
    $("#countryMain").change(function () {
        $.ajax({
            type: "post",
            url: "/States/CountryWiseStateList",
            data: { cCode: $("#countryMain").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var statelist = '<option value="">Select State</option>';
                for (var i = 0; i < data.length; i++) {
                    statelist = statelist + "<option value=" + data[i].stateId + ">" + data[i].stateName + "</option>";
                }
                $("#stateMain").html(statelist);
            },
        });
    });
    // For loading all Active States
    // $.ajax({
    //     type: "post",
    //     url: "/States/StateList",
    //     datatype: "json",
    //     traditional: true,
    //     success: function (data) {
    //         console.log(data);
    //         statelist = '<option value="">Select State</option>';
    //         for (var i = 0; i < data.length; i++) {
    //             statelist = statelist + "<option value=" + data[i].stateId + ">" + data[i].stateName + "</option>";
    //         }
    //         //district = district + '</select>';
    //         $("#stateMain").html(statelist);
    //     },
    // });


    // for loading District List
    $("#stateMain").change(function () {
        $.ajax({
            type: "post",
            url: "/States/DistrictList",
            data: { stateID: $("#stateMain").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var distlist = '<option value="">Select District</option>';
                for (var i = 0; i < data.length; i++) {
                    distlist = distlist + "<option value=" + data[i].distId + ">" + data[i].distName + "</option>";
                }
                $("#districtMain").html(distlist);
            },
        });
    });

    // for loading City List
    $("#districtMain").change(function () {
        $.ajax({
            type: "post",
            url: "/States/CityList",
            data: { stateID: $("#stateMain").val(), distID: $("#districtMain").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var citylist = '<option value="">Select City</option>';
                for (var i = 0; i < data.length; i++) {
                    citylist = citylist + "<option value=" + data[i].cityId + ">" + data[i].cityName + "</option>";
                }
                $("#cityMain").html(citylist);
            },
        });
    });



})
var video = document.getElementById('video');
var canvas = document.createElement('canvas');
var context = canvas.getContext('2d');

navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
    video.srcObject = stream;
    video.play();
}).catch(function (err) {
    console.log(err);
});

document.getElementById('capture').addEventListener('click', function () {
    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    // add time stamp
    const timeNow = new Date()
    context.fillStyle = '#000'
    context.fontSize = '10px'
    context.fillText(timeNow, 0, canvas.height - 5)

    var image = canvas.toDataURL();
    //uploadImage(image);
    //console.log(image)
    // image placeholder where the image will be displayed
    var imagecapture = document.getElementById('image-capture');
    var imagePlaceholder = document.getElementById('image-placeholder');
    
    // display the image in placeholder
    displayBase64Image(imagePlaceholder, imagecapture, image);
});

function displayBase64Image(placeholder, placeholdercapture, base64Image) {
    var image = document.createElement('img');
    var image1 = document.createElement('img');
    image.onload = function () {
        placeholdercapture.innerHTML = '';
        placeholdercapture.appendChild(this);
        
    }
    image1.onload = function () {
       
        placeholder.innerHTML = '';
        placeholder.appendChild(this);
    }
    image1.src = base64Image;
    image.src = base64Image;
}
function previewFile() {
    navigator.getMedia = (navigator.getUserMedia || // use the proper vendor prefix
        navigator.webkitGetUserMedia ||
        navigator.mozGetUserMedia ||
        navigator.msGetUserMedia);

    navigator.getMedia({ video: true }, function () {
        $("#addImage").modal({ backdrop: 'static', keyboard: false }, "show");
    }, function () {
        // webcam is not available
        alert("Web Cam Is Not Available");
    });


    
}
 $(function () {
     $('#image-placeholder').on('click', function () {
         $("#addImage").modal({ backdrop: 'static', keyboard: false }, "show");
     });
 });


function addAdult() {
    $("#addAdult").modal({ backdrop: 'static', keyboard: false }, "show");
}
function addChild() {
    $("#addChild").modal({ backdrop: 'static', keyboard: false }, "show");
}

const dataTable = $("#formdataTable").DataTable();
const childDataArray = [];
const addOnGuest = [];

$("#main-form").submit(function (e) {
    // alert()
    // debugger
    e.preventDefault();
    const firstName = $("#firstNameMain").val();
    const lastName = $("#lastNameMain").val();
    const mobile = $("#mobileMain").val();
    const age = $("#ageMain").val();
    const gender = $("#genderMain").val();
    const email = $("#emailMain").val();
    const country = $("#countryMain").val();
    const state = $("#stateMain").val();
    const city = $("#cityMain").val();
    const visitPurpose = $("#visitMain").val();
    const comingFrom = $("#comingMain").val();
    const guestIdType = $("#idtypeMain").val();
    var formMaindata = {
        // firstName : firstName,
        // lastName : lastName,
        hotelRegNo: "Collection123",
        guestName: firstName + " " + lastName,
        guestType: "Adult",
        gender: gender,
        email: email,
        country: country,
        state: state,
        city: city,
        numberOfGuest: 3,
        age: age,
        mobile: mobile,
        visitPurpose: visitPurpose,
        roomType: "AC",
        roomNo: 108,
        comingFrom: comingFrom,
        guestIdType: guestIdType,
        guestIDProof: "data:image/png;base64,iVBORw0KGg",
        guestPhoto: $('#image-placeholder img').attr('src'),
        paymentMode: "Cash",
    }

    // Save main data in localStorage
    localStorage.setItem("mainData", JSON.stringify(formMaindata));

    // Add main data as the first row in DataTable
    // dataTable.row.add([formMaindata.guestName, formMaindata.gender, formMaindata.age, formMaindata.city, formMaindata.state, formMaindata.comingFrom, formMaindata.guestIdType]).draw();


    console.log(formMaindata)
    document.querySelector('.formDetails').innerHTML += `
         <div class="card">
         <div class="d-flex">
         <div>
         <img src="${formMaindata.guestPhoto}"/>
         </div>
         <div style="margin-left:4%">
                 <h3><span>Name:</span>${formMaindata.guestName}</h3>
                     <h4><span>Age:</span>${formMaindata.age}</h4>
                     <p><span>Mobile No:</span> ${formMaindata.mobile}</p>
                     <p><span>Coming From:</span> ${formMaindata.comingFrom}</p>
                     </div>
                  </div>
           </div>
          `;
    $("#main-form").hide();
    $("#formTable").show();
    e.target.reset();

});

$("#formAdult").submit(function (e) {
    e.preventDefault();

    // Retrieve main data from localStorage
    const mainDataJSON = localStorage.getItem("mainData");
    const firstName = $("#firstNameAdult").val() + " " + $("#lastNameAdult").val();
    const age = $("#ageAdult").val();
    const gender = $("#genderAdult option:selected").text();;

    if (mainDataJSON) {
        const mainData = JSON.parse(mainDataJSON);
        var formChilddata = {
            guestName: $("#firstNameAdult").val() + " " + $("#lastNameAdult").val(),
            age: $("#ageAdult").val(),
            mobile: $("#mobileAdult").val(),
            relationWithGuest: $("#relationAdult").val(),
            guestType: "Adult",
            gender: $("#genderAdult").val(),
            email: $("#emailAdult").val(),
            country: $("#countryAdult").val(),
            state: $("#stateAdult").val(),
            city: $("#cityAdult").val(),
            comingFrom: $("#comingAdult").val(),
            guestIdType: $("#idTypeAdult").val(),
            guestIDProof: "data:image/png;base64,iVBORw0",
            guestPhoto: "data:image/jpeg;base64,/9j/4AAQSkZJ"
        }



        // Add child data as the second row in DataTable
        dataTable.row.add([formChilddata.guestName, formChilddata.gender, formChilddata.age, formChilddata.city, formChilddata.state, formChilddata.comingFrom, formChilddata.guestIdType]).draw();

        // Save the last child data in the array
        childDataArray.push(formChilddata);
        document.querySelector('.formDetails').innerHTML += `
             <div class="card">
                         <h2>${firstName}</h2>
                         <h3>${gender}</h3>
                         

               </div>
              `;
        // const formData = {
        //     mainData,
        //     addOnGuest: [
        //         childDataArray
        //     ]
        // };
        // console.log(formData)
        $("#addAdult").modal("hide");
        e.target.reset();
        // Clear the child form input

    } else {
        alert("Please submit main data first.");
    }
});

$("#formChild").submit(function (e) {
    e.preventDefault();

    // Retrieve main data from localStorage
    const mainDataJSON = localStorage.getItem("mainData");

    if (mainDataJSON) {
        const mainData = JSON.parse(mainDataJSON);
        var formChilddata = {
            guestName: $("#firstNameChild").val() + " " + $("#lastNameChild").val(),
            age: $("#ageChild").val(),

            relationWithGuest: $("#relationChild").val(),
            guestType: "Child",
            gender: $("#genderChild").val(),



            guestIDProof: "data:image/png;base64,iVBORw0",
            guestPhoto: "data:image/jpeg;base64,/9j/4AAQSkZJ"
        }



        // Add child data as the second row in DataTable
        dataTable.row.add([formChilddata.guestName, formChilddata.gender, formChilddata.age, formChilddata.city, formChilddata.state, formChilddata.comingFrom, formChilddata.guestIdType]).draw();

        // Save the last child data in the array
        childDataArray.push(formChilddata);
        // const formData = {
        //     mainData,
        //     addOnGuest: [
        //         childDataArray
        //     ]
        // };
        // console.log(formData)
        // Clear the child form input
        $(formChilddata).val("");
    } else {
        alert("Please submit main data first.");
    }
});

    // for (var i = 0; i < shopData.length; i++) {
    //     document.querySelector('.formDetails').innerHTML += `
    //   <div class="card">
    //       <img src="${shopData[i].image}" alt="">
    //       <h3>$${shopData[i].price}</h3>
    //       <h1> ${shopData[i].title}</h1>
    //       <p> ${shopData[i].description}</p>
    //       <button>Buy now</button>
    //    </div>
    //   `;

    // }