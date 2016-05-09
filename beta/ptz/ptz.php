<?php
if (!isset($_GET['nvr']) ||
	!isset($_GET['id']) ||
	!isset($_GET['act']))
{
	echo 'wrong parameter';
	return;
}

$nvrId = $_GET['nvr'];
$cameraId = $_GET['id'];
$act = $_GET['act'];

//get json file
$json_dev_file = file_get_contents("./../config/dev.json");
$json_dev = json_decode($json_dev_file, true);
$find = FALSE;
$camObj;
// Get camera object from JSON
for ($i = 0; $i < count($json_dev['camera']) ; $i++ ) {
	if ($json_dev['camera'][$i]['nvr'] == $nvrId) {
		for ($j = 0; $j < count($json_dev['camera'][$i]['list']) ; $j++ ) {		
			if ($json_dev['camera'][$i]['list'][$j]['id'] == $cameraId) {
				//echo count($json_dev['camera'][$i]['list'][$j]);
				$camObj = $json_dev['camera'][$i]['list'][$j];
				$find = TRUE;
				break;
			}
		}
	}
}

//cannot find correct device, this is a bug
if ($find == FALSE)
{
	echo 'wrong camera index';
	return;
}

// Do not support PTZ
if ($camObj['ptz'] == 0)
{
	echo 'Do not support PTZ';
	return;
}

//translate ptz command
$real_act = trans_ptz($act, $camObj['ip'], $camObj['port'], $camObj['brand']);
//echo $real_act;
//send ptz command
send_ptz($real_act, $camObj['user'], $camObj['password'], $camObj['brand']);
					  
function trans_ptz($act, $ip, $port, $brand) 
{
	$trans_act = $act;
	$cgi_cmd = '';
    if ($brand == 'axis')
	{
		// Axis will not need handle stop 
		if ($act == 'stop')
		{
			return;
		}
		
		if ($act == 'up')
		{
			$trans_act = 'rtilt=10';		
		}
		else if ($act == 'down')
		{
			$trans_act = 'rtilt=-10';		
		}
		else if ($act == 'left')
		{
			$trans_act = 'rpan=-10';		
		}
		else if ($act == 'right')
		{
			$trans_act = 'rpan=10';		
		}
		else if ($act == 'in')
		{
			$trans_act = 'rzoom=10';		
		}
		else if ($act == 'out')
		{
			$trans_act = 'rzoom=-10';		
		}
		else if ($act == 'home')
		{
			$trans_act = 'home';		
		}
		
		$cgi_cmd = "http://" . $ip . ":" . $port . "/axis-cgi/com/ptz.cgi?" . $trans_act;
	}
	else //samsung
	{
		$speed = 3;
		
		if ($act == 'up' ||
			$act == 'down' ||
			$act == 'right' ||
			$act == 'left' )
		{
			$trans_act = "mode=ptz&move=" . $act ."&speed=" . $speed;		
		}
		else if ($act == 'in' ||
				 $act == 'out')
		{
			$trans_act = "zoom=" . $act ."&speed=" . $speed;			
		}
	
		else if ($act == 'home')
		{
			$trans_act = 'home';		
		}
		else if ($act == 'pt_stop')
		{
			$trans_act = "mode=ptz&move=stop";
		}
		else if ($act == 'z_stop')
		{
			$trans_act = $trans_act = "zoom=stop";		
		}
		
		$cgi_cmd = "http://" . $ip . ":" . $port . "/cgi-bin/ptz.cgi?" . $trans_act;
	}
	
	return $cgi_cmd;
}

function send_ptz($real_act, $username, $password, $brand)
{
	//if ($brand == 'axis')
	{
		$post_data = array(
			'fieldname1' => 'value1',
			'fieldname2' => 'value2'
		);

		$options = array(
			CURLOPT_URL            => $real_act,
			CURLOPT_HEADER         => true,    
			CURLOPT_VERBOSE        => true,
			CURLOPT_RETURNTRANSFER => true,
			CURLOPT_FOLLOWLOCATION => true,
			CURLOPT_SSL_VERIFYPEER => true,    // for https
			CURLOPT_USERPWD        => $username . ":" . $password,
			CURLOPT_HTTPAUTH       => CURLAUTH_DIGEST,
			CURLOPT_POST           => false
			//CURLOPT_POSTFIELDS     => http_build_query($post_data) 
		);

		$ch = curl_init();

		curl_setopt_array( $ch, $options );

		try {
			$raw_response  = curl_exec( $ch );

			// validate CURL status
			if(curl_errno($ch))
			{
				throw new Exception(curl_error($ch), 500);
			}
			// validate HTTP status code (user/password credential issues)
			$status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
			if ($status_code != 200 && $status_code != 204)
			{
				//  throw new Exception("Response with Status Code [" . $status_code . "].", 500);
				echo "Response with Status Code [" . $status_code . "].";
			}	
		} catch(Exception $ex) {
				if ($ch != null) curl_close($ch);
				throw new Exception($ex);
		}

		if ($ch != null) 
		{
			curl_close($ch);
		}
	}
	//else //samsung
	{
		
	}
	
}

?>