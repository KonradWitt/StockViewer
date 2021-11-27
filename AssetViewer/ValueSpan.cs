using System;
using System.Collections.Generic;


namespace AssetsManager.Model
{
    public class ValueSpan
    {
        public List<DateTime> TimeStamps { get; set; }
        public List<double> Values { get; set; }

        public ValueSpan()
        {
            TimeStamps = new();
            Values = new();
        }
    }
}
