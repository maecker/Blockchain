using Blockchain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blockchain.Controller
{
    public class PeerController : IPeerController
    {
        public void AddPeer(string url)
        {
            
        }

        public IEnumerable<string> GetPeers()
        {
            throw new NotImplementedException();
        }

        private static Block RequestLatestBlock(string url, string method)
        {
            var client = new HttpClient();
            var json = client.GetStringAsync(url + "/api/latestBlock").Result;




            
            return null;
        }
    }

    public interface IPeerController
    {
        void AddPeer(string url);
        IEnumerable<string> GetPeers();
    }
}
