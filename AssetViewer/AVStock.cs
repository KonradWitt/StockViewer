using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockViewer
{
    public class AVStock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private string _symbol;
        private string _currency;
        private double _price;
        private string _priceStr;
        private string _changePercent;
        private double _changePercentNum;
        private AVStockChange _changeDirection;


        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Currency
        {
            get { return _currency; }
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public string PriceStr
        {
            get { return _priceStr; }
            set
            {
                if (_priceStr != value)
                {
                    _priceStr = value;
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

        public double ChangePercentNum
        {
            get { return _changePercentNum; }
            set
            {
                if (_changePercentNum != value)
                {
                    _changePercentNum = value;
                    OnPropertyChanged();
                }
            }
        }

        public AVStockChange ChangeDirection
        {
            get { return _changeDirection; }
            set
            {
                if (_changeDirection != value)
                {
                    _changeDirection = value;
                    OnPropertyChanged();
                }
            }
        }



        public AVStock(string name, string symbol, string currency, double price, string changePercent)
        {
            _name = name;
            _symbol = symbol;
            _currency = currency;
            _price = price;
            _priceStr = price.ToString() + " " + currency;
            _changePercent = changePercent;
            _changePercentNum = String2Double.GetDouble(_changePercent.Trim('%'), 0);
            _changeDirection = checkChangeDirection(_changePercentNum);
        }

        private AVStockChange checkChangeDirection(double change)
        {
            if (change < 0)
            {
                return AVStockChange.Down;
            }
            else if (change > 0)
            {
                return AVStockChange.Up;
            }
            else
            {
                return AVStockChange.NoChange;
            }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
