using System;
using System.Collections.Generic;
using System.Text;

namespace CargoTrack.DTO.DTOs.BranchDtos
{
    public class CreateBranchDto
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
    }
}
