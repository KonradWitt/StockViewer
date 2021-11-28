using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewer
{
    class AssetHistory
    {
        private readonly string _name;
        public string Name
        {
            get { return _name; }
            private set { Name = _name; }
        }

        private readonly ValueSpan _priceSpan;

        private List<ValueStamp> prices = new();
        private List<ValueStamp> quantities = new();

        public ValueSpan AssetSpan;
        

        public AssetHistory (string name, ValueSpan priceSpan)
        {
            _name = name;
            _priceSpan = priceSpan;
        }

        public void AssetOperation(DateTime timestamp, double quantity, double price)
        {
            quantities.Add(new ValueStamp(timestamp, quantity));
            prices.Add(new ValueStamp(timestamp, price));           
        }

        public void CalculateAssetSpan()
        {
            
        }

    }
}
