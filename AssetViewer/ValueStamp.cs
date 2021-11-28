using System;

namespace StockViewer
{
    class ValueStamp
    {
        private DateTime timestamp;
        public DateTime Timestamp
        { 
            get { return timestamp; }
            set { timestamp = value; }
        }

        private double value;
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public ValueStamp(DateTime _timestamp, double _price)
        {
            Value = _price;
            Timestamp = _timestamp;
        }
    }
}
