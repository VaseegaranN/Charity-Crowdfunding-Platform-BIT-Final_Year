<?php
    // Check if the form is submitted
    if ($_SERVER["REQUEST_METHOD"] === "POST") {
        // Get the form data
        $ngo_user_name = $_POST["ngo_user_name"];
        $ngo_user_position = $_POST["ngo_user_position"];
        $ngo_user_phone = $_POST["ngo_user_phone"];
        $ngo_user_email = $_POST["ngo_user_email"];
        $ngo_user_pwd = $_POST["ngo_user_pwd"];
        $ngo_org_name = $_POST["ngo_org_name"];
        $ngo_org_place = $_POST["ngo_org_place"];
        $ngo_org_phone = $_POST["ngo_org_phone"];
        $ngo_org_mail = $_POST["ngo_org_mail"];
        $childhood_pet = $_POST["childhood_pet"];
        $vacation_destination= $_POST["vacation_destination"];
        $favourite_hobby= $_POST["favourite_hobby"];

        // Handle image upload
        if (!empty($_FILES["ngo_id_image"]["tmp_name"])) {
            $imageData = addslashes(file_get_contents($_FILES["ngo_id_image"]["tmp_name"]));
        } else {
            $imageData = null;
        }

        // Create a connection to the database
        $conn = new mysqli("localhost", "root", "", "crowdfund");

        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        // Generate a unique ID
        $ngo_id = uniqid();

        // Prepare and execute the SQL statement to insert the data
        $sql = "INSERT INTO ngo_details (ngo_id, ngo_user_name, ngo_user_position, ngo_user_phone, ngo_user_email, ngo_user_pwd, ngo_org_name, ngo_org_place, ngo_org_phone, ngo_org_mail, ngo_id_image,childhood_pet,vacation_destination,favourite_hobby) VALUES ('$ngo_id', '$ngo_user_name', '$ngo_user_position', '$ngo_user_phone', '$ngo_user_email', '$ngo_user_pwd', '$ngo_org_name', '$ngo_org_place', '$ngo_org_phone', '$ngo_org_mail', '$imageData',' $childhood_pet','$vacation_destination','$favourite_hobby')";
        if ($conn->query($sql) === TRUE) {
            echo "<script>alert('Registration successful! Account Approval is Pending,will be updated shortly within 2 hours.Kindly Wait.');</script>";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }

        // Close the database connection
        $conn->close();
    }
    ?>
    <!DOCTYPE html>
    <html>
    <!-- https://localhost/districtcare/public/main_login.php -->

    <head>
        <title>NGO Registration</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link href="https://fonts.googleapis.com/css2?family=Roboto+Slab:wght@300&display=swap" rel="stylesheet">
        <link rel="icon" href="../images/urbanlink-logo.png" type="image/icon type">
        <style>
            @import url('https://fonts.googleapis.com/css?family=Poppins:400,500,600,700&display=swap');
*{
  margin: 0;
  padding: 0;
  outline: none;
  box-sizing: border-box;
  font-family: 'Poppins', sans-serif;
}
body{
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  padding: 10px;
  font-family: 'Poppins', sans-serif;
  background-color: #6247aa;
}
.container{
  max-width: 800px;
  background: #fff;
  width: 800px;
  padding: 25px 40px 10px 40px;
  box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
}
.container .text{
  text-align: center;
  font-size: 41px;
  font-weight: 600;
  font-family: 'Poppins', sans-serif;
  background: -webkit-linear-gradient(right, #56d8e4, #9f01ea, #56d8e4, #9f01ea);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
}
.container form{
  padding: 30px 0 0 0;
}
.container form .form-row{
  display: flex;
  margin: 32px 0;
}
form .form-row .input-data{
  width: 80%;
  height: 40px;
  margin: 0 10px;
  position: relative;
}
form .form-row .id-image{
  width: 80%;
  height: 40px;
  margin: 0 10px;
  position: relative;
}

form .form-row .textarea{
  height: 70px;
}
.input-data input,
.textarea textarea{
  display: block;
  width: 100%;
  height: 100%;
  border: none;
  font-size: 17px;
  border-bottom: 2px solid rgba(0,0,0, 0.12);
}
.input-data input:focus ~ label, .textarea textarea:focus ~ label,
.input-data input:valid ~ label, .textarea textarea:valid ~ label{
  transform: translateY(-20px);
  font-size: 14px;
  color: #780d9b;
}
.textarea textarea{
  resize: none;
  padding-top: 10px;
}
.input-data label{
  position: absolute;
  pointer-events: none;
  bottom: 10px;
  font-size: 16px;
  transition: all 0.3s ease;
}


.textarea label{
  width: 100%;
  bottom: 40px;
  background: #fff;
}
.input-data .underline{
  position: absolute;
  bottom: 0;
  height: 2px;
  width: 100%;
}
.input-data .underline:before{
  position: absolute;
  content: "";
  height: 2px;
  width: 100%;
  background: #780d9b;
  transform: scaleX(0);
  transform-origin: center;
  transition: transform 0.3s ease;
}
.input-data input:focus ~ .underline:before,
.input-data input:valid ~ .underline:before,
.textarea textarea:focus ~ .underline:before,
.textarea textarea:valid ~ .underline:before{
  transform: scale(1);
}
.submit-btn .input-data{
  overflow: hidden;
  height: 45px!important;
  width: 25%!important;
}
.submit-btn .input-data .inner{
  height: 100%;
  width: 300%;
  position: absolute;
  left: -100%;
  background: -webkit-linear-gradient(right, #56d8e4, #9f01ea, #56d8e4, #9f01ea);
  transition: all 0.4s;
}
.submit-btn .input-data:hover .inner{
  left: 0;
}
.submit-btn .input-data input{
  background: none;
  border: none;
  color: #fff;
  font-size: 17px;
  font-weight: 500;
  text-transform: uppercase;
  letter-spacing: 1px;
  cursor: pointer;
  position: relative;
  z-index: 2;
}
@media (max-width: 700px) {
  .container .text{
    font-size: 30px;
  }
  .container form{
    padding: 10px 0 0 0;
  }
  .container form .form-row{
    display: block;
  }
  form .form-row .input-data{
    margin: 35px 0!important;
  }
  .submit-btn .input-data{
    width: 40%!important;
  }
}

#ngo_org_mail {
  
}

            .error-message-password {
                border: 1px solid white;
                border-radius: 5px;
                padding: 5px 10px;
                color: #4CAF50;
                -webkit-animation: NAME-YOUR-ANIMATION 1s infinite;
                -moz-animation: NAME-YOUR-ANIMATION 1s infinite;
                -o-animation: NAME-YOUR-ANIMATION 1s infinite;
                animation: NAME-YOUR-ANIMATION 1s infinite;
                text-align: center;
                font-weight: 700;
            }

            .password-conditions {
                position: fixed;
                background: white;
                border: 2px solid #ccc;
                padding: 10px;
                left: 200px;
                border-radius: 5px;
                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                color: red;
            }

            .password-conditions ul {
                list-style: none;
                padding: 0;
            }

            .back-button {
                position: relative;
                top: 10px;
                margin-left: 10px;
            }

           

            .form-group {
                margin-bottom: 20px;
            }

            li:nth-child(even) {
                color: #ffa500;
            }

            .form-group label {
                display: block;
                font-weight: bold;
                margin-bottom: 5px;
            }

            .form-group input {
                width: 100%;
                padding: 8px;
                border: none;
                border-bottom: 2px solid black;
                font-size: 16px;
                /* border-radius: 5px;3 */
                box-sizing: border-box;

            }

            .form-group select {
                width: 100%;
                padding: 8px;
                border: 2px solid black;
                font-size: 16px;
                border-radius: 5px;
                box-sizing: border-box;

            }

            .form-group input[type="submit"] {
                background-color: #ffa500;
                border-radius: 5px;
                color: white;
                cursor: pointer;
                border: none !important;
                width: 80%;
                transition: 0.3s ease-in;
                background-image: linear-gradient(to right, #780d9b 45%, orange 55%);
                background-size: 220% 100%;
                background-position: 100% 50%;

                &:hover {
                    background-position: 0% 50%;
                }
            }

            .form-group input[type="submit"]:hover {
                width: 100%;
                /* background-color: #37ff00; */

            }
            

            .slider {
                position: relative;
                width: 100%;
                height: 300px;
                margin: 50px auto;
                box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 1px 5px 0 rgba(0, 0, 0, 0.12),
                    0 3px 1px -2px rgba(0, 0, 0, 0.2);
                overflow: hidden;
}

.slider-controls {
  position: absolute;
  bottom: 0px;
  left: 50%;
  width: 200px;
  text-align: center;
  transform: translatex(-50%);
  z-index: 1000;

  list-style: none;
  text-align: center;
}

.slider input[type="radio"] {
  position: absolute;
  top: 0;
  left: 0;
  opacity: 0;
  width: 0;
  height: 0;
}

.slider-controls label {
  display: inline-block;
  border: none;
  height: 20px;
  width: 20px;
  border-radius: 50%;
  cursor: pointer;
  background-color: #212121;
  transition: background-color 0.2s linear;
}

#btn-1:checked ~ .slider-controls label[for="btn-1"] {
  background-color: #780d9b;
}

#btn-2:checked ~ .slider-controls label[for="btn-2"] {
  background-color: #780d9b;
}

#btn-3:checked ~ .slider-controls label[for="btn-3"] {
  background-color: #780d9b;
}

/* SLIDES */

.slides {
  list-style: none;
  padding: 0;
  margin: 0;
  height: 100%;
}

.slide {
  position: absolute;
  top: 0;
  left: 0;

  display: flex;
  justify-content: space-between;
  padding: 20px;
  width: 100%;
  height: 100%;

  opacity: 0;
  transform: translatex(-100%);
  transition: transform 250ms linear;
}

.slide-content {
  width: 400px;
}

.slide-title {
  margin-bottom: 20px;
  font-size: 30px;
}

.slide-text {
  margin-bottom: 20px;
}

.slide-link {
  display: inline-block;
  padding: 10px 20px;
  color: #fff;
  border-radius: 3px;
  text-decoration: none;
  background-color: #ff4081;
}

.slide-image img {
  max-width: 100%;
}

/* Slide animations */
#btn-1:checked ~ .slides .slide:nth-child(1) {
  transform: translatex(0);
  opacity: 1;
}

#btn-2:checked ~ .slides .slide:nth-child(2) {
  transform: translatex(0);
  opacity: 1;
}

#btn-3:checked ~ .slides .slide:nth-child(3) {
  transform: translatex(0);
  opacity: 1;
}

#btn-1:not(:checked) ~ .slides .slide:nth-child(1) {
  animation-name: swap-out;
  animation-duration: 300ms;
  animation-timing-function: linear;
}

#btn-2:not(:checked) ~ .slides .slide:nth-child(2) {
  animation-name: swap-out;
  animation-duration: 300ms;
  animation-timing-function: linear;
}

#btn-3:not(:checked) ~ .slides .slide:nth-child(3) {
  animation-name: swap-out;
  animation-duration: 300ms;
  animation-timing-function: linear;
}

/* necessary to give position: relative to parent. */
/* Import Google font - Poppins */
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: "Poppins", sans-serif;
}
.input-field {
  position: relative;
}
.input-field input {
  width: 350px;
  height: 60px;
  border-radius: 6px;
  font-size: 18px;
  padding: 0 15px;
  border: 2px solid #212121;
  background: transparent;
  color: #212121;
  outline: none;
}
input:focus {
  border: 2px solid #780d9b;
}
input:focus ~ label,
input:valid ~ label {
  top: 0;
  left: 15px;
  font-size: 16px;
  padding: 0 2px;
  background: #ffffff;
}

/* necessary to give position: relative to parent. */


@keyframes swap-out {
  0% {
    transform: translatex(0);
    opacity: 1;
  }

  50% {
    transform: translatex(50%);
    opacity: 0;
  }

  100% {
    transform: translatex(100%);
  }
}


@media (max-width: 700px) {
  .container .text{
    font-size: 30px;
  }
  .container form{
    padding: 10px 0 0 0;
  }
  .container form .form-row{
    display: block;
  }
  form .form-row .input-data{
    margin: 35px 0!important;
  }
  .submit-btn .input-data{
    width: 40%!important;
  }
}

            .note-flash {
                border: 1px solid white;
                border-radius: 5px;
                padding: 5px 10px;
                color: #4CAF50;
                -webkit-animation: NAME-YOUR-ANIMATION 1s infinite;
                /* Safari 4+ */
                -moz-animation: NAME-YOUR-ANIMATION 1s infinite;
                /* Fx 5+ */
                -o-animation: NAME-YOUR-ANIMATION 1s infinite;
                /* Opera 12+ */
                animation: NAME-YOUR-ANIMATION 1s infinite;
                /* IE 10+, Fx 29+ */
            }

            @-webkit-keyframes NAME-YOUR-ANIMATION {

                0%,
                49% {
                    background-color: #000;

                }

                50%,
                100% {
                    background-color: #e50000;

                }
            }



            @media only screen and (min-width:901px) and (max-width: 1100px) {
                body {
                    background-position: -20px 400px;
                    background-size: cover;
                    margin: 0;
                    padding: 0;
                    overflow-y: scroll;
                    width: 100%;
                }

                .instructions {
                    height: 720px;
                    width: 300px;
                    position: relative;
                    top: 90px;
                    /* left: 50px; */
                }

                .form-container {
                    height: 1100px;
                    width: 450px;
                    position: relative;
                    top: 150px;
                    left: 20px;
                }
            }

            @media only screen and (min-width:701px) and (max-width: 900px) {
                body {
                    background-position: -20px 400px;
                    background-size: cover;
                    margin: 0;
                    padding: 0;
                    overflow-y: scroll;
                    width: 100%;
                }

                .form-container {
                    width: 60%;
                    position: relative;
                    top: 180px;
                    height: 1150px;
                    margin: 50px;
                    left: 120px;
                }

                .form-group {
                    position: relative;
                    left: 50px;
                }

                .form-group input[type="text"],
                .form-group input[type="email"],
                .form-group input[type="date"],
                .form-group input[type="password"],
                .form-group select,
                .form-group textarea {
                    width: 80%;
                    /* max-width: 100%; */
                }

                .form-group input[type="file"] {
                    width: 80%;
                }

                .mpin-inputs {
                    width: 100%;
                }

                .btn,
                .note-flash {
                    position: relative;
                    left: -50px;
                }

                .instructions {
                    display: none;
                }
            }


            @media only screen and (min-width:501px) and (max-width:700px) {
                body {
                    background-position: -100px 200px;
                    background-size: cover;
                    margin: 0;
                    padding: 0;
                    /* height: 100%; */
                    overflow-y: scroll;
                    width: 100%;
                }

                .form-container {
                    width: 85%;
                    position: relative;
                    top: 180px;
                    height: 1150px;
                    margin: 50px;
                }

                .form-group input[type="text"],
                .form-group input[type="email"],
                .form-group input[type="date"],
                .form-group input[type="password"],
                .form-group select,
                .form-group textarea {
                    width: 100%;
                    max-width: 100%;
                }

                .form-group input[type="date"] {
                    width: 92%;
                }

                .mpin-inputs {
                    width: 100%;
                }

                .btn {
                    position: relative;
                    top: 0px;
                }

                .instructions {
                    display: none;
                }
            }

            @media only screen and (min-width:300px) and (max-width:500px) {
                body {
                    background-image: none;
                    background-color: #435f75;
                    margin: 0;
                    padding: 0;
                    height: 100%;
                    overflow-y: scroll;
                    width: 100%;
                }

                .form-container {
                    width: 85%;
                    position: relative;
                    top: 180px;
                    height: 1140px;
                }

                .form-group input[type="text"],
                .form-group input[type="email"],
                .form-group input[type="date"],
                .form-group input[type="password"],
                .form-group select,
                .form-group textarea {
                    width: 100%;
                    max-width: 100%;
                }

                .form-group input[type="date"] {
                    width: 92%;
                }

                .mpin-inputs {
                    width: 100%;
                }

                .btn {
                    position: relative;
                    top: 0px;
                }

                .instructions {
                    display: none;
                }
            }
        </style>
        <script>
            function confirmRegistration() {
                return confirm("Are you sure you want to register?");
            }
        </script>
        <script>
            function validateForm() {
                var password = document.getElementById("ngo_user_pwd").value;
                var conditionsMet = true;
                var conditionsRegex = [
                    /.{8,}/, // Minimum 8 characters
                    /[A-Z]/, // Minimum 1 uppercase letter
                    /[a-z]/, // Minimum 1 lowercase letter
                    /\d/, // Minimum 1 number
                    /[!@#\$%\^&\*\(\)_\+{}\|:"<>\?]/ // Minimum 1 special character
                ];

                for (var i = 0; i < conditionsRegex.length; i++) {
                    if (!password.match(conditionsRegex[i])) {
                        conditionsMet = false;
                        break;
                    }
                }

                if (!conditionsMet) {
                    var errorDiv = document.getElementById("password-error");
                    errorDiv.style.display = "block";
                    // Hide the error message after 3 seconds
                    setTimeout(function() {
                        errorDiv.style.display = "none";
                    }, 3000);

                    // Prevent the form from being submitted
                    return false;
                }

                return true; // Allow form submission if conditions are met
            }
        </script>
    </head>

    <body>
        <!-- <input class="goback" type="button" value="Go Back" onclick="history.back()"> -->
        <!-- <div class="back-button">
            <a href="../index.php" class="go-back">⬅️ GO BACK</a>
        </div> -->

        <div class="container">
            <div class="form-container">
                <script>
                    function combinedSubmit() {
                        if (confirmRegistration() && validateForm()) {
                            return true; // If both functions return true, the form will be submitted
                        } else {
                            return false; // If any function returns false, the form submission will be cancelled
                        }
                    }
                </script>
                <h2 style="text-align: center;">NGO SignUp</h2>
                <form method="POST" action="ngo_signup.php" onsubmit="return combinedSubmit()" enctype="multipart/form-data">
                    <div class="form-row">
                      <div class="input-data">
                         <input type="text" id="ngo_user_name" name="ngo_user_name" required>
                         <div class="underline"></div>
                         <label for="ngo_user_name">User Name *</label>
                     </div>
                      <div class="input-data">
                        <input type="text" id="ngo_user_position" name="ngo_user_position" required>
                        <div class="underline"></div>
                        <label for="ngo_user_position">User Position *</label>
                      </div>
                    </div>
                    <div class="form-row">
                        <div class="input-data">
                            <input type="text" id="ngo_user_phone" name="ngo_user_phone" pattern="[0-9]{10}" title="Please enter exactly 10 digits."  oninput="validatePhoneNumber(this)" required>
                            <div class="underline"></div>
                            <label for="ngo_user_phone">User Phone *</label>
                        </div>
                        <div class="input-data">
                            <input type="email" id="ngo_user_email" name="ngo_user_email" required>
                            <div class="underline"></div>
                             <label for="ngo_user_email">User Email *</label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="input-data">
                            <input type="password" id="ngo_user_pwd" name="ngo_user_pwd" onfocus="showPasswordConditions()" oninput="checkPassword()" required>
                            <div class="underline"></div>
                            <label for="ngo_user_pwd">User Password *</label>
                            <div id="password-conditions" class="password-conditions" style="display: none;">
                                <ul>
                                 <li id="length">Minimum 8 characters</li>
                                 <li id="uppercase">Minimum 1 uppercase letter</li>
                                 <li id="lowercase">Minimum 1 lowercase letter</li>
                                 <li id="number">Minimum 1 number</li>
                                 <li id="special">Minimum 1 special character</li>
                                </ul>
                           </div>
                        </div>
                        <div class="input-data">
                            <input type="text" id="ngo_org_name" name="ngo_org_name" required>
                            <div class="underline"></div>
                            <label for="ngo_org_name">Organization Name *</label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="input-data">
                            <input type="text" id="ngo_org_place" name="ngo_org_place" required>
                            <div class="underline"></div>
                            <label for="ngo_org_place">Organization Place *</label>
                        </div>
                        <div class="input-data">
                            <input type="text" id="ngo_org_phone" name="ngo_org_phone" pattern="[0-9]{10}" oninput="validatePhoneNumber(this)" required>
                            <div class="underline"></div>
                            <label for="ngo_org_phone">Organization Phone *</label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="id-image">
                        <label for="ngo_id_image">NGO ID Image *</label>  
                        <input type="file" id="ngo_id_image" name="ngo_id_image" accept=".jpg, .jpeg, .png">    
                        </div> 
                        <div class="input-data">
                            <input type="email" id="ngo_org_mail" name="ngo_org_mail" required>
                            <label for="ngo_org_mail">Organization Email *</label>
                        </div>               
                    </div>
                    <div class="security-questions">
                        <p>Answer the given three security questions..</p><br>
                        <div class="slider">
                            <input type="radio" name="toggle" id="btn-1" checked>
                            <input type="radio" name="toggle" id="btn-2">
                            <input type="radio" name="toggle" id="btn-3">
                        
                            <div class="slider-controls">
                            <label for="btn-1"></label>
                            <label for="btn-2"></label>
                            <label for="btn-3"></label>
                            </div>
                        
                            <ul class="slides">
                            <li class="slide">
                                <div class="slide-content">
                                <h2 class="slide-title">Question #1</h2>
                                <label for="childhood_pet" style="font-size: 20px;">What was the name of your childhood pet?</label>
                                  <div class="input-field">
                                    <input type="text" id="childhood_pet" name="childhood_pet" spellcheck="false" required>
                                  </div>
                                </div>
                            </li>
                            <li class="slide">
                                <div class="slide-content">
                                <h2 class="slide-title" style="color: #000;">Question #2</h2>
                                <label for="vacation_destination" style="font-size: 20px; color: #000;">What is your dream vacation destination?</label>
                                  <div class="input-field">
                                    <input type="text" id="vacation_destination" name="vacation_destination" spellcheck="false" required>
                                  </div>
                                </div>
                            </li>
                            <li class="slide">
                                <div class="slide-content">
                                <h2 class="slide-title">Question #3</h2>
                                <label for="favourite_hobby" style="font-size: 20px;">What is your favorite hobby?</label>
                                  <div class="input-field">
                                    <input type="text" id="favourite_hobby" name="favourite_hobby" spellcheck="false" required>
                                  </div>
                                </div>
                            </li>
                            </ul>
                        </div>
                    </div>
                    <div class="form-group">
                        <p class="note-flash">NOTE: THE ABOVE QUESTIONS ARE FOR SECURITY PURPOSE,IT WILL BE ASKED FOR SECURITY REASONS. KINDLY NOTE IT DOWN.</p>
                    </div>
                    <div class="form-group">
                        <input type="submit" class="btn" value="Register">
                    </div>
                </form>
                <div id="password-error" class="error-message-password" style="display:none;">
                    <span>❌</span>
                    <p>Password must meet the specified criteria.</p>
                </div>
            </div>
        </div>
        <script>
            function validatePhoneNumber(input) {
                // Remove non-numeric characters
                input.value = input.value.replace(/\D/g, '');

                // Limit the length to 10 characters
                if (input.value.length > 10) {
                    input.value = input.value.slice(0, 10);
                }
            }
        </script>
        <script>
            function showPasswordConditions() {
                var passwordConditions = document.getElementById("password-conditions");
                passwordConditions.style.display = "block";
            }

            function hidePasswordConditions() {
                var passwordConditions = document.getElementById("password-conditions");
                passwordConditions.style.display = "none";
            }

            function checkPassword() {
                var password = document.getElementById("ngo_user_pwd").value;
                var conditions = document.getElementById("password-conditions").getElementsByTagName("li");

                var allConditionsMet = true;
                for (var i = 0; i < conditions.length; i++) {
                    if (password.match(conditionsRegex[i])) {
                        conditions[i].style.textDecoration = "line-through";
                        conditions[i].style.color = "green";
                    } else {
                        conditions[i].style.textDecoration = "none";
                        conditions[i].style.color = "red";
                        allConditionsMet = false;
                    }
                }

                if (allConditionsMet) {
                    hidePasswordConditions();
                }
            }
            // Define regular expressions for conditions
            var conditionsRegex = [
                /.{8,}/, // Minimum 8 characters
                /[A-Z]/, // Minimum 1 uppercase letter
                /[a-z]/, // Minimum 1 lowercase letter
                /\d/, // Minimum 1 number
                /[!@#\$%\^&\*\(\)_\+{}\|:"<>\?]/ // Minimum 1 special character
            ];

            document.addEventListener('click', function(e) {
                var passwordField = document.getElementById("ngo_user_pwd");
                var passwordConditions = document.getElementById("password-conditions");

                if (e.target !== passwordField && e.target !== passwordConditions) {
                    hidePasswordConditions();
                }

            });

            // FOR MPIN
            document.addEventListener('DOMContentLoaded', function() {
                var mpinInput = document.getElementById('mpin');

                mpinInput.addEventListener('keypress', function(event) {
                    var key = event.keyCode || event.charCode;
                    if (key < 48 || key > 57) { // Check if the key pressed is not a number
                        event.preventDefault(); // Prevent the default action (typing the character)
                    }
                });
            });
        </script>
    </body>

    </html>
