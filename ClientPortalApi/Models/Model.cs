using System;

namespace ClientPortalApi.Models {
    public interface Model {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}