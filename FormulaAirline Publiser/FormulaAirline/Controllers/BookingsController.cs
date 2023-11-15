using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookingsController : Controller
    {
        private readonly IMessagesProducer _messagesProducer;

        public BookingsController( IMessagesProducer messagesProducer) 
        {
            _messagesProducer = messagesProducer;
        }

        // In-Memeory DB
        public static readonly List<Booking> _bookins = new List<Booking>()
        {

        };

        [HttpGet]
        public IActionResult GetBookings()
        {
           
            return Ok(_bookins);
        }

        [HttpPost]
        public IActionResult CreatingBookins(Booking NewBooking)
        {
            if(!ModelState.IsValid)
                 return BadRequest();
            
            _bookins.Add(NewBooking);

            _messagesProducer.SendMessage<Booking>(NewBooking);

            return Ok();
        }
    }
}
