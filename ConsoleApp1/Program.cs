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
            string url = "https://eastus.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1?language=zh-CN&format=detailed";
            string base64str = File.ReadAllText(@"E:\Repo\AudioBase64Converter\audio\convertwavbase64.txt");
            string responseContentTxt = @"E:\Repo\AudioBase64Converter\audio\responseContentTxt.txt";


            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = @"application/json;text/xml";
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;
            httpWebRequest.Host = "eastus.stt.speech.microsoft.com";
            httpWebRequest.ContentType = @"audio/wav; codecs=audio/pcm; samplerate=16000";
            httpWebRequest.Headers["Ocp-Apim-Subscription-Key"] = "a7ab1a67c9544c529f877cabd75c67e6";
            var s = httpWebRequest.GetRequestStream();
            var strings = new StringContent(base64str);

            byte[] btBodys = Convert.FromBase64String(base64str);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            File.WriteAllText(responseContentTxt, responseContent);
            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();
        }
    }
}
