<?php
    $dbhost = '127.0.0.1';
    $dbuser = 'root';
    $dbpass = '111111';
    $dbname = 'test';
    $conn = mysqli_connect($dbhost, $dbuser, $dbpass, $dbname) or die('Error with MySQL connection');
    mysqli_query($conn, "SET NAMES 'utf8'");
   // mysql_select_db($dbname);
	
    $sql = "SELECT * FROM `fall`;";
    $result = mysqli_query($conn, $sql) or die('MySQL query error');
    while($row = mysqli_fetch_array($result)){
        echo $row['location1'];
		echo '<br>';
    }
	
	mysqli_close($conn);
?>