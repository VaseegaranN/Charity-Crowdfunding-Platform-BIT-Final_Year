<?php
// Database connection parameters
if ($_SERVER["REQUEST_METHOD"] == "POST") {
$servername = "localhost"; // Change this if your MySQL server is on a different host
$username = "root";
$password = "";
$database = "crowdfund";

// Create connection
$conn = new mysqli($servername, $username, $password, $database);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Get form data
$fullName = $_POST['fullName'];
$email = $_POST['email'];
$phone = $_POST['phone'];
$streetAddress = $_POST['streetAddress'];
$city = $_POST['city'];
$state = $_POST['state'];
$zipCode = $_POST['zipCode'];
$country = $_POST['country'];
$password = $_POST['password'];
$dob = $_POST['dob'];
$gender = $_POST['gender'];
$fundingType = $_POST['fundingType'];
$donationAmount = isset($_POST['amount']) ? $_POST['amount'] : "-";
$paymentType = $_POST['paymentType'];
$upiId = isset($_POST['upiId']) ? $_POST['upiId'] : "-";
$cardNumber = isset($_POST['cardNumber']) ? $_POST['cardNumber'] : "-";
$expiry = isset($_POST['expiry']) ? $_POST['expiry'] : "-";
$cvCode = isset($_POST['cvCode']) ? $_POST['cvCode'] : "-";
$childhoodPet = isset($_POST['childhood_pet']) ? $_POST['childhood_pet'] : "-";
$vacationDestination = isset($_POST['vacation_destination']) ? $_POST['vacation_destination'] : "-";
$favouriteHobby = isset($_POST['favourite_hobby']) ? $_POST['favourite_hobby'] : "-";

// Prepare SQL statement
$sql = "INSERT INTO donor_details (donor_user_name, donor_user_email, donor_user_phone, 
donor_user_street, donor_user_city, donor_user_state, donor_user_zip, donor_user_country, 
donor_user_pwd, donor_user_gender, fundingType, donationAmount, paymentType, 
upiId, cardNumber, expiry, cvCode, childhood_pet, vacation_destination, favourite_hobby) 
VALUES ('$fullName', '$email', '$phone', '$streetAddress', '$city', '$state', '$zipCode', '$country',
'$password', '$gender', '$fundingType', '$donationAmount', '$paymentType', '$upiId', '$cardNumber', 
'$expiry', '$cvCode', '$childhoodPet', '$vacationDestination', '$favouriteHobby')";


// Execute SQL statement
if ($conn->query($sql) === TRUE) {
    echo json_encode(array("status" => "success"));
} else {
    echo json_encode(array("status" => "error", "message" => "Error: " . $sql . "<br>" . $conn->error));
}


// Close connection
$conn->close();
}
?>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Donor Signup</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Poppins:wght@200;300;400;500;600;700&display=swap">
    <style>
        /* Custom CSS for styling */
        body {
            font-family: 'Poppins', sans-serif;
            background: linear-gradient(135deg, #71b7e6, #9b59b6);
            padding: 20px;
        }

        .container {
            max-width: 700px;
            width: 100%;
            background-color: #fff;
            padding: 25px;
            border-radius: 5px;
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.15);
        }

        .main-heading {
            text-align: center;
            font-size: 30px;
            font-weight: 600;
            margin-bottom: 30px;
            color: #333;
        }

        .panel-title {
            font-size: 25px;
            font-weight: 500;
            position: relative;
            margin-bottom: 20px;
            text-align: center;
            color: #333;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            font-weight: 500;
        }

        .form-group input[type="text"],
        .form-group input[type="email"],
        .form-group input[type="tel"],
        .form-group input[type="number"],
        .form-group input[type="password"],
        .form-group select {
            height: 45px;
            width: 100%;
            outline: none;
            font-size: 16px;
            border-radius: 5px;
            padding-left: 15px;
            border: 1px solid #ccc;
            border-bottom-width: 2px;
            transition: all 0.3s ease;
        }

        .form-group input[type="text"]:focus,
        .form-group input[type="email"]:focus,
        .form-group input[type="tel"]:focus,
        .form-group input[type="number"]:focus,
        .form-group input[type="password"]:focus,
        .form-group select:focus {
            border-color: #9b59b6;
        }

        .btn-success {
            background-color: #28a745;
            border-color: #28a745;
            height: 45px;
            margin: 35px 0;
            width: 100%;
            border-radius: 5px;
            border: none;
            color: #fff;
            font-size: 18px;
            font-weight: 500;
            letter-spacing: 1px;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .btn-success:hover {
            background: linear-gradient(-135deg, #71b7e6, #9b59b6);
        }

        .slider {
            position: relative;
            width: 100%;
            height: 300px;
            margin: 10px auto;
            box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 1px 5px 0 rgba(0, 0, 0, 0.12),
                0 3px 1px -2px rgba(0, 0, 0, 0.2);
            overflow: hidden;
        }

        .security-questions {
            margin-bottom: 10px; /* Adjust the margin as needed */
        }


        .security-questions p {
            font-size: 18px; /* Adjust the font size as needed */
        }

        .slider-controls {
            position: absolute;
            bottom: 0px;
            left: 50%;
            width: 200px;
            text-align: center;
            transform: translateX(-50%);
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

        #btn-1:checked~.slider-controls label[for="btn-1"],
        #btn-2:checked~.slider-controls label[for="btn-2"],
        #btn-3:checked~.slider-controls label[for="btn-3"] {
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
            transform: translateX(-100%);
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
        #btn-1:checked~.slides .slide:nth-child(1),
        #btn-2:checked~.slides .slide:nth-child(2),
        #btn-3:checked~.slides .slide:nth-child(3) {
            transform: translateX(0);
            opacity: 1;
        }

        #btn-1:not(:checked)~.slides .slide:nth-child(1),
        #btn-2:not(:checked)~.slides .slide:nth-child(2),
        #btn-3:not(:checked)~.slides .slide:nth-child(3) {
            animation-name: swap-out;
            animation-duration: 300ms;
            animation-timing-function: linear;
        }

        @keyframes swap-out {
            0% {
                transform: translateX(0);
                opacity: 1;
            }

            50% {
                transform: translateX(50%);
                opacity: 0;
            }

            100% {
                transform: translateX(100%);
            }
        }

        /* Additional styles for input fields */
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

        .input-field input:focus {
            border: 2px solid #780d9b;
        }

        .input-field input:focus~label,
        .input-field input:valid~label {
            top: 0;
            left: 15px;
            font-size: 16px;
            padding: 0 2px;
            background: #ffffff;
        }

        /* Flashing note style */
        .note-flash {
            border: 1px solid white;
            border-radius: 5px;
            padding: 5px 10px;
            color: #4CAF50;
            animation: flash-note 1s infinite;
        }

        @keyframes flash-note {
            0% {
                opacity: 1;
            }

            50% {
                opacity: 0.5;
            }

            100% {
                opacity: 1;
            }
        }
    </style>
</head>

<body>
    <div class="container">
        <h2 class="main-heading">Donor Signup</h2>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">Personal Information</h3>
            </div>
            <div class="panel-body">
                <form id="donor-signup-form" action="donor-signup.php" method="post">

                    <!-- Personal Information -->
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="fullName">Full Name:</label>
                                <input type="text" id="fullName" name="fullName" class="form-control" placeholder="Enter your name" required>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="email">Email Address:</label>
                                <input type="email" id="email" name="email" class="form-control" placeholder="Enter your email" required>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="phone">Phone Number:</label>
                                <input type="tel" id="phone" name="phone" class="form-control" placeholder="Enter your phone number" required>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="streetAddress">Street Address:</label>
                                <input type="text" id="streetAddress" name="streetAddress" class="form-control" placeholder="Enter your address" required>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="city">City:</label>
                                <input type="text" id="city" name="city" class="form-control" placeholder="Enter your city" required>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="state">State/Province:</label>
                                <input type="text" id="state" name="state" class="form-control" placeholder="Enter your state" required>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="zipCode">Zip / Postal Code:</label>
                                <input type="text" id="zipCode" name="zipCode" class="form-control" placeholder="Enter your zip code" required>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="country">Country</label>
                                <input type="text" id="country" name="country" class="form-control" placeholder="Enter your country" required>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="password">Password:</label>
                                <input type="password" id="password" name="password" class="form-control" placeholder="Enter your password" required>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="dob">Date of Birth:</label>
                                <input type="date" id="dob" name="dob" class="form-control" required>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="gender">Gender</label>
                                <select name="gender" id="gender" class="form-control" required>
                                    <option value="">Select Gender</option>
                                    <option value="male">Male</option>
                                    <option value="female">Female</option>
                                    <option value="other">Other</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    </div>

                    <!-- Donation Details -->
                    <div class="form-group">
                        <label for="fundingType">Funding Type</label>
                        <select name="fundingType" id="fundingType" class="form-control" required>
                            <option value="">Select Funding Type</option>
                            <option value="fixed">Fixed</option>
                            <option value="flexible">Flexible</option>
                        </select>
                    </div>
                    <div class="form-group" id="amount-group" style="display: none;">
                        <label for="amount">Donation Amount</label>
                        <input type="number" id="amount" name="amount" class="form-control" placeholder="Enter donation amount">
                    </div>

                    <!-- Payment Details -->
                    <div class="form-group">
                        <label for="paymentType">Payment Type</label>
                        <select name="paymentType" id="paymentType" class="form-control" required>
                            <option value="">Select Payment Type</option>
                            <option value="upi">UPI</option>
                            <option value="credit-debit-card">Credit/Debit Card</option>
                        </select>
                    </div>
                    <div id="upi-field" style="display: none;">
                        <div class="form-group">
                            <label for="upiId">UPI ID</label>
                            <input type="text" id="upiId" name="upiId" class="form-control" placeholder="Enter your UPI ID">
                        </div>
                    </div>
                    <div id="card-fields" style="display: none;">
                        <div class="form-group">
                            <label for="cardNumber">Card Number</label>
                            <input type="text" id="cardNumber" name="cardNumber" class="form-control" placeholder="Enter card number">
                        </div>
                        <div class="form-group">
                            <label for="expiry">Expiry Date (MM/YYYY)</label>
                            <input type="text" id="expiry" name="expiry" class="form-control" placeholder="MM/YYYY">
                        </div>
                        <div class="form-group">
                            <label for="cvCode">CVV</label>
                            <input type="text" id="cvCode" name="cvCode" class="form-control" placeholder="Enter CVV">
                        </div>
                    </div>

                    <!-- Security Questions -->
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

                    <button type="submit" class="btn btn-success" id="submit-btn">Submit</button>
                </form>
            </div>
        </div>
    </div>

    <script>
        // JavaScript to handle form submission
        document.getElementById('donor-signup-form').addEventListener('submit', function (event) {
            event.preventDefault(); // Prevent the default form submission

            var formData = new FormData(this); // Get form data

            // Submit form data to a server using fetch
            fetch('donor-signup.php', {
                method: 'POST',
                body: formData
            })
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    console.log(data);
                    alert('Form submitted successfully!');
                })
                .catch(function (error) {
                    console.error(error);
                    alert('Form submitted successfully!');
                });
        });

        // Show/hide donation amount field based on funding type selection
        document.getElementById('fundingType').addEventListener('change', function () {
            var amountGroup = document.getElementById('amount-group');
            if (this.value === 'fixed') {
                amountGroup.style.display = 'block';
            } else {
                amountGroup.style.display = 'none';
            }
        });

        // Show/hide UPI or card fields based on payment type selection
        document.getElementById('paymentType').addEventListener('change', function () {
            var upiField = document.getElementById('upi-field');
            var cardFields = document.getElementById('card-fields');
            if (this.value === 'upi') {
                upiField.style.display = 'block';
                cardFields.style.display = 'none';
            } else if (this.value === 'credit-debit-card') {
                upiField.style.display = 'none';
                cardFields.style.display = 'block';
            } else {
                upiField.style.display = 'none';
                cardFields.style.display = 'none';
            }
        });
    </script>
</body>

</html>