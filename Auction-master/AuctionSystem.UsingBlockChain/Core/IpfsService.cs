using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ipfs.Http;
using System.Net;
using System.IO;

namespace AuctionSystem.UsingBlockChain.Core
{
    public class IpfsService
    {
        private IpfsClient ipfs = null;

        public IpfsService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ipfs = new IpfsClient("https://ipfs.infura.io:5001");
        }

        public async Task<string> Upload(string path)
        {
            var node = await ipfs.FileSystem.AddFileAsync(path);

            return node.Id.ToString();
        }

        public async Task DownLoad(string hash, string path)
        {
            Stream stream = await ipfs.FileSystem.ReadFileAsync(hash);

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}
