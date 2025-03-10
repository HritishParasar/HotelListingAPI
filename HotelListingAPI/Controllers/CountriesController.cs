using HotelListingAPI.Context;
using HotelListingAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelDbContext dbContext;

        public CountriesController(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("Id")]
        public async Task<ActionResult> GetCountryById(int Id)
        {
            var country = await dbContext.Countries.FindAsync(Id);
            if(country == null)
                return NotFound();
            else
                return Ok(country);
        }
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAllCountry()
        {
            var countries = await dbContext.Countries.ToListAsync();
            return Ok(countries);   
        }
        [HttpPost]
        public async Task<ActionResult<Country>> AddCountry(Country country)
        {
            var item = new Country
            {
                Id = country.Id,
                Name = country.Name,
                ShortName = country.ShortName
            };
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return Ok("SuccessFully Added!");
        }
        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteCountry(int Id)
        {
            var country = await dbContext.Countries.FindAsync(Id);
            dbContext.Countries.Remove(country);
            await dbContext.SaveChangesAsync();
            return Ok("Successfully Deleted!");
        }
        [HttpPut("Id")]
        public async Task<ActionResult> UpdateCountry(int Id, Country country)
        {
            if (Id != country.Id)
                return BadRequest();
            var find = await dbContext.Countries.FindAsync(Id);
            find.Id = country.Id;
            find.Name = country.Name;
            find.ShortName = country.ShortName;
            dbContext.Countries.Update(find);
            await dbContext.SaveChangesAsync();
            return Ok("Successfully Updated!");
        }
    }
}
