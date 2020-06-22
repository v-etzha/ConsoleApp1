using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace CallCognitiveAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://webmwavfunctionconverterapp.azurewebsites.net/api/CallFFMPeg";
            string base64str = File.ReadAllText(@"C:\Users\ethan\Desktop\AudioSource\testwebmbase64.txt");
            string convertedWavFilePath = @"C:\Users\ethan\Desktop\AudioSource\convertedwav.wav";

            using (var fs = File.Create(convertedWavFilePath))
            {

            }          

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";           
            httpWebRequest.ContentType = "application/octet-stream";
           

            byte[] btBodys = Convert.FromBase64String(base64str);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();            
            
            using (var ms = new MemoryStream())
            {
                httpWebResponse.GetResponseStream().CopyTo(ms);
                File.WriteAllBytes(convertedWavFilePath, ms.ToArray());
            }
            
            httpWebRequest.Abort();
            httpWebResponse.Close();
        }
    }
}
