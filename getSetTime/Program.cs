using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace getSetTime
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(
                    $"http://api.timezonedb.com/v2/get-time-zone?key={Api.KEY}&format=json&by=zone&zone=America/New_York&fields=formatted");

                var date = Regex.Match(response, @"\d+-\d+-\d+");
                var time = Regex.Match(response, @"\d+:\d+:\d+");

                var day = date.Value.Substring(date.Value.Length - 2, 2);
                var month = date.Value.Substring(5, 2);
                var year = date.Value.Substring(0, 4);

                var process = new ProcessStartInfo();
                process.UseShellExecute = true;
                process.WorkingDirectory = @"C:\Windows\System32";
                process.CreateNoWindow = true;
                process.FileName = "cmd.exe";
                process.Verb = "runas";
                process.Arguments = $"/C date {month}-{day}-{year} & time {time.Value}";
                Process.Start(process);
            }



        }
    }
}
