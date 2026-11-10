<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="style.css">
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Lato&family=Proza+Libre&display=swap" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet">
    <title> Home | UnityFunds</title>
</head>
<body>
  <div class="header">
    <div class="brand">
        <h1>UnityFunds</h1>
        <h3>Illuminate Lives</h3>
</div>
    <nav class="nav-menu">
      <a href="index.php">Home</a>
      <ul class="nav-list">
        <li>
          <a href="#">Login</a>
          <ul class="nav-list-dropdown">
            <li><a href="./Donor/donor-login.php">Donor Login</a></li>
            <li><a href="./Logistics/logistics_login.php">Logistics Login</a></li>
            <li><a href="./NGO/ngo_login.php">NGO Login</a></li>
            <li><a href="./Admin/admin_login.php">Admin Login</a></li>
          </ul>
        </li>
      </ul>

      <ul class="nav-list">
        <li>
          <a href="#">Sign Up</a>
          <ul class="nav-list-dropdown">
            <li><a href="./Donor/donor-signup.php">Donor</a></li>
            <li><a href="./Logistics/logistics_signup.php">Logistics</a></li>
            <li><a href="./NGO/ngo_signup.php">NGO</a></li>
          </ul>
        </li>
      </ul>
    </nav>
  </div>

  <div class="banner">
  <blockquote>Dream Big, Fund Bigger: Crowdfunding the Path to Progress!</blockquote>
  </div>

  <div class="brief-desc">
    <p>At UnityFunds, we believe in the power of collective impact. <br>Our platform is more than just crowdfunding;<br> It's a community-driven ecosystem connecting passionate individuals, NGOs and Logistics enthusiasts to drive positive change.</p>
    <img src="images\donation1.jpg" alt="" />
    <img src="images\donation2.jpg" class="anotherimg" alt="" />
  </div>

<div class="features">
  <div>Unlocking Possibilities</div> 
  <div> 
  <span>: Our Signature Features</span>
  </div>
</div>

<div class="features-list">

<div>
<h1>Diverse Registration</h1> 
<p>Join us as a Donor, NGO or Logistics supporter, each playing a unique role in making a difference.</p>
</div>

<div>
  <h1>Flexible Funding Options</h1>
  <p>Donors have the flexibility to choose fixed or flexible funding, allowing for monthly contributions or emergency-only donations.</p>
</div>

<div>
  <h1>Physical Goods, Real Impact</h1>
  <p>Donate physical items and track their journey on our platform, bringing tangible change to those in need.</p>
</div>

<div>
  <h1>Real-Time Impact Updates</h1>
  <p>Donors receive geo-tagged photos, providing a direct view of the impact their contributions have on the ground.</p>
</div>
</div>

<div class="black-lives-matter">
    <h1> Join us on this journey to create meaningful impact</h1>
</div>
</body>

<footer>
        <p class="footer">© <span id="currentYear"></span> UnityFunds. All rights reserved.</p>
</footer>
<script>
        // Automatic year update
        document.getElementById("currentYear").innerHTML = new Date().getFullYear();
</script>

</html>