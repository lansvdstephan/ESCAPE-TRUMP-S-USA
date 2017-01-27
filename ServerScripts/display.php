<?php
    $database = mysql_connect('localhost', 'ewi3620tu6', 'MobkicGeyp7') or die('Could not connect: ' . mysql_error());
    mysql_select_db('ewi3620tu6') or die('Could not select database');
 
    $query = "SELECT username, score FROM `highscores`, `users` WHERE `users`.`user_id` = `highscores`.`user_id` ORDER by `score` DESC LIMIT 10";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
    $num_results = mysql_num_rows($result);  
 
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['username'] . "&" . $row['score'] . "&";
    }
?>