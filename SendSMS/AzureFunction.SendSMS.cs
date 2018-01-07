/* 
    Azure Function
*/

#load "SendSmsHelper.csx"
#load "AzureFunctionHelper.csx"

using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{    
    var ah = new AzureFunctionHelper(req, log);

    ah.Log(log, $"-- SendSMS --");

    string smsID    = null;
    var smsText     = await ah.GetParameter("smsText");
    var responseMsg = $"Invalid parameters";
    var to          = await ah.GetParameter("to", SendSmsHelper.FRED);
   
    if(smsText == null) { 
        ah.Log(log, responseMsg);
    }
    else{

        var finalSmsText = $"[{DateTime.Now}][{smsText}]";
        smsID            = SendSmsHelper.SendSMS(finalSmsText, to);
        responseMsg      = $"To:{to}, SmsID:{smsID}, SmsText:'{finalSmsText}'";
        ah.Log(log, responseMsg);
    }
    return await ah.GetResponse(smsID != null, responseMsg);
}
