using System.ComponentModel;
using System.Net;

internal class Program
{
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    const string img_url_1 = @"https://as1.ftcdn.net/v2/jpg/01/07/68/98/1000_F_107689864_aDRzRTm4JYIF0AP8uTcM46Lu8ayQ397U.jpg";
    const string img_url_2 = @"https://wallpapers.com/images/featured/summer-pictures-nyv022soo0r5p1sq.jpg";
    private static async Task Main(string[] args)
    {
        var client = new HttpClient();

        // way 1
        /*HttpRequestMessage message =  new HttpRequestMessage()
        { 
            Method = HttpMethod.Get, 
            RequestUri = new Uri(img_url_1) 
        };
        HttpResponseMessage response = await client.SendAsync(message);
        Console.WriteLine($"Status :: {response.StatusCode}");
        using(FileStream fs = new FileStream(desktopPath + "/img.jpg", FileMode.Create))
        {
            await response.Content.CopyToAsync(fs);
        }*/
        // c://users/desktop/summer-pictures-nyv022soo0r5p1sq.jpg
        /*byte[] data = await client.GetByteArrayAsync(img_url_2);
        await File.WriteAllBytesAsync(desktopPath + "/" + Path.GetFileName(img_url_2),data);*/

        // WebClient
        // sync download

        /*WebClient webClient = new WebClient();
        webClient.DownloadFile(img_url_1, Path.Combine(desktopPath, Path.GetFileName(img_url_1)));*/

        // async download
        Console.WriteLine("File loading .... ");

        //DownloadFileAsync(img_url_2);
        DownloadFileAsync(@"https://ash-speed.hetzner.com/100MB.bin");

        Console.WriteLine("Continue");
        Console.ReadKey();

    }
    private static async void DownloadFileAsync(string address)
    {
        WebClient webClient = new WebClient();
        webClient.DownloadFileCompleted += Client_DownloadFileCompleted;
        webClient.DownloadProgressChanged += Client_DownloadProgressChanged;
        string fileName = Path.Combine(desktopPath, Path.GetFileName(address));
        await webClient.DownloadFileTaskAsync(address, fileName);
        // cancel download
        //webClient.CancelAsync();
        Console.WriteLine($"{Path.GetFileName(address)} - File loaded");
    }
    private static void Client_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled)
            Console.WriteLine("File downloading was canceled!");
        else
            Console.WriteLine("File downloaded succesfully!!!");
    }
    private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        Console.WriteLine($"Downloading ... {Math.Round((float)e.BytesReceived / 1024 / 1024,2),10} MB  {Math.Round((float)e.TotalBytesToReceive / 1024 / 1024,2),10} MB {e.ProgressPercentage,10}%");
    }

}