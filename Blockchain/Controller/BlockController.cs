using Blockchain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using Microsoft.Extensions.Configuration;

namespace Blockchain.Controller
{
    public class BlockController : IBlockController
    {
        private readonly IBlockRepository m_Repository;

        public BlockController(IConfiguration config)
        {
            
            m_Repository = new BlockRepository(config);
        }

        public IEnumerable<Block> GetBlocks()
        {
            return m_Repository.GetBlocks();
        }

        public Block GetLatestBlock()
        {
            return m_Repository.GetLatestBlock();
        }

        public Block GenerateNextBlock(string blockData)
        {
            var latestBlock = m_Repository.GetLatestBlock() ?? CreateNewChain();

            var newBlock = new Block
            {
                Index = latestBlock.Index + 1,
                PreviousHash = latestBlock.Hash,
                Timestamp = DateTime.Now,
                Data = blockData
            };

            newBlock.Hash = CalculateHash(newBlock);
            m_Repository.AddBlock(newBlock);

            return newBlock;
        }        

        private Block CreateNewChain()
        {
            var genesisBlock = new Block
            {
                Index = 0,
                PreviousHash = "0",
                Timestamp = DateTime.Now,
                Data = "The genesis block",
            };

            genesisBlock.Hash = CalculateHash(genesisBlock);
            m_Repository.AddBlock(genesisBlock);

            return genesisBlock;
        }

        private static string CalculateHash(Block block)
        {            
            var s = string.Join("|", new string[]
            {
                block.Index.ToString(),
                block.PreviousHash,
                block.Timestamp.ToString("o"),
                block.Data
            });                    
                    
            var sha = SHA256.Create();
            var crypto = sha.ComputeHash(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetByteCount(s));

            var hash = new StringBuilder();
            foreach (byte b in crypto)
            {
                hash.Append(b.ToString("x2"));
            }

            return hash.ToString();
        }
    }

    public interface IBlockController
    {
        Block GenerateNextBlock(string blockData);
        IEnumerable<Block> GetBlocks();
        Block GetLatestBlock();
    }
}
