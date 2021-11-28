using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockViewer
{

    public class AVSearchResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _symbol;
        private string _name;
        private string _type;
        private string _region;
        private DateTime _marketOpen;
        private DateTime _marketClose;
        private string _timezone;
        private string _currency;
        private double _matchScore;

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

        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Region
        {
            get { return _region; }
            set
            {
                if (_region != value)
                {
                    _region = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime MarketOpen
        {
            get { return _marketOpen; }
            set
            {
                if (_marketOpen != value)
                {
                    _marketOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime MarketClose
        {
            get { return _marketClose; }
            set
            {
                if (_marketClose != value)
                {
                    _marketClose = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Timezone
        {
            get { return _timezone; }
            set
            {
                if (_timezone != value)
                {
                    _timezone = value;
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

        public double Matchscore
        {
            get { return _matchScore; }
            set
            {
                if (_matchScore != value)
                {
                    _matchScore = value;
                    OnPropertyChanged();
                }
            }
        }



        public AVSearchResult(string symbol, string name, string type, string region, DateTime marketOpen, DateTime marketClose, string timezone, string currency, double matchScore)
        {
            _symbol = symbol;
            _name = name;
            _type = type;
            _region = region;
            _marketOpen = marketOpen;
            _marketClose = marketClose;
            _timezone = timezone;
            _currency = currency;
            _matchScore = matchScore;
        }



        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
