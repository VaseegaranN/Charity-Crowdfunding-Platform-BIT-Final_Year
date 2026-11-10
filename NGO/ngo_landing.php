<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <title>Side Navigation - NGO</title>
  <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css'>
  <link rel="stylesheet" href="style.css">
</head>

<body>
  <div id="nav-bar">
    <input id="nav-toggle" type="checkbox" />
    <div id="nav-header">
      <a id="nav-title" href="#">UnityFunds</a>
      <label for="nav-toggle"><span id="nav-toggle-burger"></span></label>
      <hr />
    </div>
    <div id="nav-content">
      <div class="nav-button"><i class="fas fa-user"></i><span>Profile</span></div>
      <div class="nav-button"><i class="fas fa-plus"></i><span>Create Post</span></div>
      <div class="nav-button"><i class="fas fa-users"></i><span>Create Campaigns</span></div>
      <div class="nav-button"><i class="fas fa-receipt"></i><span>Your Activity</span></div>
      <div class="nav-button"><i class="fas fa-question"></i><span>FAQ - HELP</span></div>
      <div id="nav-content-highlight"></div>
    </div>
    <input id="nav-footer-toggle" type="checkbox"/>
    <div id="nav-footer">
      <div id="nav-footer-heading">
        <div id="nav-footer-avatar"><img src="https://gravatar.com/avatar/4474ca42d303761c2901fa819c4f2547"/></div>
        <div id="nav-footer-titlebox"><a id="nav-footer-title" href="#">NGO</a></div>
        <label for="nav-footer-toggle"><i class="fas fa-caret-up"></i></label>
      </div>
      <div id="nav-footer-content">
        <p>
          <!-- <a href="#" class="logout">Logout</a>
          <a href="#"><i class="fas fa-sign-out-alt"></i></a> -->
          <div class="nav-button"><i class="fas fa-sign-out-alt"></i><a href="#" class="logout"><span>Logout</span></div>
        </p>
      </div>
    </div>
  </div>
</body>

</html>