﻿namespace IdentityProvider.Core
{
    public static class Templates
    {
        public static string GetEmailConfirmTemplate()
        {
            const string template = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""x-ua-compatible"" content=""ie=edge"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
    <title>Account confirmation</title>
    <meta name=""description"" content=""Account confirmation Template."" />
    <style type=""text/css"">
        a:hover {
            text-decoration: underline !important;
        }
    </style>
</head>
<body marginheight=""0""
      topmargin=""0""
      marginwidth=""0""
      style=""margin: 0px; background-color: #f2f3f8""
      leftmargin=""0"">
    <!--100% body table-->
    <table cellspacing=""0""
           border=""0""
           cellpadding=""0""
           width=""100%""
           bgcolor=""#f2f3f8""
           style=""
        @import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700);
        font-family: 'Open Sans', sans-serif;
      "">
        <tr>
            <td>
                <table style=""background-color: #f2f3f8; max-width: 670px; margin: 0 auto""
                       width=""100%""
                       border=""0""
                       align=""center""
                       cellpadding=""0""
                       cellspacing=""0"">
                    <tr>
                        <td style=""height: 80px"">&nbsp;</td>
                    </tr>
                    <!--  <tr>
                      <td style=""text-align: center"">
                        <a href=""https://rakeshmandal.com"" title=""logo"" target=""_blank"">
                          <img
                            width=""60""
                            src=""https://i.ibb.co/hL4XZp2/android-chrome-192x192.png""
                            title=""logo""
                            alt=""logo""
                          />
                        </a>
                      </td>
                    </tr> -->
                    <tr>
                        <td style=""height: 10px"">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table width=""95%""
                                   border=""0""
                                   align=""center""
                                   cellpadding=""0""
                                   cellspacing=""0""
                                   style=""
                    max-width: 670px;
                    background: #fff;
                    border-radius: 3px;
                    text-align: center;
                    -webkit-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    -moz-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                  "">
                                <tr>
                                    <td style=""height: 10px"">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 0 35px"">
                                        <h1 style=""
                          color: #1e1e2d;
                          font-weight: 500;
                          margin: 0;
                          font-size: 32px;
                          font-family: 'Rubik', sans-serif;
                        "">
                                            Account Confirmation
                                        </h1>
                                        <span style=""
                          display: inline-block;
                          vertical-align: middle;
                          margin: 29px 0 26px;
                          border-bottom: 1px solid #cecece;
                          width: 100px;
                        ""></span>
                                        <p style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        "">
                                            <span>Hi <b>{UserName}</b>,</span> <br /><br />
                                            <span>Welcome to the Dama platform, we've created a new account for you.</b></span> <br /><br />
                                            <span>
                                                Finish setting up your account by clicking confirm
                                                email
                                            </span>
                                        </p>
                                        <a href=""{Url}""
                                           style=""
                          background: #12638f;
                          text-decoration: none !important;
                          font-weight: 500;
                          max-width: 50%;
                          margin-top: 35px;
                          color: #fff;
                          font-size: 14px;
                          padding: 10px 24px;
                          display: inline-block;
                          border-radius: 5px;
                        "">
                                            Confirm Email
                                        </a>

                                    </td>
                                </tr>
                                <tr>
                                    <td style=""height: 20px"">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align=""left""
                                        bgcolor=""#ffffff""
                                        style=""
                        padding: 24px;
                        font-family: 'Source Sans Pro', Helvetica, Arial,
                          sans-serif;
                        font-size: 16px;
                        line-height: 24px;
                        border-bottom: 3px solid #d4dadf;
                        text-align: center;
                      "">
                                        <p style=""margin: 0"">
                                            Thanks,<br />
                                            Dama Support
                                        </p>
                                    </td>
                                </tr>
                                <!-- end copy -->
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td style=""height: 20px"">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style=""text-align: center"">
                            <p style=""
                    font-size: 14px;
                    color: rgba(69, 80, 86, 0.7411764705882353);
                    line-height: 18px;
                    margin: 0 0 0;
                  "">
                                &copy; <strong>C.B.C Health Service</strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style=""height: 5px"">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!--/100% body table-->
</body>
</html>
";
            return template;
        }

        public static string GetPasswordResetTemplate()
        {
            const string template = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""x-ua-compatible"" content=""ie=edge"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
    <title>Request Password Reset</title>
    <meta name=""description"" content=""Request Password Reset Template."" />
    <style type=""text/css"">
        a:hover {
            text-decoration: underline !important;
        }
    </style>
</head>
<body marginheight=""0""
      topmargin=""0""
      marginwidth=""0""
      style=""margin: 0px; background-color: #f2f3f8""
      leftmargin=""0"">
    <!--100% body table-->
    <table cellspacing=""0""
           border=""0""
           cellpadding=""0""
           width=""100%""
           bgcolor=""#f2f3f8""
           style=""
        @import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700);
        font-family: 'Open Sans', sans-serif;
      "">
        <tr>
            <td>
                <table style=""background-color: #f2f3f8; max-width: 670px; margin: 0 auto""
                       width=""100%""
                       border=""0""
                       align=""center""
                       cellpadding=""0""
                       cellspacing=""0"">
                    <tr>
                        <td style=""height: 80px"">&nbsp;</td>
                    </tr>
                    <!--  <tr>
                      <td style=""text-align: center"">
                        <a href=""https://rakeshmandal.com"" title=""logo"" target=""_blank"">
                          <img
                            width=""60""
                            src=""https://i.ibb.co/hL4XZp2/android-chrome-192x192.png""
                            title=""logo""
                            alt=""logo""
                          />
                        </a>
                      </td>
                    </tr> -->
                    <tr>
                        <td style=""height: 10px"">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table width=""95%""
                                   border=""0""
                                   align=""center""
                                   cellpadding=""0""
                                   cellspacing=""0""
                                   style=""
                    max-width: 670px;
                    background: #fff;
                    border-radius: 3px;
                    text-align: center;
                    -webkit-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    -moz-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                  "">
                                <tr>
                                    <td style=""height: 10px"">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 0 35px"">
                                        <h1 style=""
                          color: #1e1e2d;
                          font-weight: 500;
                          margin: 0;
                          font-size: 32px;
                          font-family: 'Rubik', sans-serif;
                        "">
                                            Request Password Reset
                                        </h1>
                                        <span style=""
                          display: inline-block;
                          vertical-align: middle;
                          margin: 29px 0 26px;
                          border-bottom: 1px solid #cecece;
                          width: 100px;
                        ""></span>
                                        <p style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        "">
                                            <span>Hi <b>{UserName}</b>,</span> <br />
                                            <span>You have requested to reset your password.</span>
                                            <span>
                                                You can reset your password by clicking on the reset
                                                password button below.
                                            </span>
                                        </p>
                                        <a href=""{Url}""
                                           style=""
                          background: #12638f;
                          text-decoration: none !important;
                          font-weight: 500;
                          margin-top: 35px;
                          color: #fff;
                          font-size: 14px;
                          padding: 10px 24px;
                          display: inline-block;
                          border-radius: 5px;
                        "">
                                            Reset Password
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""height: 20px"">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align=""left""
                                        bgcolor=""#ffffff""
                                        style=""
                        padding: 24px;
                        font-family: 'Source Sans Pro', Helvetica, Arial,
                          sans-serif;
                        font-size: 16px;
                        line-height: 24px;
                        border-bottom: 3px solid #d4dadf;
                        text-align: center;
                      "">
                                        <p style=""margin: 0"">
                                            Thanks,<br />
                                            Dama Support
                                        </p>
                                    </td>
                                </tr>
                                <!-- end copy -->
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td style=""height: 20px"">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style=""text-align: center"">
                            <p style=""
                    font-size: 14px;
                    color: rgba(69, 80, 86, 0.7411764705882353);
                    line-height: 18px;
                    margin: 0 0 0;
                  "">
                                &copy; <strong>C.B.C Health Service</strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style=""height: 5px"">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!--/100% body table-->
</body>
</html>
";
            return template;
        }
    }
}
