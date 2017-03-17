using Blockchain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blockchain.Controller
{
    public class BlockRepository : IBlockRepository
    {
        private List<Block> m_Blocks = new List<Block>();
        private readonly string m_Path = null;

        public BlockRepository(IConfiguration config)
        {
            var id = config.GetValue<string>("blockchain.server.id");
            var path = config.GetValue<string>("blockchain.server.dataPath");
            m_Path = string.Concat(path, id, ".json");
            Load();
        }

        public void AddBlock(Block block)
        {
            m_Blocks.Add(block);
            Save();
        }

        public Block GetLatestBlock()
        {
            return m_Blocks.Any() ? m_Blocks.Last() : null;
        }

        public IEnumerable<Block> GetBlocks()
        {
            return m_Blocks;
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(m_Blocks, Formatting.Indented);
            File.WriteAllText(m_Path, json);
        }

        private void Load()
        {
            if (File.Exists(m_Path))
            {
                var json = File.ReadAllText(m_Path);
                m_Blocks = JsonConvert.DeserializeObject<List<Block>>(json);
            }
        }
    }

    public interface IBlockRepository
    {
        Block GetLatestBlock();
        IEnumerable<Block> GetBlocks();
        void AddBlock(Block block);
    }
}
