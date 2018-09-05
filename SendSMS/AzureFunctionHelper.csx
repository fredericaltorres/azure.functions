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

    public static string GetSharedFileNamePath(string fileName)
    {
        var folder = Environment.ExpandEnvironmentVariables(@"%HOME%\data\myAzureFunction");
        var fullPath = Path.Combine(folder, fileName);
        if(!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        return fullPath;
    }
    public static bool WriteShared(string fileName, string text)
    {
        fileName = GetSharedFileNamePath(fileName);
        File.WriteAllText(fileName, text);
        return true;
    }
    public static string ReadShared(string fileName)
    {
        fileName = GetSharedFileNamePath(fileName);
        if(File.Exists(fileName)) {
            return File.ReadAllText(fileName);
        }
        else return null;
    }
    public static void DeleteShared(string fileName)
    {
        fileName = GetSharedFileNamePath(fileName);
        if(File.Exists(fileName)) {
            File.Delete(fileName);
        }
    }
} 
