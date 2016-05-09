<?php
header('Content-Type: application/json; charset=utf-8');
ini_set("max_execution_time", "80");
if (!isset($_GET['id']))
{
	echo 'wrong parameter';
	return;
}
$id = $_GET['id'];

//get json file
$json_dev_file = file_get_contents("./../data/dev.json");
$json_dev = json_decode($json_dev_file, true);
$find = FALSE;
$eyeObj;
$location = array("惠賓樓", "沉澱池", "大灣", "霞雲", "太平山");
$result = array();


$lastTime = date('Y/m/d H:i:s');
	
if ($id == -1) {
	
	for ($i = 0; $i < count($location); $i++)
	{
		$eyeObj = $json_dev['eye'][$i];
		$temperature_name = $eyeObj['name'] . '環境溫度';
		$humid_name = $eyeObj['name'] . '環境濕度';
		$di_name = $eyeObj['name'] . '市電電壓';
		$do_name = $eyeObj['name'] . 'UPS輸出電壓';
		//outside exec
		$outside_cmd = 'eye.exe ' . $eyeObj['ip'];
		//echo $outside_cmd;
		$output = '';
		exec($outside_cmd, $output, $return_var);
		//echo $output[1];

		$temperature = "0.0";
		$humid = "0.0";
		$di = "0.0";
		$do = "0.0";
		
		if ($return_var != 3) {
			$NewString1 = preg_split("/[\s,]+/", $output[1]);

			$temperature = $NewString1[0];
			$humid = $NewString1[1];
			$di = $NewString1[2];
			$do = $NewString1[3];	
		}
	
		$name = urlencode($location[$i]);
		$value = urlencode($temperature_name . ':' . $temperature . ' ' . $humid_name . ':' . $humid . ' ' . $di_name . ':' . $di . ' ' . $do_name . ':' . $do);
		
		$temp = array(
		  'name' => $name,
		  'lastTime' => $lastTime,
		  'value' => $value,
		  'temperature' => $temperature,
		  'humid' => $humid,
		  'di' => $di,
		  'do_out' => $do
		);
		array_push($result, $temp);	
	}

	echo urldecode(json_encode($result));	
}
else {
	//find real eye index
	for ($i = 0; $i < count($json_dev['eye']) ; $i++ ) {
		if ($json_dev['eye'][$i]['id'] == $id)
		{
			$eyeObj = $json_dev['eye'][$i];
			$find = TRUE;
			break;
		}
	}
	//cannot find correct device, this is a bug
	if ($find == FALSE)
	{
		echo 'wrong eye falcon index';
		return;
	}

	//outside exec
	$outside_cmd = 'eye.exe ' . $eyeObj['ip'];
	//echo $outside_cmd;
	exec($outside_cmd, $output, $return_var);
	//echo $output[1];

	$temperature = "0.0";
	$humid = "0.0";
	$di = "0.0";
	$do = "0.0";
	
	if ($return_var != 3) {
		$NewString1 = preg_split("/[\s,]+/", $output[1]);

		$temperature = $NewString1[0];
		$humid = $NewString1[1];
		$di = $NewString1[2];
		$do = $NewString1[3];	
	}

	$temperature_name = $eyeObj['name'] . '環境溫度';
	$humid_name = $eyeObj['name'] . '環境濕度';
	$di_name = $eyeObj['name'] . '市電電壓';
	$do_name = $eyeObj['name'] . 'UPS輸出電壓';

	$name = urlencode($location[$i]);
	$value = urlencode($temperature_name . ':' . $temperature . ' ' . $humid_name . ':' . $humid . ' ' . $di_name . ':' . $di . ' ' . $do_name . ':' . $do);
	
	$temp = array(
	  'name' => $name,
	  'lastTime' => $lastTime,
	  'value' => $value,
	  'temperature' => $temperature,
	  'humid' => $humid,
	  'di' => $di,
	  'do_out' => $do
	);
	array_push($result, $temp);	
	
	echo urldecode(json_encode($result));	
}
?>