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
    internal const string ACCOUNT_SID  = "ACaa703f534daa6a07ec98bfd319769588";
    internal const string AUTH_TOKEN   = "fc8af06f3cb98e6ff2ccf759d96fd419";
    internal const string FRED_TWILLIO = "+19782126025";
    internal const string FRED         = "+19787606031";
    internal const string ALLY         = "+19787606113";
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