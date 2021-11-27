using System;
using Microsoft.Data.Analysis;

namespace StockViewer
{
    class CSVHelper
    {
        DataFrame CSV2DataFrame(string symbol, AVMode avMode)
        {
            string date = DateTime.Today.ToString("d");
            string fileName = $"{symbol}_{date}.csv";

            DataFrame stockData = DataFrame.LoadCsv(fileName);

            return stockData;
        }
    }
}