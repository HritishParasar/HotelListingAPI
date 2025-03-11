using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.DTOs.Country;
using HotelListingAPI.DTOs.HotelDTOs;
using HotelListingAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IHotelsRepository repository;

        public HotelsController(IMapper mapper, IHotelsRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        [HttpGet("Id")]
        public async Task<ActionResult> GetHotelById(int Id)
        {
            var hotel = await repository.GetByIdAsync(Id);
            if (hotel == null)
                return NotFound();
            else
                return Ok(hotel);
        }
        [HttpGet]
        public async Task<ActionResult<List<GetHotelDTO>>> GetAllHotel()
        {
            var hotels = await repository.GetAllAsync();
            var items = mapper.Map<List<GetHotelDTO>>(hotels);
            return Ok(items);
        }
        [HttpPost]
        public async Task<ActionResult<Hotel>> AddHotel(AddHotelDTO hotelDTO)
        {
            var items = mapper.Map<Hotel>(hotelDTO);
            await repository.AddAsync(items);
            return Ok("SuccessFully Added!");
        }
        [HttpPut("Id")]
        public async Task<ActionResult> UpdateHotel(int Id , UpdateHotelDTO hotelDTO)
        {
            var find = await repository.GetByIdAsync(Id);
            if(find is null)
                return NotFound();
            else
                find.Name = hotelDTO.Name;
                find.Address = hotelDTO.Address;
                find.Rating = hotelDTO.Rating;
                await repository.UpdateAsync(find);
                return Ok("SuccessFully Updated!");
        }
        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteHotel(int Id)
        {
            await repository.DeleteAsync(Id);
            return Ok("Successfully deleted!");
        }

    }
}
