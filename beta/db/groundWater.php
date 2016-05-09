<?php
header('Content-Type: application/json; charset=utf-8');

if (!isset($_GET['id'])) {
    echo 'wrong parameter';

    return;
}

$id = $_GET['id'];

$location = array('PF1-A', 'PF1-B', 'PF1', 'PF1-1', 'PF2',
                  'DU-3', 'DU-4', 'DL1-1', 'DL-2C', 'DL-2D');
              
if ($id != -1) {
    echo 'wrong parameter';

    return;
}

$result = array();
    
for ($i = 0; $i < count($location); $i++) {
	$name = urlencode($location[$i]);
    $temp = array(
      'name' => $name,
      'status' => '1',
      'lastTime' => '2016-03-14 14:01:23',
      'value' => '53.82'
    );
    array_push($result, $temp);     
}

echo urldecode(json_encode($result));   

return;

// connect to db
/*$serverName = "A0A2\SQLEXPRESS"; //serverName\instanceName
$connectionInfo = array( "Database"=>"model", "UID"=>"sa", "PWD"=>"111111");
$conn = sqlsrv_connect( $serverName, $connectionInfo);

if( $conn ) {
echo "Connection established.<br />";
}else{
echo "Connection could not be established.<br />";
die( print_r( sqlsrv_errors(), true));
}

$act_cgi = $json_dev['camera'][$real_index]['ip'] . ':' . $json_dev['camera'][$real_index]['port'] . '.cgi?act=' . $real_act; 
echo $act_cgi;*/
?>