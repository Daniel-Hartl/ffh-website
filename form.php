<?php

require __DIR__ . '/vendor/autoload.php';

use Dotenv\Dotenv;
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;

$dotenv = Dotenv::createImmutable(__DIR__);
$dotenv->load();

if ($_SERVER["REQUEST_METHOD"] !== "POST") {
    exit;
}
// check honeypot value (not user visible)
if (!empty($_POST['website'])) {
    exit;
}

$name = htmlspecialchars($_POST["name"]);
$email = filter_var($_POST["email"], FILTER_VALIDATE_EMAIL);
$subject = htmlspecialchars($_POST["subject"]);
$message = htmlspecialchars($_POST["message"]);

if (!$email) {
    die("Ungültige Email-Adresse");
}

$mail = new PHPMailer(true);

try {

    $mail->isSMTP();
    $mail->Host = $_ENV['SMTP_HOST'];
    $mail->SMTPAuth = true;
    $mail->Username = $_ENV['SMTP_USERNAME'];
    $mail->Password = $_ENV['SMTP_PASSWORD'];
    $mail->SMTPSecure = $_ENV['SMTP_SECURE'];
    $mail->Port = $_ENV['SMTP_PORT'];

    $mail->setFrom($_ENV['FROM_EMAIL'], $name);
    $mail->addReplyTo($email, $name);
    $mail->addAddress($_ENV['TO_EMAIL']);

    $mail->isHTML(true);
    $mail->Subject = "[Kontaktanfrage] $subject";

    $mail->Body =
        "<b>Name:</b> $name<br>" .
        "<b>Email:</b> <a href='mailto:$email'>$email</a><br>" .
        "<b>Betreff:</b> $subject<br>" .
        "------------------------------<br>" .
        "<b>Nachricht:</b><br>$message";

    $mail->send();

    header('Location: form-submitted.html');
    exit;

} catch (Exception $e) {

    echo "Fehler beim Senden." . $mail->ErrorInfo;

}
?>