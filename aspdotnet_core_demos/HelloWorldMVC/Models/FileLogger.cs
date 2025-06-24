namespace HelloWorldMVC.Models
{
    public class FileLogger : IMyFileLogger
    {
        public void Log(string message)
        {
            var timeStr=DateTime.Now.ToString("dd MM yy hh mm ss");
            File.AppendAllText("error_logs", "Error:" + timeStr+":"+message+"\n");   
        }
    }
}
