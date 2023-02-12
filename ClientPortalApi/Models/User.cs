using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ClientPortalApi.Models {
    public class User : Model {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}