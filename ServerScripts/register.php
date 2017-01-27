 <?php

//connect with the database
 $database = mysql_connect('localhost', 'ewi3620tu6', 'MobkicGeyp7') or die('Could not connect: ' . mysql_error());
 mysql_select_db('ewi3620tu6') or die('Could not select database');

//prepare data for insertion in database
 $username = mysql_real_escape_string($_GET['username'],$database);
 $password = mysql_real_escape_string($_GET['password'],$database);
 
//check if the username already exist, if not insert data into database
$query = "SELECT username FROM `users` WHERE username = '".$username."'"; 
$result = mysql_query($query) or die('Query failed:'.mysql_error());
$num_results = mysql_num_rows($result);

if ($num_results == 1){
	echo "username exists";
} else {
	$query = "INSERT INTO `ewi3620tu6`.`users` (`user_id`, `username`, `password`) VALUES(NULL, '$username', '$password')";
	$result = mysql_query($query) or die('Query failed:'.mysql_error());
	echo "account created";	
}	

?> 