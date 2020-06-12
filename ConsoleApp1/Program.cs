using System;
using System.IO;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:7071/api/Function1";
            string base64str = File.ReadAllText(@"E:\Repo\AudioBase64Converter\audio\testwebmbase64.txt");
            string responseContentTxt = @"E:\Repo\AudioBase64Converter\audio\responseContentTxt.txt";


            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;            

            byte[] btBodys = Convert.FromBase64String(base64str);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            //File.WriteAllText(responseContentTxt, responseContent);
            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();
        }
    }
}
