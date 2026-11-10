<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Display Posts</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }

        .container {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .post {
            margin-bottom: 20px;
            padding: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #fff;
        }

        .post-content {
            margin-bottom: 10px;
        }

        .buttons {
            display: flex;
            justify-content: space-between;
        }

        .button {
            padding: 8px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .donate {
            background-color: #4caf50;
            color: #fff;
        }

        .donate:hover {
            background-color: #45a049;
        }

        .report {
            background-color: #f44336;
            color: #fff;
        }

        .report:hover {
            background-color: #d32f2f;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>Posts</h2>
        <?php
        // Database connection details
        $servername = "localhost"; // Change this if your database is hosted on a different server
        $username = "root"; // Change this to your database username
        $password = ""; // Change this to your database password
        $dbname = "crowdfund"; // Change this to your database name

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);

        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        // Fetch posts from the database
        $sql = "SELECT * FROM admin_posts";
        $result = $conn->query($sql);

        // Display each post
        if ($result->num_rows > 0) {
            while ($row = $result->fetch_assoc()) {
                echo "<div class='post'>";
                echo "<div class='post-content'>";
                echo "<p>" . $row['username'] . "</p>";
                echo "<p>" . $row['content'] . "</p>";
                if (!empty($row['media'])) {
                    echo "<img src='uploads/" . $row['media'] . "' alt='Media'>";
                }
                echo "</div>";
                echo "<div class='buttons'>";
                echo "<button class='button donate'>Donate</button>";
                echo "<button class='button report'>Report</button>";
                echo "</div>";
                echo "</div>";
            }
        } else {
            echo "No posts found.";
        }

        // Close connection
        $conn->close();
        ?>
    </div>
</body>
</html>
