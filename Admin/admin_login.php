<?php
session_start(); // Start the session

$host = "localhost";
$username = "root";
$password = "";
$database = "crowdfund";

$conn = mysqli_connect($host, $username, $password, $database);

if (isset($_POST['username']) && isset($_POST['user_email']) && isset($_POST['password'])) {
    $username = $_POST['username'];
    $user_email = $_POST['user_email'];
    $password = $_POST['password'];

    $query = "SELECT * FROM admin_details WHERE admin_name = '$username' AND admin_email = '$user_email' AND admin_pass = '$password'";

    $result = mysqli_query($conn, $query);

    if (mysqli_num_rows($result) == 1) {
        // Successful verification
        $row = mysqli_fetch_assoc($result);

        $_SESSION['admin_id'] = $row['admin_id'];
        $_SESSION['admin_name'] = $row['admin_name'];
        $_SESSION['admin_email'] = $row['admin_email'];
        $_SESSION['admin_phone'] = $row['admin_phone_number'];

        header("Location: admin_landing.php");
        exit();
    } else {
        // Error message
        $error = "Invalid credentials. Please try again.";
    }
}
?>

    <!DOCTYPE html>
    <html>

    <head>
        <title>Admin Login</title>
        <link rel="icon" href="../images/urbanlink-logo.png" type="image/icon type">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <style>
*
{
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: 'Quicksand', sans-serif;
}
body 
{
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background: #000;
}
section 
{
  position: absolute;
  width: 100vw;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 2px;
  flex-wrap: wrap;
  overflow: hidden;
}
section::before 
{
  content: '';
  position: absolute;
  width: 100%;
  height: 100%;
  background: linear-gradient(#000,#780d9b,#000);
  animation: animate 5s linear infinite;
}
@keyframes animate 
{
  0%
  {
    transform: translateY(-100%);
  }
  100%
  {
    transform: translateY(100%);
  }
}
section span 
{
  position: relative;
  display: block;
  width: calc(6.25vw - 2px);
  height: calc(6.25vw - 2px);
  background: #181818;
  z-index: 2;
  transition: 1.5s;
}
section span:hover 
{
  background: #780d9b;
  transition: 0s;
}

section .signin
{
  position: absolute;
  width: 400px;
  background: #222;  
  z-index: 1000;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 40px;
  border-radius: 4px;
  box-shadow: 0 15px 35px rgba(0,0,0,9);
}
section .signin .content 
{
  position: relative;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  gap: 40px;
}
section .signin .content h2 
{
  font-size: 2em;
  color: #0f0;
  text-transform: uppercase;
}
section .signin .content .form 
{
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 25px;
}
section .signin .content .form .inputBox
{
  position: relative;
  width: 100%;
}
section .signin .content .form .inputBox input 
{
  position: relative;
  width: 100%;
  background: #333;
  border: none;
  outline: none;
  padding: 25px 10px 7.5px;
  border-radius: 4px;
  color: #fff;
  font-weight: 500;
  font-size: 1em;
}
section .signin .content .form .inputBox i 
{
  position: absolute;
  left: 0;
  padding: 15px 10px;
  font-style: normal;
  color: #aaa;
  transition: 0.5s;
  pointer-events: none;
}
.signin .content .form .inputBox input:focus ~ i,
.signin .content .form .inputBox input:valid ~ i
{
  transform: translateY(-7.5px);
  font-size: 0.8em;
  color: #fff;
}
.signin .content .form .links 
{
  position: relative;
  width: 100%;
  display: flex;
  justify-content: space-between;
}
.signin .content .form .links a 
{
  color: #fff;
  text-decoration: none;
}
.signin .content .form .links a:nth-child(2)
{
  color: #780d9b;
  font-weight: 600;
}
.signin .content .form .inputBox input[type="submit"]
{
  padding: 10px;
  background: #780d9b;
  color: #000;
  font-weight: 600;
  font-size: 1.35em;
  letter-spacing: 0.05em;
  cursor: pointer;
}
input[type="submit"]:active
{
  opacity: 0.6;
}
@media (max-width: 900px)
{
  section span 
  {
    width: calc(10vw - 2px);
    height: calc(10vw - 2px);
  }
}
@media (max-width: 600px)
{
  section span 
  {
    width: calc(20vw - 2px);
    height: calc(20vw - 2px);
  }
}
</style>
</head>

    <body>
        <section> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span>
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span>
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> <span></span> 
        <span></span> <span></span> 
        <div class="signin">
            <div class="content">
                <h2 style="color:#780d9b;">Admin Login</h2>
                <form method="POST" action="<?php echo $_SERVER['PHP_SELF']; ?>">
                    <div class="form">
                        <div class="inputBox">
                            <label for="username" style="color:#fff;">Username:</label>
                            <input type="text" id="username" name="username" placeholder="Enter your username" required>
                        </div>
                        <div class="inputBox">
                            <label for="email" style="color:#fff;">Email:</label>
                            <input type="email" id="user_email" name="user_email" placeholder="Enter your user-email" required>
                        </div>
                        <div class="inputBox">
                            <label for="password" style="color:#fff;">Password:</label>
                            <input type="password" id="password" name="password" placeholder="Enter your password" required>
                        </div>
                        <div class="inputBox">
                            <input type="submit" value="Login">
                        </div>
                    </div>    
                </form>
            </div>
        </div>
        </section>
        </body>

    </html>
