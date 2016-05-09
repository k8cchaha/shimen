<?php
// connect to db

$serverName = "192.168.96.207,1433"; //serverIp,port
$connectionInfo = array( "Database"=>"Reservoir", "UID"=>"sa", "PWD"=>"sa123");
$conn = sqlsrv_connect( $serverName, $connectionInfo);

if( $conn === false ) {
	echo "Connection could not be established.<br />";
	die( print_r( sqlsrv_errors(), true));
}

$sql = "select max(InDate) from RainfallList";

$stmt = sqlsrv_query( $conn, $sq);
if( $stmt === false ) {
     die( print_r( sqlsrv_errors(), true));
}

//phpinfo();
?>