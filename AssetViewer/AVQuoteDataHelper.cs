using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewer
{
    class AVQuoteDataHelper
    {
        private readonly DataFrame _data;
        public AVQuoteDataHelper(DataFrame data)
        {
            _data = data;
        }

        public AVQuoteResult GetQuoteResult()
        {

            string symbol;
            double open;
            double high;
            double low;
            double price;
            double volume;
            DateTime latestTradingDay;
            double previousClose;
            double change;
            string changePercent;

            NumberFormatInfo formatProvider = new NumberFormatInfo();
            formatProvider.NumberDecimalSeparator = ".";

            symbol = (string)_data["symbol"][0];
            open = String2Double.GetDouble((string)_data["open"][0], 0);
            high = String2Double.GetDouble((string)_data["high"][0], 0);
            low = String2Double.GetDouble((string)_data["low"][0], 0);
            price = String2Double.GetDouble((string)_data["price"][0], 0);
            volume = Convert.ToDouble(_data["volume"][0]);
            latestTradingDay = DateTime.Parse((string)_data["latestDay"][0]);
            previousClose = String2Double.GetDouble((string)_data["previousClose"][0], 0);
            change = String2Double.GetDouble((string)_data["change"][0], 0);
            changePercent = (string)_data["changePercent"][0];
            return new AVQuoteResult(symbol, open, high, low, price, volume, latestTradingDay, previousClose, change, changePercent);
        }
    }
}


