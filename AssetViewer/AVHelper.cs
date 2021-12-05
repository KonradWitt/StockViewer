using System;
using System.IO;
using System.Net;

namespace StockViewer
{
    public class AVHelper
    {
        private readonly string _apiKey;

        public AVHelper(string apiKey)
        {
            _apiKey = apiKey;
        }

        public void SaveDailyAdjustedCSVFromURL(string symbol)
        {
            string date = DateTime.Today.ToString("d");
            string fileName = $"{symbol}_DailyAdjusted_{date}.csv";

            if (File.Exists($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{fileName}"))
            {
                return;
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://" + $@"www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={symbol}&outputsize=full&apikey={_apiKey}&datatype=csv");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            File.WriteAllText(fileName, results);
        }

        public void SaveQuoteCSVFromURL(string symbol)
        {
            string date = DateTime.Today.ToString("d");
            string fileName = $"{symbol}_Quote_{date}.csv";

            if (File.Exists($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{fileName}"))
            {
                return;
                File.Delete($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{fileName}");
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://" + $@"www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}&datatype=csv");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            File.WriteAllText(fileName, results);
        }

        public void SaveSearchCSVFromURL(string keywords)
        {
            string date = DateTime.Today.ToString("d");
            string fileName = $"{keywords}_Search_{date}.csv";

            if (File.Exists($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{fileName}"))
            {
                return;
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://" + $@"www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={keywords}&apikey={_apiKey}&datatype=csv");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            File.WriteAllText(fileName, results);
        }

    }
}