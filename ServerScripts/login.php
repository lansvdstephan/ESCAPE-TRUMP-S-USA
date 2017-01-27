<?php

$database = mysql_connect('localhost', 'ewi3620tu6', 'MobkicGeyp7','ewi3620tu6') or die('Could not connect: ' . mysql_error());
 mysql_select_db('ewi3620tu6') or die('Could not select database');

//dit moet nog met escape
 $username = mysql_real_escape_string($_GET['username'],$database);
 $password = mysql_real_escape_string($_GET['password'],$database);

 	$query="SELECT * FROM `users` WHERE username = '".$username."' AND password = '".$password."'";
	$result = mysql_query($query) or die('Query failed:'.mysql_error());
	$num_results = mysql_num_rows($result);  
 	if(!$num_results == 1) {
		echo "Invalid username/password combination";
 	} else {
		echo "Logged in succesfully";
 	}
 ?> 