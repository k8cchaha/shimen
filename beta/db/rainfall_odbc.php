<?php

function get_current_time($conn) {	
	$stmt = odbc_exec($conn, "select max(InDate) from RainfallList");
	
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
	$result = odbc_exec($conn, "select * from Station where cast(pcStaNo/100 as int)=2");

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

function get_current_rainfall($conn) {
	$time_str = get_current_time($conn);
	$stmt = odbc_exec($conn, "select * from RainfallList Where InDate='$time_str'");
	
	$current_rainfall = array();

	if ( $stmt )
	{		
		while ($array = odbc_fetch_array($stmt))
		{
			$current_rainfall = $array;
		}
		odbc_free_result($stmt);
	}
	
	return $current_rainfall;
}

function get_rainfall_list($conn, $date_str) {
	$hours = array();
	$stmt = odbc_exec($conn, "select * from RainfallList Where convert(char(10),DateAdd(minute,-50,InDate),21)='$date_str' order by InDate");

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

function get_sum_of_rainfall_list($conn, $date_str) {
	
	$sum = array_fill_keys(
		array('Shemen', 'Clouds', 'High', 'Bar', 'Scrunch', 'Jade', 'White', 'Burg', 'West', 'Mere'), 
		0.0
	);
	
	$keys = array_keys($sum);

	$result = odbc_exec($conn, "select sum(shemen),sum(clouds),sum(High),sum(bar),sum(scrunch),sum(jade),sum(white),sum(burg),sum(west),sum(mere) from RainfallList Where convert(char(10),DateAdd(minute,-50,InDate),21)='$date_str'");

	if ( $result )
	{		
		while ( odbc_fetch_row($result) )
		{
			for( $i = 1; $i <= odbc_num_fields($result); $i++)
			{
				$sum[$keys[$i-1]] = odbc_result($result,$i);
			}
		}

		odbc_free_result($result);
	}
	
	return $sum;
}

$serverName = "192.168.96.206,1433"; //serverIp,port
$dbModel = "Reservoir";
$uid = "sa";
$pass = "sa123";

$connstr = "Driver={SQL Server};Server=$serverName;Database=$dbModel;";

$conn = odbc_connect($connstr, $uid, $pass);
if ( $conn )
{
	
	$rainfall_list['station'] = get_station($conn);
	$rainfall_list['hours'] = get_rainfall_list($conn, '2015-09-12');
	$rainfall_list['sum'] = get_sum_of_rainfall_list($conn, '2015-09-12');
	
	$output = array();
	//$output['name'] = array();
	$output['density'] = array_fill_keys(
		array('Shemen', 'Clouds', 'High', 'Bar', 'Scrunch', 'Jade', 'White', 'Burg', 'West', 'Mere'), 
		0.0
	);
	$output['hours'] = array();	
		
	for ($i = 0; $i < count($rainfall_list['station']); $i++)
	{
		//array_push($output['name'], utf8_encode($rainfall_list['station'][$i]['StaName']));
		$output['density'][array_keys($output['density'])[$i]] = $rainfall_list['station'][$i]['Density'];
	}	
	
	for ($i = 0; $i < count($rainfall_list['hours']); $i++)
	{
		$total_rain = 0.0;
		$total_weight = 0.0;	
		
		foreach ($output['density'] as $dens_key => $dens_value) 
		{
			foreach($rainfall_list['hours'][$i] as $hour_keys => $hour_value)
			{
				if($hour_keys == $dens_key)
				{
					$total_rain += $hour_value;
					$total_weight += round($hour_value * ($dens_value/100), 1);
				}
			}
		}
		
		$rainfall_list['hours'][$i]['avg_rain'] = round($total_rain / count($output['density']), 1);
		$rainfall_list['hours'][$i]['avg_weight'] = $total_weight;
		
		array_push($output['hours'], $rainfall_list['hours'][$i]);
	}
	
	{
		$total_rain = 0.0;
		$total_weight = 0.0;	
		
		foreach ($output['density'] as $dens_key => $dens_value) 
		{
			foreach($rainfall_list['sum'] as $sum_keys => $sum_value)
			{
				if($sum_keys == $dens_key)
				{
					$total_rain += $sum_value;
					$total_weight += round($sum_value * ($dens_value/100), 1);
				}
			}
		}
		
		$rainfall_list['sum']['avg_rain'] = round($total_rain / count($output['density']), 1);
		$rainfall_list['sum']['avg_weight'] = $total_weight;
	}
	
	$output['sum'] = $rainfall_list['sum'];
		
	print_r(json_encode($output));
	
	odbc_close($conn);
}
else 
{
	echo 'Connection error: ' . odbc_errormsg();
}
?>