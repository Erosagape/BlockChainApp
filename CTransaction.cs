using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChainApp
{
    public class CTransaction
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Data { get; set; }
        public CTransaction(string from,string to,string data)
        {
            this.From = from;
            this.To = to;
            this.Data = data;
        }
    }
}
