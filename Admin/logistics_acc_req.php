<?php
session_start();

// Replace with your database connection code
$conn = new mysqli("localhost", "root", "", "crowdfund");
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
// CODE FOR UPDATING ACCOUNT STATUS
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $status = $_POST["status"];
    $logistics_id = $_POST["logistics_id"];

    // Update logistics_account_req_status
    $update_sql = "UPDATE logistics_details SET logistics_account_req_status = '$status' WHERE logistics_id = '$logistics_id'";
    if ($conn->query($update_sql) === TRUE) {
        echo '<p class="success-message">Status updated successfully!</p>';
        header("Location: " . $_SERVER['PHP_SELF']);
    } else {
        echo '<p class="error-message">Error updating status: ' . $conn->error . '</p>';
    }
}

// Pagination configuration
$perPage = 10;
$page = isset($_GET['page']) ? intval($_GET['page']) : 1;
$offset = ($page - 1) * $perPage;

$queryCount = "SELECT COUNT(*) as total FROM logistics_details WHERE logistics_account_req_status = 'Pending'";
$resultCount = $conn->query($queryCount);
$totalRows = $resultCount->fetch_assoc()['total'];

$query = "SELECT * FROM logistics_details WHERE logistics_account_req_status = 'Pending' LIMIT $offset, $perPage";
$result = $conn->query($query);

$conn->close();
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Logistics Account Requests</title>
    <link rel="icon" href="../images/urbanlink-logo.png" type="image/icon type">
    <style>
             body {
            font-family: "lato", sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 1200px;
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

    <script>
        function confirmUpdate() {
            return confirm("Are you sure you want to update?");
        }
    </script>
</head>

<body>
<div class="container">
        <h2>Logistics Account Requests</h2>
        <table>
            <tr class="table-head">
                <th>ID</th>
                <th>Name</th>
                <th>Position</th>
                <th>Phone</th>
                <th>Email</th>
                <th>Organization</th>
                <th>Location</th>
                <th>Approve/Reject</th>
                <th>Image</th>
            </tr>
            <?php
            if ($result->num_rows > 0) {
                while ($row = $result->fetch_assoc()) {
                    echo '<tr>';
                    echo '<td>' . $row['logistics_id'] . '</td>';
                    echo '<td>' . $row['logistics_user_name'] . '</td>';
                    echo '<td>' . $row['logistics_user_position'] . '</td>';
                    echo '<td>' . $row['logistics_user_phone'] . '</td>';
                    echo '<td>' . $row['logistics_user_email'] . '</td>';
                    echo '<td>' . $row['logistics_org_name'] . '</td>';
                    echo '<td>' . $row['logistics_org_place'] . '</td>';
                    echo '<td>';
                    echo '<form method="POST" onsubmit="return confirmUpdate()">';
                    echo '<select class="status-select" name="status">
                        <option value="--Select Option--">--Select Option--</option>
                        <option value="Approve">Approve</option>
                        <option value="Reject">Reject</option>
                    </select>';
                    echo '<input type="hidden" name="logistics_id" value="' . $row['logistics_id'] . '">';
                    echo '<input type="submit" class="button" value="Update">';
                    echo '</form>';
                    echo '</td>';
                    echo '<td><a href="data:image/jpeg;base64,' . base64_encode($row['logistics_id_image']) . '" target="_blank"><img class="image-popup" src="data:image/jpeg;base64,' . base64_encode($row['logistics_id_image']) . '" alt="Image" width="80" height="60"></a></td>';
                    // echo '<td><img class="image-popup" src="data:image/jpeg;base64,' . base64_encode($row['ngo_id_image']) . '" alt="Image" width="80" height="60"></td>';
                    echo '</tr>';
                }
            } else {
                echo "<tr><td colspan='16'>No  Requests found.</td></tr>";
            }
            ?>
        </table>
        <?php
        // Pagination links
        $totalPages = ceil($totalRows / $perPage);
        echo "<div class='pagination'>";
        for ($i = 1; $i <= $totalPages; $i++) {
            $activeClass = $i === $page ? 'active' : '';
            echo "<a class='$activeClass' href='logistics_acc_req.php?page=$i'>$i</a>";
        }
        echo "</div>";
        ?>
    </div>

</body>

</html>