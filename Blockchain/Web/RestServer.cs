using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blockchain.Controller;
using Blockchain.Model;

namespace Blockchain.Web
{    
    public class RestServer : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IBlockController m_BlockController;
        private readonly IPeerController m_PeerController;

        public RestServer(IBlockController blockController, IPeerController peerController)
        {
            m_BlockController = blockController;
            m_PeerController = peerController;
        }

        [HttpGet]
        [Route("api/blocks")]
        public IEnumerable<Block> GetBlocks()
        {
            return m_BlockController.GetBlocks();
        }

        [HttpGet]
        [Route("api/latestBlock")]
        public Block GetLatestBlock()
        {
            return m_BlockController.GetLatestBlock();
        }

        [HttpPost]
        [Route("api/mineBlock")]
        public Block MineBlock([FromBody]string blockData)
        {            
            return m_BlockController.GenerateNextBlock(blockData);
        }

        [HttpPost]
        [Route("api/addPeer")]
        public void AddPeer([FromBody]string url)
        {
            m_PeerController.AddPeer(url);
        }

        [HttpGet]
        [Route("api/peers")]
        public IEnumerable<string> GetPeers()
        {
            return m_PeerController.GetPeers();
        }
    }
}
