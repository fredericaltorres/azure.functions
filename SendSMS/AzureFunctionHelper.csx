/* 
  
 */
 
using System.Net;
 
public class AzureFunctionHelper {

    HttpRequestMessage _req;
    TraceWriter _log;

    public AzureFunctionHelper(HttpRequestMessage req, TraceWriter log) {
        this._req = req;
        this._log = log;
    }
    public async Task<HttpResponseMessage> GetResponse(bool succeeded, string msg) {

        return this._req.CreateResponse( succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest, msg);
    }
    private static dynamic _bodyData = null;
    public async Task<dynamic> GetBody() {
        
        if(_bodyData == null)
            _bodyData = await this._req.Content.ReadAsAsync<object>();
        return _bodyData;
    }
    public async Task<string> GetParameter(string paramName, string defaultValue = null) {

        // Parse query parameter smsText
        var val  = this._req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, paramName, true) == 0).Value;        
        var data = await this.GetBody();
        val      = val ?? data?[paramName];
        if(string.IsNullOrEmpty(val))
            val = defaultValue; 

        return val;
    }
    public void Log(TraceWriter log, string msg) {
    
        this._log.Info($"[{DateTime.Now}]{msg}");  
    }
} 