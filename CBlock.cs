using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChainApp
{
    public class CBlock
    {
        public int Index { get; set; }
        readonly DateTime timeStamp;
        string owner;
        public string PreviousHash { get; set; }
        public List<CTransaction> Transactions { get; set; }
        public string Hash { get; set; }
        public CBlock(string id,DateTime currentDateTime, List<CTransaction> trans, string previousHash = "")
        {
            Index = 0;
            timeStamp = currentDateTime;
            owner = id;
            Transactions = trans;            
            PreviousHash = previousHash;
            Hash = CreateHash();
        }
        public void Encrypt()
        {
            foreach(CTransaction data in Transactions)
            {
                data.From = CHash.EncryptDES(data.From, owner);
                data.To = CHash.EncryptDES(data.To, owner);
                data.Data = CHash.EncryptDES(data.Data, owner);
            }
        }
        public void Decrypt()
        {
            foreach (CTransaction data in Transactions)
            {
                data.From = CHash.DecryptDES(owner, data.From);
                data.To = CHash.DecryptDES(owner, data.To);
                data.Data = CHash.DecryptDES(owner, data.Data);
            }
        }
        public string CalculateHash()
        {
            return CreateHash();
        }
        private string CreateHash()
        {
            string data = "{0}|{1}|{2}";

            return CHash.EncryptSHA256(string.Format(data,timeStamp,Transactions,PreviousHash),owner);
        }
    }
}
