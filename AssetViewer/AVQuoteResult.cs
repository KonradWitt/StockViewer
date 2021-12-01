using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockViewer
{

    public class AVQuoteResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _symbol;
        private double _open;
        private double _high;
        private double _low;
        private double _price;
        private double _volume;
        private DateTime _latestTragingDay;
        private double _previousClose;
        private double _change;
        private string _changePercent;


        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Open
        {
            get { return _open; }
            set
            {
                if (_open != value)
                {
                    _open = value;
                    OnPropertyChanged();
                }
            }
        }

        public double High
        {
            get { return _high; }
            set
            {
                if (_high != value)
                {
                    _high = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Low
        {
            get { return _low; }
            set
            {
                if (_low != value)
                {
                    _low = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ChangePercent
        {
            get { return _changePercent; }
            set
            {
                if (_changePercent != value)
                {
                    _changePercent = value;
                    OnPropertyChanged();
                }
            }
        }



        public AVQuoteResult(string symbol, double open, double high, double low, double price, double volume, DateTime latestTradingDay, double previousClose, double change, string changePercent)
        {
            _symbol = symbol;
            _open = open;
            _high = high;
            _low = low;
            _price = price;
            _volume = volume;
            _latestTragingDay = latestTradingDay;
            _previousClose = previousClose;
            _change = change;
            _changePercent = changePercent;
        }



        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
