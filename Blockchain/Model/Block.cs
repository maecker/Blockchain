using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Model
{
    public class Block
    {
        public long Index { get; set; }
        public string PreviousHash { get; set; }
        public DateTime Timestamp { get; set; }
        public string Data { get; set; }
        public string Hash { get; set; }        
    }
}
