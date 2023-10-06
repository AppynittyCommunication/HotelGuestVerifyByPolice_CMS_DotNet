$(document).ready(function () {
    $("#mobile").keypress(function (e) {
        var $input = $(this),
        value = $input.val(),
        length = value.length
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 44) {
            //display error message
            //$("#err_mobile").html("केवल अंक | Digits Only").show().delay(1500).show().fadeOut('slow');
            $("#mobileerror").html("Digits Only").show().delay(1500).show().fadeOut("slow");
            return false;
        }
        if (length === 10) {
       return false
     }
    });
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
            $("#visitAdult").html(vplist);
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
            $("#idTypeAdult").html(idprooflist);
        },
    });
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
            $("#countryAdult").html(countrylist);
        },
    });
      $("#countryAdult").change(function () {
        $.ajax({
            type: "post",
            url: "/States/CountryWiseStateList",
            data: { cCode: $("#countryAdult").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var statelist = '<option value="">Select State</option>';
                for (var i = 0; i < data.length; i++) {
                    statelist = statelist + "<option value=" + data[i].stateId + ">" + data[i].stateName + "</option>";
                }
                $("#stateAdult").html(statelist);
            },
        });
    });
   
   

});


 var addOnGuestCount = parseInt($('#addOnGuestCount').val());
 var appendData=0
    $('#addAddOnGuest').click(function () {
        appendData++
         // For loading all Relation Type  List
    $.ajax({
        type: "post",
        url: "/States/RelationTypeList",
        datatype: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            relationlist = '<option value="">Select Relation</option>';
            for (var i = 0; i < data.length; i++) {
                relationlist = relationlist + "<option value=" + data[i].id + ">" + data[i].name + "</option>";
            }
            //district = district + '</select>';
            $("#relationAdult"+appendData).html(relationlist);
        },
    });
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
            
            $("#idTypeAdult"+appendData).html(idprooflist);
        },
    });
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
          alert("#countryAdult"+appendData)
            $("#countryAdult"+appendData).html(countrylist);
        },
    });
   $(function(){ /* DOM ready */
    $("#countryAdult"+appendData).change(function() {
      
        $.ajax({
            type: "post",
            url: "/States/CountryWiseStateList",
            data: { cCode: $("#countryAdult"+appendData).val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var statelist = '<option value="">Select State</option>';
                for (var i = 0; i < data.length; i++) {
                    statelist = statelist + "<option value=" + data[i].stateId + ">" + data[i].stateName + "</option>";
                }
                $("#stateAdult"+appendData).html(statelist);
            },
        });
    });
});
     $(function(){
       $("#stateAdult"+appendData).change(function () {
          
        $.ajax({
            type: "post",
            url: "/States/DistrictList",
            data: { stateID: $("#stateAdult"+appendData).val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var distlist = '<option value="">Select District</option>';
                for (var i = 0; i < data.length; i++) {
                    distlist = distlist + "<option value=" + data[i].distId + ">" + data[i].distName + "</option>";
                }
                $("#districtAdult"+appendData).html(distlist);
            },
        });
    });})
    $(function(){
       $("#districtAdult"+appendData).change(function () {
        $.ajax({
            type: "post",
            url: "/States/CityList",
            data: { stateID: $("#stateAdult"+appendData).val(), distID: $("#districtAdult"+appendData).val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                // console.log("Selected State Id:" + $('#statelist').val());
                // console.log(data);
                var citylist = '<option value="">Select City</option>';
                for (var i = 0; i < data.length; i++) {
                    citylist = citylist + "<option value=" + data[i].cityId + ">" + data[i].cityName + "</option>";
                }
                $("#cityAdult"+appendData).html(citylist);
            },
        });
    })

})
        $("#mainHotelGuest").hide();
        var name = $("#firstName").val();
       
        if(addOnGuestCount > 0){
        var hideOnCount=addOnGuestCount-1;
            
             $("#formAdult"+hideOnCount).hide();
             
        }
         
        var addOnGuestHtml = `
        
                    <div class="add-on-guest">
                          <!--  <h5>Add-On Guest ${addOnGuestCount}</h5>
                        <input type="text" class="form-control" name="addOnGuest[${addOnGuestCount}].firstName" placeholder="firstname"/>
                          <input type="text" class="form-control" name="addOnGuest[${addOnGuestCount}].lastName" placeholder="lastname"/>-->
                        <!-- Add form fields for other AddOnGuestSource properties -->
                         <div id="formAdult${addOnGuestCount}">
                    <h4>Adult ${addOnGuestCount}</h4>
                    <div class="d-flex">
                        <div class="form-group">
                         <input class="form-control" id="guestType"name="addOnGuest[${addOnGuestCount}].guestType" type="hidden" value="Adult" />
                            <div class="form-input">
                                <input class="form-control" id="firstNameAdult"name="addOnGuest[${addOnGuestCount}].firstName" placeholder="First Name" />
        
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-input">
                                <input class="form-control" id="lastNameAdult"name="addOnGuest[${addOnGuestCount}].lastName" placeholder="Last Name" />
                            </div>
                        </div>
                        <div class="form-group">

                           <div class="form-input">
                                <input class="form-control" id="mobileAdult"name="addOnGuest[${addOnGuestCount}].mobile" placeholder="Mobile" />
                           </div>

                        </div>
                    </div>
                    <div class="d-flex">

                        <div class="form-group">

                            <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].relationWithGuest" class="form-control" id="relationAdult${appendData}">
                                </select>

                            </div>

                        </div>
                        <div class="form-group">
                        <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].gender" class="form-control" id="genderAdult">
                                    <option value="">Select Gender</option>
                                    <option value="M">Male</option>
                                    <option value="F">Female</option>
                                </select>

                            </div>

                        </div>

                        <div class="form-group">
                      <div class="form-input">
                                <input class="form-control" id="ageAdult" placeholder="Age" name="addOnGuest[${addOnGuestCount}].age"/>
                            </div>

                        </div>
                    </div>

                    <div class="d-flex">
                        <div class="form-group">
                          <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].country" class="form-control" id="countryAdult${appendData}" >
                                    <option value="">Select Country</option>
                                    <option value="India">India</option>
                                </select>


                            </div>

                        </div>

                        <div class="form-group">
                            <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].state" class="form-control" id="stateAdult${appendData}">
                                    <option value="">Select State</option>
                                    <option value="Maharashtra">Maharashtra</option>
                                </select>
                            </div>

                        </div>

                        <div class="form-group">
                         <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].district" class="form-control" id="districtAdult${appendData}">
                                   
                                </select>


                            </div>

                        </div>
                        <div class="form-group">
                          <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].city" class="form-control" id="cityAdult${appendData}">
                                    <option value="">Select City</option>
                                    <option value="Nagpur">Nagpur</option>
                                </select>

                            </div>

                        </div>

                    </div>
                    <div class="d-flex">
                      
                       <div class="form-group">
                            <div class="form-input">
                                <input class="form-control" name="addOnGuest[${addOnGuestCount}].comingFrom" placeholder="Coming From" id="comingAdult" />


                            </div>

                        </div>
                        <div class="form-group">

                            <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].guestIdType" class="form-control" id="idTypeAdult${appendData}">
                                    <option value="">Select ID Type</option>
                                    <option value="Adhar">Adhar Card</option>
                                </select>

                            </div>

                        </div>
                    </div>
                    <div class="d-flex">
                        <div class="profile-pic">

                            <img alt="User Pic" src="/Areas/HotelDashboard/Content/Images/Icon/Screenshot 2023-09-28 143659.png" id="profile-image1" data-toggle="modal" data-target="#addImage" height="200">
                            <input id="profile-image-upload" class="hidden" type="file" >
                            <div style="color:#999;">  </div>
                            <p style="margin-top: auto;margin-bottom: auto;">Take Photo</p>
                        </div>
                        <div class="profile-pic">

                            <img alt="User Pic" src="/Areas/HotelDashboard/Content/Images/Icon/Screenshot 2023-09-28 143659.png" id="profile-image1_ID" height="200">
                            <input id="profile-image-upload_ID" class="hidden" type="file" onchange="previewFile_ID()">
                            <div style="color:#999;">  </div>
                            <p style="margin-top: auto;margin-bottom: auto;">Take ID Photo</p>
                        </div>
                    </div>
                    <div class="text-center">
                        <button class="addbtn btn btn-md" type="button">Done</button>
                    </div>
                </div>
                    </div>
                `;

        $('#addOnGuests').append(addOnGuestHtml);
      
        addOnGuestCount++;
        $('#addOnGuestCount').val(addOnGuestCount); // Update the count in the hidden field
    });
    $('#addAddOnChild').click(function () {
        appendData++
         // For loading all Relation Type  List
   
        $("#mainHotelGuest").hide();
        var name = $("#firstName").val();
       
        if(addOnGuestCount > 0){
        var hideOnCount=addOnGuestCount-1;
            
             $("#formAdult"+hideOnCount).hide();
             
        }
         
        var addOnGuestHtml = `
        
                    <div class="add-on-guest">
                          <!--  <h5>Add-On Guest ${addOnGuestCount}</h5>
                        <input type="text" class="form-control" name="addOnGuest[${addOnGuestCount}].firstName" placeholder="firstname"/>
                          <input type="text" class="form-control" name="addOnGuest[${addOnGuestCount}].lastName" placeholder="lastname"/>-->
                        <!-- Add form fields for other AddOnGuestSource properties -->
                         <div id="formAdult${addOnGuestCount}">
                    <h4>Adult ${addOnGuestCount}</h4>
                    <div class="d-flex">
                        <div class="form-group">
                         <input class="form-control" id="guestType"name="addOnGuest[${addOnGuestCount}].guestType" type="hidden" value="Child" />
                            <div class="form-input">
                                <input class="form-control" id="firstNameAdult"name="addOnGuest[${addOnGuestCount}].firstName" placeholder="First Name" />
        
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-input">
                                <input class="form-control" id="lastNameAdult"name="addOnGuest[${addOnGuestCount}].lastName" placeholder="Last Name" />
                            </div>
                        </div>
                  
                    </div>
                    <div class="d-flex">

                        <div class="form-group">

                            <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].relationWithGuest" class="form-control" id="relationAdult${appendData}">
                                </select>

                            </div>

                        </div>
                        <div class="form-group">
                        <div class="form-input">

                                <select name="addOnGuest[${addOnGuestCount}].gender" class="form-control" id="genderAdult">
                                    <option value="">Select Gender</option>
                                    <option value="M">Male</option>
                                    <option value="F">Female</option>
                                </select>

                            </div>

                        </div>

                        <div class="form-group">
                      <div class="form-input">
                                <input class="form-control" id="ageAdult" placeholder="Age" name="addOnGuest[${addOnGuestCount}].age"/>
                            </div>

                        </div>
                    </div>

                   
                    <div class="d-flex">
                        <div class="profile-pic">

                            <img alt="User Pic" src="/Areas/HotelDashboard/Content/Images/Icon/Screenshot 2023-09-28 143659.png" id="profile-image1" data-toggle="modal" data-target="#addImage" height="200">
                            <input id="profile-image-upload" class="hidden" type="file" >
                            <div style="color:#999;">  </div>
                            <p style="margin-top: auto;margin-bottom: auto;">Take Photo</p>
                        </div>
                       
                    </div>
                    <div class="text-center">
                        <button class="addbtn btn btn-md" type="button">Done</button>
                    </div>
                </div>
                    </div>
                `;

        $('#addOnGuests').append(addOnGuestHtml);
      
        addOnGuestCount++;
        $('#addOnGuestCount').val(addOnGuestCount); // Update the count in the hidden field
    });



 function showCard(){
        alert()
      
       
  var name = $("#firstName").val();
  var lastname = $("#lastName").val();
  var mobile = $("#mobile").val();
  var email = $("#email").val();
   var age = $("#age").val();
  var gender = $("#genderMain").val();
  var country = $("#countryMain").val();
  var state = $("#stateMain").val();
  var district = $("#districtMain").val();
  var city = $("#cityMain").val();
  var visitPurpose = $("#visitMain").val();
  var comingForm = $("#comingFrom").val();
  var idProof = $("#idtypeMain").val();
  var valid = 0;
  var pattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
 
  /* Name validation */
  
   if (name==""){
    document.getElementById("nameError").innerHTML = "Pleae Enter First Name";
  } else {       document.getElementById("nameError").innerHTML = "";
    valid++;
  }
   if (lastname == ""){
    document.getElementById("lastError").innerHTML = "Pleae Enter Last Name";
  } else {
    document.getElementById("lastError").innerHTML = "";
    valid++;
  }
   /* Mobile validation */
  if (mobile == ""){
    document.getElementById("mobileError").innerHTML = "Please Enter Mobile Number";
  } else {
    document.getElementById("mobileError").innerHTML = "";
    valid++;
  }
   /* Email validation */
  if (email == ""){
    document.getElementById("emailError").innerHTML = "Please Enter Email";
  } else if(email.match(pattern)) {
     document.getElementById("emailError").innerHTML = "";
     valid++;
   }else{
       document.getElementById("emailError").innerHTML = "Please Enter Valid  Email";
   
   }
   /* Gender validation */
  if (gender == ""){
    document.getElementById("genderError").innerHTML = "Please Select Gender";
  } else {
    document.getElementById("genderError").innerHTML = "";
    valid++;
  }
  if (age == ""){
    document.getElementById("ageError").innerHTML = "Please Enter Age";
  } 
  else if (age > 100 || age < 18){
    document.getElementById("ageError").innerHTML = "Age must be between 18 and 100.";
  }  else {
    document.getElementById("ageError").innerHTML = "";
    valid++;
  }
  if (country == ""){
    document.getElementById("countryError").innerHTML = "Please Select Country";
  } else {
    document.getElementById("countryError").innerHTML = "";
    valid++;
  }
  if (state == ""){
    document.getElementById("stateError").innerHTML = "Please Select State";
  } else {
    document.getElementById("stateError").innerHTML = "";
    valid++;
  }
  if (district == ""){
    document.getElementById("districtError").innerHTML = "Please Select District";
  } else {
    document.getElementById("districtError").innerHTML = "";
    valid++;
  }
  if (city == ""){
    document.getElementById("cityError").innerHTML = "Please Select City";
  } else {
    document.getElementById("cityError").innerHTML = "";
    valid++;
  }
   if (visitPurpose == ""){
    document.getElementById("visitError").innerHTML = "Please Select Visit purpose";
  } else {
    document.getElementById("visitError").innerHTML = "";
    valid++;
  }
  if (comingForm == ""){
    document.getElementById("comeingformError").innerHTML = "Please Enter Coming Form";
  } else {
    document.getElementById("comeingformError").innerHTML = "";
    valid++;
  }
  if (idProof == ""){
    document.getElementById("idproofError").innerHTML = "Please Select ID Proof";
  } else {
    document.getElementById("idproofError").innerHTML = "";
    valid++;
  }
  /* Age validation */
  
  
  
  /* Hometown validation */
  
  
  
  /* Final validation */
  
   if (valid == 6)  {
       alert('u')
     
  }  else {
        return false
     }
 
  
        $("#mainHotelGuest").hide();
        $("#finalData").show();

         document.querySelector('#displayFormData').innerHTML += `
         <div class="card">
         <div class="d-flex">
         <div>
        
         </div>
         <div style="margin-left:4%">
                 <h3><span>Name:</span>${$("#firstName").val() }</h3>
                    
                     </div>
                  </div>
           </div>
          `;
    }
    function showCardAdult(){
        
        $("#mainHotelGuest").hide();
        $("#finalData").show();

         document.querySelector('#displayFormData').innerHTML += `
         <div class="card">
         <div class="d-flex">
         <div>
        
         </div>
         <div style="margin-left:4%">
                 <h3><span>Name:</span>${$("#firstName").val() }</h3>
                    
                     </div>
                  </div>
           </div>
          `;
    }
       function showCardChild(){
        alert()
        $("#mainHotelGuest").hide();
        $("#finalData").show();

         document.querySelector('#displayFormData').innerHTML += `
         <div class="card">
         <div class="d-flex">
         <div>
        
         </div>
         <div style="margin-left:4%">
                 <h3><span>Name:</span>${$("#firstName").val() }</h3>
                    
                     </div>
                  </div>
           </div>
          `;
    }
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
    document.getElementById('guestPhoto').value = base64Image;
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




    //Guest Id Proof Photo

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
    document.getElementById('guestPhoto').value = base64Image;
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