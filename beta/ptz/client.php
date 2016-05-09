<?php

error_reporting(E_ALL); 
ini_set( 'display_errors','1');

$zurl = "http://172.20.129.213/axis-cgi/com/ptz.cgi?rzoom=10";
$purl = "http://172.20.129.213/axis-cgi/com/ptz.cgi?rpan=10";
$turl = "http://172.20.129.213/axis-cgi/com/ptz.cgi?rtilt=10";
$username = "root";
$password = "pass";
$post_data = array(
        'fieldname1' => 'value1',
        'fieldname2' => 'value2'
  );

$options = array(
        CURLOPT_URL            => $purl,
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

echo "raw response: " . $raw_response; 

?>