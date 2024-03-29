﻿using System;
using Microsoft.Data.Analysis;
using System.Globalization;


namespace StockViewer
{
    class AVDailyAdjustedDataHelper
    {
        private readonly DataFrame _data;
        public AVDailyAdjustedDataHelper(DataFrame data)
        {
            _data = data;
        }

        private int GetRowOfDate(DataFrameColumn dates, DateTime date, int defaultRow = - 1)
        {
            int rowIndex = defaultRow;

            for (int i=0; i<dates.Length;i++)
            {
                if ((DateTime)dates[i] <= date)
                {
                    rowIndex = i;
                    break;
                }
            }

            return rowIndex;
        }

        public ValueSpan GetPriceSpan(double defaultPrice)
        {
            ValueSpan priceSpan = new ValueSpan();

            NumberFormatInfo formatProvider = new NumberFormatInfo();
            formatProvider.NumberDecimalSeparator = ".";
            
            for(int i=0; i<_data.Rows.Count; i++)
            {
                priceSpan.TimeStamps.Add((DateTime)_data["timestamp"][i]);
                try
                {
                    priceSpan.Values.Add(Convert.ToDouble(_data["close"][i], formatProvider));
                }
                catch
                {
                    priceSpan.Values.Add(defaultPrice);
                }
            }

            return priceSpan;
        }

        /*
        public ValueSpan GetPriceSpan (DateTime dateStart, DateTime dateEnd, double defaultPrice = -1)
        {
            ValueSpan priceSpan = new ValueSpan();
            

            int rowIndexStart = GetRowOfDate(_data["timestamp"], dateStart, -1);
            int rowIndexEnd = GetRowOfDate(_data["timestamp"], dateEnd, -1);

            NumberFormatInfo formatProvider = new NumberFormatInfo();
            formatProvider.NumberDecimalSeparator = ".";
            for (int i = rowIndexEnd; i <= rowIndexStart; i++)
            {
                priceSpan.TimeStamps.Add((DateTime)_data["timestamp"][i]);
                try
                {
                    priceSpan.Values.Add(Convert.ToDouble(_data["close"][i], formatProvider));
                }
                catch
                {
                    priceSpan.Values.Add(defaultPrice);
                }
            }

            return priceSpan;
        }
        */
    }
}
