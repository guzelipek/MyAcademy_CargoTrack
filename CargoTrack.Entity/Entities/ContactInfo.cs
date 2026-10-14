using CargoTrack.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoTrack.Entity.Entities
{
    public class ContactInfo:BaseEntity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
