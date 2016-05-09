<?php

error_reporting(E_ALL); 
ini_set( 'display_errors','1');


$upurl = "http://192.168.95.191/cgi-bin/ptz.cgi?mode=ptz&move=up&speed=3";
$downurl = "http://192.168.95.191/cgi-bin/ptz.cgi?mode=ptz&move=down&speed=3";
$rightpurl = "http://192.168.95.191/cgi-bin/ptz.cgi?mode=ptz&move=right&speed=3";
$lefturl = "http://192.168.95.191/cgi-bin/ptz.cgi?mode=ptz&move=left&speed=3";
$stopurl = "http://192.168.95.191/cgi-bin/ptz.cgi?mode=ptz&move=stop&speed=3";
$outurl = "http://192.168.95.191/cgi-bin/ptz.cgi?zoom=out&speed=3";
$inurl = "http://192.168.95.191/cgi-bin/ptz.cgi?zoom=in&speed=3";
$zoomstopurl = "http://192.168.95.191/cgi-bin/ptz.cgi?zoom=stop";
$homestopurl = "http://192.168.95.191/cgi-bin/ptz.cgi?move=absmove&pan=18000&tilt=9000";

$username = "admin";
$password = "admin13579";
$post_data = array(
        'fieldname1' => 'value1',
        'fieldname2' => 'value2'
  );

$options = array(
        CURLOPT_URL            => $inurl,
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
      throw new Exception(curl_error($ch), 500);

  // validate HTTP status code (user/password credential issues)
  $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
  if ($status_code != 200 && $status_code != 204)
    //  throw new Exception("Response with Status Code [" . $status_code . "].", 500);
	echo "Response with Status Code [" . $status_code . "].";
} catch(Exception $ex) {
    if ($ch != null) curl_close($ch);
    throw new Exception($ex);
}

if ($ch != null) curl_close($ch);

//echo "raw response: " . $raw_response; 

?>