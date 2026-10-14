using System;
using System.Collections.Generic;
using System.Text;

namespace CargoTrack.Entity.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
