using AutoMapper;
using HotelListingAPI.Context;
using HotelListingAPI.Data;
using HotelListingAPI.DTOs.Country;
using HotelListingAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        
        private readonly ICountryRepository repository;
        private readonly IMapper mapper;

        public CountriesController(ICountryRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("Id")]
        public async Task<ActionResult> GetCountryById(int Id)
        {
            var country = await repository.GetCountryDetails(Id);
            if(country == null)
                return NotFound();
            else
                return Ok(country);
        }
        [HttpGet]
        public async Task<ActionResult<List<GetCountryDTO>>> GetAllCountry()
        {
            var countries = await repository.GetAllAsync();
            var items = mapper.Map<List<GetCountryDTO>>(countries);
            return Ok(countries);   
        }
        [HttpPost]
        public async Task<ActionResult<Country>> AddCountry(AddCountryDTO Addcountry)
        {
            var item = mapper.Map<Country>(Addcountry); 
            await repository.AddAsync(item);
            return Ok("SuccessFully Added!");
        }
        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteCountry(int Id)
        {
            await repository.DeleteAsync(Id);
            return Ok("Successfully Deleted!");
        }
        [HttpPut("Id")]
        public async Task<ActionResult> UpdateCountry(int Id, UpdateCountryDTO country)
        {

            var find = await repository.GetByIdAsync(Id);
            if (find == null)
                return NotFound();
            find.Name = country.Name;
            find.ShortName = country.ShortName;
            await repository.UpdateAsync(find);
            return Ok("Successfully Updated!");
        }
    }
}
