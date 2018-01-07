# Azure Functions
Examples of Azure Functions in C# or JavaScript.

## SendSMS
An Azure Function triggered by an HTTP URL (web hook) to send an SMS using
Twillio.

### Parameters
* smsText: Sms Text
* to: Phone number    

### Syntax
`
https://XXXXX.azurewebsites.net/api/RequestSmsSending?code=XXXX&smsText=HowAreYou&To=+19787606031
`




