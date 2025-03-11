using HotelListingAPI.Context;
using HotelListingAPI.Data;

namespace HotelListingAPI.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}
