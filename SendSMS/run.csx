/* 
    Azure Portal Subscription FredericALTorres@live.com
    https://musb-sms.azurewebsites.net/api/RequestSmsSending?smsText=Hello Fred&code=urah93liM/nw1bb1c3owQIb2T06yiHKaUKnHBTFX4H7LHmvcZrA==

    From the plateform Feature Dialog, in the Azure Portal,
    Create a ref folder.
    Use KUDU Powershell console to drag and drop the Twilio.Api.dll 
    in folder ref

    Deploy to Azure using Azure Functions
        https://code.visualstudio.com/tutorials/functions-extension/getting-started

    KUDU Web Site
        https://musb-sms.scm.azurewebsites.net/        

    CORS Cross Origin Scripting
    https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings        
    Use the Feature -> API -> CORS, to add the authorized caller
*/

//#r "D:\home\site\wwwroot\RequestSmsSending\ref\Twilio.Api.dll"
#load "SendSmsHelper.csx"
#load "AzureFunctionHelper.csx" 

using System.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{    
    var ah = new AzureFunctionHelper(req, log);
    ah.Log(log, $"-- RequestSendingSMS --");

    var lastSMSFileName = "LastSMS.txt";
    var lastAnswerFileName = "LastAnswer.txt";
    string smsID    = null;
    var action      = await ah.GetParameter("action");
    var smsText     = await ah.GetParameter("smsText");
    var responseMsg = $"Invalid parameters";
    var to          = await ah.GetParameter("to", SendSmsHelper.FRED);
   
    if(action != null) { 

        switch(action.ToLowerInvariant()) {
            /*
                TWILLIO Call Back 
                    Configuration 
                        https://www.twilio.com/console/phone-numbers/PN4d0164a91ccc821b3c488e786af70ead
                        callback: https://musb-sms.azurewebsites.net/api/RequestSmsSending?code=Spm1mZhsd4aItGsSGPH5OcboIvcYfaMKBIIFEGsqP11SQaAcFQQ==&action=setAnswer
                    how to setAnswer from a browser
                    https://musb-sms.azurewebsites.net/api/RequestSmsSending?code=1mZhsd4aItGsSGPH5OcboIvcYfaMKBIIFEGsqP11SQaAcFQQ==&action=setAnswer&body=ManualTest
             */
            case "setanswer" : // Http call made by the Twillio server when user send an answer
                smsText = await ah.GetParameter("body");
                AzureFunctionHelper.WriteShared(lastAnswerFileName, smsText); // Write the last SMS into a shared file       
                responseMsg      = $@"{{ ""Status"":""OK"", ""Answer"":""{smsText}"" }}";
                smsID = "ok";
            break;
            case "getanswer" :
                var lastAnswer = AzureFunctionHelper.ReadShared(lastAnswerFileName);
                if(lastAnswer != null) {
                    AzureFunctionHelper.DeleteShared(lastAnswerFileName);
                }
                responseMsg      = $@"{{ ""Status"":""OK"", ""lastAnswer"":""{lastAnswer}"" }}";
                smsID = "ok";
            break;
            case "sendsms":
                var finalSmsText = $"[{DateTime.Now}][{smsText}]";
                smsID            = SendSmsHelper.SendSMS(finalSmsText, to);
                responseMsg      = $@"{{ ""To"":""{to}"", ""SmsID"":""{smsID}"", ""SmsText"":""{finalSmsText}"" }}";
            break;
        }
    }
    ah.Log(log, responseMsg);
    return await ah.GetResponse(smsID != null, responseMsg);
}

