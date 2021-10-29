using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChainApp
{
    public class CBlockChain
    {
        public IList<CBlock> Chain { get; set; }
        private string keyString { get; set; }
        public CBlockChain(string key)
        {
            keyString = key;
            Chain = new List<CBlock>();
        }
        private CBlock CreateGenesisBlock()
        {
            return new CBlock(keyString, DateTime.Now, null);
        }
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }
        public CBlock GetLastBlock()
        {
            return Chain[Chain.Count - 1];
        }
        public void AddBlock(CBlock block)
        {
            if (Validate() == false)
                return;
            CBlock lastBlock = GetLastBlock();
            block.Index = lastBlock.Index + 1;
            block.PreviousHash = lastBlock.Hash;
            block.Hash=block.CalculateHash();
            block.Encrypt();
            Chain.Add(block);
        }
        public CBlock? GetBlock(int index)
        {
            CBlock? block =  Chain.Where(x => x.Index.Equals(index)).FirstOrDefault();
            if (block != null)
            {
                block.Decrypt();
            }
            return block;
        }
        public bool Validate()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                CBlock current = Chain[i];
                CBlock previous = Chain[i - 1];
                if (current.Hash != current.CalculateHash())
                {
                    return false;
                }
                if (current.PreviousHash != previous.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
