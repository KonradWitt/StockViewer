using System;
using Microsoft.Data.Analysis;

namespace StockViewer
{
    class CSVHelper
    {
        public DataFrame CSV2DataFrame(string symbol, AVFunction avFunction)
        {
            string date = DateTime.Today.ToString("d");
            string fileName = "";
            DataFrame data = new();
            switch (avFunction)
            {
                case AVFunction.dailyAdjusted:
                    fileName = $"{symbol}_DailyAdjusted_{date}.csv";
                    data = DataFrame.LoadCsv(fileName);
                    break;
                case AVFunction.quote:
                    fileName = $"{symbol}_Quote_{date}.csv";
                    Type[] dataTypes = new Type[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) };
                     data = DataFrame.LoadCsv(fileName, ',', true, null, dataTypes, -1, 10, false, null);
                    break;
                case AVFunction.search:
                    fileName = $"{symbol}_Search_{date}.csv";
                    data = DataFrame.LoadCsv(fileName);
                    break;
            }
            return data;
        }
    }
}