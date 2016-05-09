<?php
if (!isset($_GET['index']) ||
	!isset($_GET['act']))
{
	echo 'wrong parameter';
	return;
}

$index = $_GET['index'];
$act = $_GET['act'];

//get json file
$json_dev_file = file_get_contents("dev.json");
$json_dev = json_decode($json_dev_file, true);
$real_index = -1;
//find real cam index
for ($i = 0; $i < count($json_dev['camera']) ; $i++ ) {
	if ($json_dev['camera'][$i]['id'] == $index)
	{
		$real_index = $i;
		break;
	}
}
//cannot find correct device, this is a bug
if ($real_index == -1)
{
	echo 'wrong camera index';
	return;
}

// connect to db
$serverName = "A0A2\SQLEXPRESS"; //serverName\instanceName
$connectionInfo = array( "Database"=>"model", "UID"=>"sa", "PWD"=>"111111");
$conn = sqlsrv_connect( $serverName, $connectionInfo);

if( $conn ) {
echo "Connection established.<br />";
}else{
echo "Connection could not be established.<br />";
die( print_r( sqlsrv_errors(), true));
}

$act_cgi = $json_dev['camera'][$real_index]['ip'] . ':' . $json_dev['camera'][$real_index]['port'] . '.cgi?act=' . $real_act; 
echo $act_cgi;
//phpinfo();
?>