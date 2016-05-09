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

function get_station($conn) {
	$station = array();
	$result = odbc_exec($conn, "select * from StageList");

	if ( $result )
	{		
		while ( $array = odbc_fetch_array($result) )
		{
			array_push($station, $array);
		}	

		odbc_free_result($result);
	}
	
	return $station;
}

function get_waterLevel_list($conn, $date_str) {
	$hours = array();
	$stmt = odbc_exec($conn, "select * from StageList Where convert(char(10),DateAdd(hour,-1,InDate),21)='$date_str' and Datepart(minute,InDate)=0 order by InDate");

	if ( $stmt )
	{		
		while ($array = odbc_fetch_array($stmt))
		{
			array_push($hours, $array);
		}
		odbc_free_result($stmt);
	}
	
	return $hours;
}

function get_waterLevel_max_list($conn, $date_str) {
	
	$max = array_fill_keys(
		array('Shemen', 'Clouds', 'High', 'Angle', 'Jade', 'Show', 'Kide'), 
		0.0
	);
	
	$keys = array_keys($max);

	$result = odbc_exec($conn, "select isnull(Max(Shemen),0), isnull(Max(Clouds),0) , isnull(Max(High),0) , isnull(Max(Angle),0) , isnull(Max(Jade),0) , isnull(Max(Show),0) , isnull(Max(Kide),0) from StageList Where convert(char(10),DateAdd(hour,-1,InDate),21)='$date_str' and Datepart(minute,InDate)=0");

	if ( $result )
	{		
		while ( odbc_fetch_row($result) )
		{
			for( $i = 1; $i <= odbc_num_fields($result); $i++)
			{
				$max[$keys[$i-1]] = odbc_result($result,$i);
			}
		}

		odbc_free_result($result);
	}
	
	return $max;
}

function get_waterLevel_min_list($conn, $date_str) {
	
	$min = array_fill_keys(
		array('Shemen', 'Clouds', 'High', 'Angle', 'Jade', 'Show', 'Kide'), 
		0.0
	);
	
	$keys = array_keys($min);

	$result = odbc_exec($conn, "select isnull(Min(Shemen),0), isnull(Min(Clouds),0) , isnull(Min(High),0) , isnull(Min(Angle),0) , isnull(Min(Jade),0) , isnull(Min(Show),0) , isnull(Min(Kide),0) from StageList Where convert(char(10),DateAdd(hour,-1,InDate),21)='$date_str' and Datepart(minute,InDate)=0");

	if ( $result )
	{		
		while ( odbc_fetch_row($result) )
		{
			for( $i = 1; $i <= odbc_num_fields($result); $i++)
			{
				$min[$keys[$i-1]] = odbc_result($result,$i);
			}
		}

		odbc_free_result($result);
	}
	
	return $min;
}

function get_waterLevel_avg_list($conn, $date_str) {
	
	$avg = array_fill_keys(
		array('Shemen', 'Clouds', 'High', 'Angle', 'Jade', 'Show', 'Kide'), 
		0.0
	);
	
	$keys = array_keys($avg);

	$result = odbc_exec($conn, "select isnull(AVG(Shemen),0), isnull(AVG(Clouds),0) , isnull(AVG(High),0) , isnull(AVG(Angle),0) , isnull(AVG(Jade),0) , isnull(AVG(Show),0) , isnull(AVG(Kide),0)  from StageList Where convert(char(10),DateAdd(hour,-1,InDate),21)='$date_str' and Datepart(minute,InDate)=0");

	if ( $result )
	{		
		while ( odbc_fetch_row($result) )
		{
			for( $i = 1; $i <= odbc_num_fields($result); $i++)
			{
				$avg[$keys[$i-1]] = odbc_result($result,$i);
			}
		}

		odbc_free_result($result);
	}
	
	return $avg;
}

$serverName = "192.168.96.206,1433"; //serverIp,port
$dbModel = "Reservoir";
$uid = "sa";
$pass = "sa123";

$connstr = "Driver={SQL Server};Server=$serverName;Database=$dbModel;";

$conn = odbc_connect($connstr, $uid, $pass);
if ( $conn )
{	
	$waterLevel_list['hours'] = get_waterLevel_list($conn, '2015-09-12');
	$waterLevel_list['max'] = get_waterLevel_max_list($conn, '2015-09-12');
	$waterLevel_list['min'] = get_waterLevel_min_list($conn, '2015-09-12');
	$waterLevel_list['avg'] = get_waterLevel_avg_list($conn, '2015-09-12');
	
	print_r(json_encode($waterLevel_list));
	odbc_close($conn);
}
else 
{
	echo 'Connection error: ' . odbc_errormsg();
}
?>