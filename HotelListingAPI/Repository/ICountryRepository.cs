using HotelListingAPI.Data;

namespace HotelListingAPI.Repository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetCountryDetails(int Id);
    }
}
