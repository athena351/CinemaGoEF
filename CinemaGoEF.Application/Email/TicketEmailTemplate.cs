 namespace CinemaGoEF.Application.Email;

public static class TicketEmailTemplate
{
    public static string Create(
    string movieTitle,
    string genre,
    string duration,
    string date,
    string time,
    string hall,
    string ticketsHtml,
    decimal totalPrice,
    string customerName)
{
    return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>

<body style='
    margin: 0;
    padding: 30px;
    background-color: #111111;
    font-family: Arial, Helvetica, sans-serif;
'>

<table width='100%' cellpadding='0' cellspacing='0'>
<tr>
<td align='center'>

<table width='600' cellpadding='0' cellspacing='0'
       style='
           max-width: 600px;
           background-color: #1c1c1c;
           border-radius: 18px;
           overflow: hidden;
           color: white;
       '>

    <!-- HEADER -->

    <tr>
        <td style='
            background-color: #8B5CF6;
            padding: 28px 30px;
        '>

            <div style='
                font-size: 28px;
                font-weight: bold;
                color: white;
            '>
                🎬 CinemaGo
            </div>

            <div style='
                margin-top: 6px;
                font-size: 13px;
                color: #F3E8FF;
            '>
                Your Movie Tickets
            </div>

        </td>
    </tr>


    <!-- MOVIE -->

    <tr>
        <td style='padding: 30px;'>

            <div style='
                color: #999999;
                font-size: 11px;
                text-transform: uppercase;
                letter-spacing: 1px;
            '>
                Movie
            </div>

            <div style='
                margin-top: 8px;
                font-size: 29px;
                font-weight: bold;
                color: #ffffff;
            '>
                {movieTitle}
            </div>

            <div style='
                margin-top: 8px;
                color: #aaaaaa;
                font-size: 14px;
            '>
                {genre} &nbsp; • &nbsp; {duration} min
            </div>


            <div style='
                margin: 25px 0;
                border-top: 1px dashed #555555;
            '>
            </div>


            <!-- DETAILS -->

            <table width='100%' cellpadding='0' cellspacing='0'>

                <tr>

                    <td width='50%' style='padding-bottom: 22px;'>

                        <div style='
                            color:#888888;
                            font-size:11px;
                        '>
                            DATE
                        </div>

                        <div style='
                            margin-top:6px;
                            color:#ffffff;
                            font-size:17px;
                            font-weight:bold;
                        '>
                            {date}
                        </div>

                    </td>


                    <td width='50%' style='padding-bottom: 22px;'>

                        <div style='
                            color:#888888;
                            font-size:11px;
                        '>
                            TIME
                        </div>

                        <div style='
                            margin-top:6px;
                            color:#ffffff;
                            font-size:17px;
                            font-weight:bold;
                        '>
                            {time}
                        </div>

                    </td>

                </tr>


                <tr>

                    <td width='50%'>

                        <div style='
                            color:#888888;
                            font-size:11px;
                        '>
                            HALL
                        </div>

                        <div style='
                            margin-top:6px;
                            color:#ffffff;
                            font-size:17px;
                            font-weight:bold;
                        '>
                            {hall}
                        </div>

                    </td>


                    <td width='50%'>

                        <div style='
                            color:#888888;
                            font-size:11px;
                        '>
                            CUSTOMER
                        </div>

                        <div style='
                            margin-top:6px;
                            color:#ffffff;
                            font-size:15px;
                        '>
                            {customerName}
                        </div>

                    </td>

                </tr>

            </table>

        </td>
    </tr>


    <!-- TICKETS -->

    <tr>
        <td style='padding: 0 30px 30px;'>

            <div style='
                border-top: 2px dashed #444444;
                padding-top: 25px;
            '>

                <div style='
                    color:#888888;
                    font-size:11px;
                    text-transform:uppercase;
                    letter-spacing:1px;
                    margin-bottom:15px;
                '>
                    Tickets
                </div>

                {ticketsHtml}

            </div>

        </td>
    </tr>


    <!-- TOTAL -->

    <tr>
        <td style='padding: 0 30px 30px;'>

            <table width='100%' cellpadding='0' cellspacing='0'
                   style='
                       background-color:#242424;
                       border-radius:12px;
                   '>

                <tr>

                    <td style='
                        padding:18px;
                        color:#999999;
                        font-size:13px;
                    '>
                        TOTAL
                    </td>

                    <td align='right'
                        style='
                            padding:18px;
                            color:#8B5CF6;
                            font-size:22px;
                            font-weight:bold;
                        '>
                        {totalPrice:0.00} ₾
                    </td>

                </tr>

            </table>

        </td>
    </tr>


    <!-- FOOTER -->

    <tr>
        <td style='
            background-color:#151515;
            padding:22px 30px;
            text-align:center;
            color:#777777;
            font-size:12px;
        '>

            Please show your ticket at the cinema entrance.

            <br><br>

            <span style='color:#8B5CF6; font-weight:bold;'>
                CinemaGo 🎥
            </span>

            <br>

            Your movies. Your tickets. Your experience.

        </td>
    </tr>

</table>

</td>
</tr>
</table>

</body>
</html>
";
}

    public static string VerifyEmailTemplate(string code)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>

<body style='
    margin: 0;
    padding: 0;
    background-color: #0f0f0f;
    font-family: Arial, Helvetica, sans-serif;
'>

<table width='100%' cellpadding='0' cellspacing='0' border='0'
       style='background-color: #0f0f0f; padding: 40px 15px;'>

    <tr>
        <td align='center'>

            <!-- Main Card -->
            <table width='600' cellpadding='0' cellspacing='0' border='0'
                   style='
                       max-width: 600px;
                       width: 100%;
                       background-color: #181818;
                       border-radius: 16px;
                       overflow: hidden;
                   '>

                <!-- Header -->
                <tr>
                    <td align='center'
                        style='
                            padding: 35px 30px;
                            background-color: #8B5CF6;
                        '>

                        <div style='
                            font-size: 30px;
                            font-weight: bold;
                            color: #ffffff;
                            letter-spacing: 1px;
                        '>
                            🎬 CinemaGo
                        </div>

                        <div style='
                            margin-top: 8px;
                            font-size: 14px;
                            color: #F3E8FF;
                        '>
                            Your cinema experience starts here
                        </div>

                    </td>
                </tr>

                <!-- Content -->
                <tr>
                    <td style='padding: 40px 35px;'>

                        <h1 style='
                            margin: 0 0 15px 0;
                            color: #ffffff;
                            font-size: 26px;
                            text-align: center;
                        '>
                            Verify your email
                        </h1>

                        <p style='
                            margin: 0 0 25px 0;
                            color: #bdbdbd;
                            font-size: 15px;
                            line-height: 1.7;
                            text-align: center;
                        '>
                            Welcome to CinemaGo! 🎬
                            <br>
                            Use the verification code below to confirm your email address.
                        </p>

                        <!-- Verification Code -->
                        <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                            <tr>
                                <td align='center'>

                                    <div style='
                                        display: inline-block;
                                        background-color: #242424;
                                        border: 2px dashed #8B5CF6;
                                        border-radius: 12px;
                                        padding: 20px 35px;
                                        margin: 5px 0 25px 0;
                                    '>

                                        <div style='
                                            color: #888888;
                                            font-size: 12px;
                                            text-transform: uppercase;
                                            letter-spacing: 2px;
                                            margin-bottom: 8px;
                                        '>
                                            Verification Code
                                        </div>

                                        <div style='
                                            color: #ffffff;
                                            font-size: 32px;
                                            font-weight: bold;
                                            letter-spacing: 8px;
                                        '>
                                            {code}
                                        </div>

                                    </div>

                                </td>
                            </tr>
                        </table>

                        <p style='
                            margin: 0;
                            color: #888888;
                            font-size: 13px;
                            line-height: 1.6;
                            text-align: center;
                        '>
                            This code will expire in
                            <strong style='color: #ffffff;'>5 minutes</strong>.
                        </p>

                        <p style='
                            margin: 25px 0 0 0;
                            color: #666666;
                            font-size: 12px;
                            line-height: 1.6;
                            text-align: center;
                        '>
                            If you didn't create a CinemaGo account,
                            you can safely ignore this email.
                        </p>

                    </td>
                </tr>

                <!-- Footer -->
                <tr>
                    <td align='center'
                        style='
                            padding: 22px 30px;
                            background-color: #121212;
                            border-top: 1px solid #292929;
                        '>

                        <div style='
                            color: #8B5CF6;
                            font-size: 14px;
                            font-weight: bold;
                        '>
                            CinemaGo 🎥
                        </div>

                        <div style='
                            margin-top: 7px;
                            color: #666666;
                            font-size: 11px;
                        '>
                            Your movies. Your tickets. Your experience.
                        </div>

                    </td>
                </tr>

            </table>

        </td>
    </tr>

</table>

</body>
</html>
";
    }
    
    public static string CreateCancellationEmail(
        string username)
    {
        return $@"
<!DOCTYPE html>
<html>
<body style='
    margin:0;
    padding:30px;
    background-color:#111111;
    font-family:Arial,sans-serif;
    color:white;
'>

<div style='
    max-width:600px;
    margin:auto;
    background-color:#1c1c1c;
    padding:30px;
    border-radius:15px;
'>

    <h1 style='
        color:#8B5CF6;
        margin-bottom:25px;
    '>
        CinemaGo
    </h1>

    <h2 style='color:white;'>
        Session Cancelled
    </h2>

    <p style='color:#cccccc;'>
        Hello {username},
    </p>

    <p style='color:#cccccc;'>
        Unfortunately, the movie session you booked
        has been cancelled by the cinema.
    </p>

    <div style='
        margin-top:20px;
        padding:18px;
        background-color:#242424;
        border-radius:10px;
        color:#cccccc;
    '>
        Your reservation has been automatically cancelled.
    </div>

    <p style='
        margin-top:25px;
        color:#888888;
    '>
        We apologize for the inconvenience.
    </p>

    <p style='color:#8B5CF6;'>
        CinemaGo
    </p>

</div>

</body>
</html>";
    }
    
    public static string CreateRefundEmail(
        string username,
        decimal refundAmount)
    {
        return $@"
<!DOCTYPE html>
<html>
<body style='
    margin:0;
    padding:30px;
    background-color:#111111;
    font-family:Arial,sans-serif;
    color:white;
'>

<div style='
    max-width:600px;
    margin:auto;
    background-color:#1c1c1c;
    padding:30px;
    border-radius:15px;
'>

    <h1 style='
        color:#8B5CF6;
        margin-bottom:25px;
    '>
        CinemaGo
    </h1>

    <h2 style='color:white;'>
        Session Cancelled
    </h2>

    <p style='color:#cccccc;'>
        Hello {username},
    </p>

    <p style='color:#cccccc;'>
        Unfortunately, the movie session for which
        you purchased tickets has been cancelled.
    </p>

    <div style='
        margin-top:25px;
        padding:25px;
        background-color:#242424;
        border-radius:12px;
        text-align:center;
    '>

        <div style='
            color:#888888;
            font-size:12px;
            letter-spacing:1px;
        '>
            REFUND AMOUNT
        </div>

        <div style='
            margin-top:10px;
            color:#8B5CF6;
            font-size:30px;
            font-weight:bold;
        '>
            {refundAmount:0.00} ₾
        </div>

    </div>

    <p style='
        margin-top:25px;
        color:#cccccc;
    '>
        The refunded amount has been added to your
        CinemaGo balance.
    </p>

    <p style='color:#888888;'>
        We apologize for the inconvenience.
    </p>

    <p style='color:#8B5CF6;'>
        CinemaGo
    </p>

</div>

</body>
</html>";
    }
}