<?php	
	if(empty($_POST['name_39878']) && strlen($_POST['name_39878']) == 0 || empty($_POST['email_39878']) && strlen($_POST['email_39878']) == 0)
	{
		return false;
	}
	
	$name_39878 = $_POST['name_39878'];
	$input_2551 = $_POST['input_2551'];
	$email_39878 = $_POST['email_39878'];
	
	$to = 'receiver@yoursite.com'; // Email submissions are sent to this email

	// Create email	
	$email_subject = "Message from a Blocs website.";
	$email_body = "You have received a new message. \n\n".
				  "Name_39878: $name_39878 \nInput_2551: $input_2551 \nEmail_39878: $email_39878 \n";
	$headers = "MIME-Version: 1.0\r\nContent-type: text/plain; charset=UTF-8\r\n";	
	$headers .= "From: contact@yoursite.com\n";
	$headers .= "Reply-To: $email_39878";	
	
	mail($to,$email_subject,$email_body,$headers); // Post message
	return true;			
?>