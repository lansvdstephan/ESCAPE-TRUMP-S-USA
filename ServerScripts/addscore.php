<?php 
        $database = mysql_connect('localhost', 'ewi3620tu6', 'MobkicGeyp7', 'ewi3620tu6') or die('Could not connect: ' . mysql_error());
        mysql_select_db('ewi3620tu6') or die('Could not select Database');
 
        $score = $_GET['score']; 
	$username = mysql_real_escape_string($_GET['username'],$database); 
	$sql = "SELECT user_id FROM `users` WHERE username = '$username'";
	$id = mysql_query($sql) or die ('Query failed: '.mysql_error());
	$user_id=mysql_fetch_row($id)[0];

            $query = "INSERT INTO `ewi3620tu6`.`highscores` (score, user_id) VALUES ('$score', '$user_id')"; 
            $result = mysql_query($query) or die('Query failed: '.mysql_error()); 
		echo 'High score is added to the database succesfully'; 
        
?> 