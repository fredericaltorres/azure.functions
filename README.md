# Azure Functions
Examples of Azure Functions in C# or JavaScript.

## SendSMS
An Azure Function triggered by an HTTP URL (web hook) to send an SMS using
Twillio.

### Parameters
* smsText: Sms Text
* to: Phone number    

### HTPP Get Syntax
`
https://XXXXX.azurewebsites.net/api/RequestSmsSending?code=XXXX&smsText=HowAreYou&To=+19787606031
`

**Notes:** *You can also use an HTTP POST.*


git add .
git status
git commit -m "Update..."
git push origin master

