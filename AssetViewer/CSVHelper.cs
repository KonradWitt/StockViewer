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
            switch(avFunction)
            {
                case AVFunction.dailyAdjusted:
                    fileName = $"{symbol}_DailyAdjusted_{date}.csv";
                    break;
                case AVFunction.quote:
                    fileName = $"{symbol}_Quote_{date}.csv";
                    break;
                case AVFunction.search:
                    fileName = $"{symbol}_Search_{date}.csv";
                    break;
            }

            DataFrame data = DataFrame.LoadCsv(fileName);
            return data;
        }
    }
}