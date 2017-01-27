<?php
    $database = mysql_connect('localhost', 'ewi3620tu6', 'MobkicGeyp7') or die('Could not connect: ' . mysql_error());
    mysql_select_db('ewi3620tu6') or die('Could not select database');


    $username = mysql_real_escape_string($_GET['username'],$database); 
    $sql = "SELECT user_id FROM `users` WHERE username = '$username'";
    $id = mysql_query($sql) or die ('Query failed: '.mysql_error());
    $user_id=mysql_fetch_row($id)[0];

    $query = "SELECT score FROM `highscores` WHERE `user_id` = '$user_id' ORDER by `score` DESC LIMIT 10";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
    $num_results = mysql_num_rows($result);  
 
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $username . "&" . $row['score'] . "&";
    }
?>