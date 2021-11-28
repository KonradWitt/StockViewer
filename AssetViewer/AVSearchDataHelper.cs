using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewer
{
    class AVSearchDataHelper
    {
        private readonly DataFrame _data;
        public AVSearchDataHelper(DataFrame data)
        {
            _data = data;
        }

        public List<AVSearchResult> GetSearchResults()
        {
            List<AVSearchResult> avSearchResults = new List<AVSearchResult>();
            string symbol;
            string name;
            string type;
            string region;
            DateTime marketOpen;
            DateTime marketClose;
            string timezone;
            string currency;
            double matchScore;

            NumberFormatInfo formatProvider = new NumberFormatInfo();
            formatProvider.NumberDecimalSeparator = ".";


            for (int i = 0; i <= _data.Rows.Count - 1; i++)
            {
                //priceSpan.TimeStamps.Add((DateTime)_data["timestamp"][i]);

                symbol = (string)_data["symbol"][i];
                name = (string)_data["name"][i];
                type = (string)_data["type"][i];
                region = (string)_data["region"][i];
                marketOpen = (DateTime)_data["marketOpen"][i];
                marketClose = (DateTime)_data["marketClose"][i];
                timezone = (string)_data["timezone"][i];
                currency = (string)_data["currency"][i];
                matchScore = String2Double.GetDouble((string)_data["matchScore"][i], 0.0);


                var AVSearchresult = new AVSearchResult(symbol, name, type, region, marketOpen, marketClose, timezone, currency, matchScore);
                avSearchResults.Add(AVSearchresult);
            }

            return avSearchResults;
        }
    }
}
