/* 
    From the plateform Feature Dialog, in the Azure Portal,
    Create a ref folder.
    Use KUDU Powershell console to drag and drop the Twilio.Api.dll 
    in folder ref
*/

#r "D:\home\site\wwwroot\RequestSmsSending\ref\Twilio.Api.dll"

using System.Net;
using Twilio;
 
public class SendSmsHelper
{
    internal const string ACCOUNT_SID  = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
    internal const string AUTH_TOKEN   = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
    internal const string FRED_TWILLIO = "+1978XXXXXXX";
    internal const string FRED         = "+1978XXXXXXX";
    internal const string ALLY         = "+19787XXXXXXX";
    internal const string FROM         = FRED_TWILLIO;
    
    public static string SendSMS(string text, string to)
    {
        try
        {
            var twilio = new TwilioRestClient(ACCOUNT_SID, AUTH_TOKEN);
            var msg = twilio.SendSmsMessage(FRED_TWILLIO, to, text);
            if(msg != null)
                return msg.Sid;               
        }
        catch
        {
        }
        return null;
    }
}