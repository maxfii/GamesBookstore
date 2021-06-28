using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesBookstore.Models
{
    public enum ItemType { Game, Book }
    public class GamesBookstoreItem
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
        public DateTime AcquisitionDate { get; set; }
    }
}
