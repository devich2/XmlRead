using System.Collections.Generic;

namespace Xml
{
    public class Stat
    {
        public int CdsCount { get; set; }
        public double PricesSum { get; set; }
        public List<string> Countries { get; set; } = new List<string>();
        public int MinYear { get; set; } = int.MaxValue;
        public int MaxYear { get; set; }
    }
}