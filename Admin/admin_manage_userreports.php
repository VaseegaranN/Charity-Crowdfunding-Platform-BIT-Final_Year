<?php
session_start();

// Check if the user is logged in as an admin
if (!isset($_SESSION['sv_admin_username']) || !isset($_SESSION['sv_admin_id'])) {
    header('Location: admin_login.php');
    exit();
}

// Include database configuration
require_once 'ap_config.php';

// Handle update and delete operations
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    if (isset($_POST['update'])) {
        $reportedUserId = $_POST['reported_user_id'];
        $reportedUserLocation = $_POST['reported_user_loc'];
        $newStatus = $_POST['status'];

        if ($reportedUserLocation === 'Gobichettipalayam') {
            // Update the gobi_users table
            $query = "UPDATE gobi_users SET gobi_user_acc_status = :new_status WHERE gobi_user_id = :reported_user_id";
        } elseif ($reportedUserLocation === 'Sathymangalam') {
            // Update the sathy_users table
            $query = "UPDATE sathy_users SET sathy_user_acc_status = :new_status WHERE sathy_user_id = :reported_user_id";
        } else {
            // Invalid location, handle the error appropriately
            // You may want to display an error message or take other actions here
            // For simplicity, we will exit the script.
            exit("Invalid user location");
        }

        $stmt = $conn->prepare($query);
        $stmt->bindParam(':new_status', $newStatus);
        $stmt->bindParam(':reported_user_id', $reportedUserId);
        $stmt->execute();
        $successMessage = "User status updated successfully!";
        header("Location: {$_SERVER['PHP_SELF']}");
        exit();
    } elseif (isset($_POST['delete'])) {
        $reportId = $_POST['user_report_id'];

        $query = "DELETE FROM reported_users WHERE user_report_id = :user_report_id";
        $stmt = $conn->prepare($query);
        $stmt->bindParam(':user_report_id', $reportId);
        $stmt->execute();
        header("Location: {$_SERVER['PHP_SELF']}");
        exit();
    }
}

// Fetch reported user list from the database
$query = "SELECT * FROM reported_users";
$stmt = $conn->prepare($query);
$stmt->execute();
$reportedUsers = $stmt->fetchAll(PDO::FETCH_ASSOC);
?>

<!DOCTYPE html>
<html>

<head>
    <title>Admin Manage User Reports</title>
    <link rel="icon" href="../images/urbanlink-logo.png" type="image/icon type">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
             body {
            font-family: "lato", sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 90%;
            margin: 0 auto;
            padding: 20px;
            background-color: rgba(255, 255, 255, 0.5);
            backdrop-filter: blur(10px);
            border-radius: 10px;
            box-shadow: 0 0 50px rgba(07, 04, 20, 1);
            position: relative;
            overflow-y: auto; /* Keep vertical scrollbar if needed */
            border: 2px solid black;
            scrollbar-width: thin;
            scrollbar-color: #a0a0a0 #f0f0f0;
            top: 50px;
        }

        h2 {
            font-size: 26px;
            margin: 20px 0;
            text-align: center;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
            margin-top: 20px;
        }

        th,
        td {
            padding: 12px 15px;
            text-align: center;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #780d9b;
            font-size: 14px;
            color: #ffffff;
            text-transform: uppercase;
            letter-spacing: 0.03em;
        }

        tr:hover {
            background-color: #d5cece;
        }

        .status-select {
            padding: 8px 12px;
            border: 2px solid #007bff;
            border-radius: 5px;
            background-color: white;
            font-size: 14px;
            font-weight: 600;
            color: #007bff;
            margin: 5px;
            cursor: pointer;
        }

        .button {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 8px 15px;
            cursor: pointer;
            border-radius: 5px;
            font-weight: 600;
        }

        .image-popup {
            cursor: pointer;
            transition: transform 0.2s ease-in-out;
            border-radius: 5px;
            border: 2px solid #ddd;
        }

        .image-popup:hover {
            transform: scale(1.05);
            border-color: #007bff;
        }

        .pagination {
            margin-top: 20px;
            display: flex;
            justify-content: center;
            position: relative;
            top: 50%;
        }

        .pagination a {
            display: inline-block;
            padding: 5px 10px;
            margin: 0 5px;
            text-decoration: none;
            background-color: #f2f2f2;
            border: 1px solid #ddd;
            border-radius: 5px;
            color: #333;
        }

        .pagination a.active {
            background-color: #007bff;
            border-color: #007bff;
            color: #fff;
        }

        .pagination a:hover {
            background-color: red;
            border-color: red;
        }

        /* Media Queries */
        @media all and (max-width: 767px) {
            table {
                display: block;
                overflow-x: auto;
                white-space: nowrap;
            }

            th,
            td {
                min-width: 120px;
            }
        }
    </style>
    <script src="https://kit.fontawesome.com/4c43584236.js" crossorigin="anonymous"></script>
</head>

<body>
    <div class="container">
        <h2>Reported Users List</h2>
        <!-- Add success message -->
        <?php if (isset($successMessage)) : ?>
            <div id="successMessage" style="background-color: #5cb85c; color: #fff; padding: 10px; text-align: center; margin-bottom: 10px;">
                <?php echo $successMessage; ?>
            </div>
        <?php endif; ?>
        <table>
            <thead>
                <tr>
                    <th>Reported User ID</th>
                    <th>Reported User Name</th>
                    <th>Reported User Phone</th>
                    <th>Reported User Location</th>
                    <th>Report Reason</th>
                    <th>Report Date</th>
                    <th>Report Type</th>
                    <th>Reporting User ID</th>
                    <th>Reporting User Name</th>
                    <th>Reporting User Phone</th>
                    <th>Update Status</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($reportedUsers as $user) : ?>
                    <tr>
                        <td><?php echo $user['reported_user_id']; ?></td>
                        <td><?php echo $user['reported_user_name']; ?></td>
                        <td><?php echo $user['reported_user_phone']; ?></td>
                        <td><?php echo $user['reported_user_loc']; ?></td>
                        <td><?php echo $user['report_reason']; ?></td>
                        <td><?php echo $user['report_date']; ?></td>
                        <td><?php echo $user['report_type']; ?></td>
                        <td><?php echo $user['reporting_user_id']; ?></td>
                        <td><?php echo $user['reporting_user_name']; ?></td>
                        <td><?php echo $user['reporting_user_phone']; ?></td>
                        <td>
                            <form method="post">
                                <input type="hidden" name="reported_user_id" value="<?php echo $user['reported_user_id']; ?>">
                                <input type="hidden" name="reported_user_loc" value="<?php echo $user['reported_user_loc']; ?>">
                                <?php
                                // Determine the appropriate table name based on the reported location
                                $tableName = ($user['reported_user_loc'] === 'Gobichettipalayam') ? 'gobi_users' : 'sathy_users';
                                $idcolumnName = ($user['reported_user_loc'] === 'Gobichettipalayam') ? 'gobi_user' : 'sathy_user';
                                $acc_status = ($user['reported_user_loc'] === 'Gobichettipalayam') ? 'gobi_user_acc_status' : 'sathy_user_acc_status';

                                // Fetch the acc_status value from the corresponding table
                                $query = "SELECT $acc_status FROM $tableName WHERE {$idcolumnName}_id = :reported_user_id";
                                $stmt = $conn->prepare($query);
                                $stmt->bindParam(':reported_user_id', $user['reported_user_id']);
                                $stmt->execute();
                                $userStatus = $stmt->fetchColumn();
                                ?>
                                <select name="status" class="status-select">
                                    <option value="--SELECT--">--SELECT--</option>
                                    <option value="BLOCKED" class="blocked" <?php if (strtolower($userStatus) === 'blocked') echo 'selected'; ?>>BLOCKED</option>
                                    <option value="UNBLOCKED" class="unblocked" <?php if (strtolower($userStatus) === 'unblocked') echo 'selected'; ?>>UNBLOCKED</option>
                                </select>
                                <input type="submit" name="update" value="Save" class="button" >
                            </form>
                        </td>
                        <td>
                            <!-- <form method="post">
                                <input type="hidden" name="user_report_id" value="<?php echo $user['user_report_id']; ?>">
                                <i class="fa-solid fa-trash">
                                <input type="submit" name="delete" value="Delete">
                                </i>
                            </form> -->
                            <form method="post" onsubmit="return confirmDelete()">
                                <input type="hidden" name="user_report_id" value="<?php echo $user['user_report_id']; ?>">
                                <button type="submit" name="delete" style=" border: none; cursor:pointer; height:25px; width:25px;">
                                    <i class="fa-solid fa-trash"></i>
                                </button>
                            </form>
                        </td>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    </div>

    <!-- JavaScript to hide the success message after 3 seconds -->
    <script>
        // Function to hide the success message
        function hideSuccessMessage() {
            var successMessage = document.getElementById("successMessage");
            if (successMessage) {
                successMessage.style.display = "none";
            }
        }

        // Hide the success message after 3 seconds
        var successMessage = document.getElementById("successMessage");
        if (successMessage) {
            setTimeout(hideSuccessMessage, 3000); // 3000 milliseconds = 3 seconds
        }
    </script>
    <script>
        function confirmDelete() {
            return confirm("Are you sure you want to delete this user?");
        }
    </script>

</body>

</html>