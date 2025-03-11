using HotelListingAPI.Context;
using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HotelDbContext dbContext;

        public CountryRepository(HotelDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Country> GetCountryDetails(int Id)
        {
            return await dbContext.Countries.FindAsync(Id);
        }
    }
}
