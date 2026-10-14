using CargoTrack.DataAccess.Context;
using CargoTrack.DataAccess.Repositories.GenericRepositories;
using CargoTrack.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoTrack.DataAccess.Repositories.Cities
{
    public class CityRepository(AppDbContext context) : GenericRepository<City>(context), ICityRepository
    {
    }
}
