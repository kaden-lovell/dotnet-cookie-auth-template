using System;
using System.Security.Cryptography.X509Certificates;

namespace ClientPortalApi.Models {
    public class Server : Model {
        public long Id { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}