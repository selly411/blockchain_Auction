using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.Model
{
    public class Metadata
    {
        [JsonProperty("ImageId")]
        public string ImageId { get; set; }

        [JsonProperty("Ext")]
        public string Ext { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        //[JsonProperty("Appraisal")]
        //public bool Appraisal { get; set; }

        [JsonProperty("Rare")]
        public string Rare { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
