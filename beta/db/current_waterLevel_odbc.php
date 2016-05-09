<?php

function get_current_time($conn) {	
	$stmt = odbc_exec($conn, "select max(InDate) from StageList");
	
	$time_str = '';

	if ( $stmt )
	{		
		while ($array = odbc_fetch_array($stmt))
		{
			$time_str = current($array);
		}			

		odbc_free_result($stmt);
	}
	
	return $time_str;
}

function get_current_waterLevel($conn) {
	$time_str = get_current_time($conn);
	$stmt = odbc_exec($conn, "select * from StageList Where InDate='$time_str'");
	
	$current_waterLevel = array();

	if ( $stmt )
	{		
		while ($array = odbc_fetch_array($stmt))
		{
			$current_waterLevel = $array;
		}
		odbc_free_result($stmt);
	}
	
	return $current_waterLevel;
}

$serverName = "192.168.96.206,1433"; //serverIp,port
$dbModel = "Reservoir";
$uid = "sa";
$pass = "sa123";

$connstr = "Driver={SQL Server};Server=$serverName;Database=$dbModel;";

$conn = odbc_connect($connstr, $uid, $pass);
if ( $conn )
{	
	print_r(json_encode(get_current_waterLevel($conn)));
	odbc_close($conn);
}
else 
{
	echo 'Connection error: ' . odbc_errormsg();
}
?>