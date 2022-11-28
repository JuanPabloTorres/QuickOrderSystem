using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;
using WebApiQuickOrder.Models;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCardController : ControllerBase
    {
        private readonly QOContext _context;

        public PaymentCardController (QOContext context)
        {
            _context = context;
        }

        // DELETE: api/PaymentCards/5
        [HttpDelete("[action]/{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeletePaymentCard (string id)
        {
            var paymentCard = await _context.PaymentCards.Where(pc => pc.ID.ToString() == id).FirstOrDefaultAsync();

            if( paymentCard == null )
            {
                return false;
            }

            _context.PaymentCards.Remove(paymentCard);

            await _context.SaveChangesAsync();

            var paymentcardResult = await _context.PaymentCards.Where(pc => pc.ID.ToString() == id).FirstOrDefaultAsync();

            if( paymentcardResult == null )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GET: api/PaymentCards
        [HttpGet("[action]/{userId}")]
        [Authorize(Policy = Policies.User)]
        public async Task<IEnumerable<PaymentCardDTO>> GetCardDTOFromUser (Guid userId)
        {
            var cards = await _context.PaymentCards.Where(c => c.UserId == userId).ToListAsync();

            List<PaymentCardDTO> paymentCardDTOs = new List<PaymentCardDTO>();

            foreach( var item in cards )
            {
                try
                {
                    string cardlastfour = item.CardNumber.Substring(( item.CardNumber.Length - 4 ));
                    var cardDtO = new PaymentCardDTO()
                    {
                        CardNumber = "●●●●" + cardlastfour,
                        HolderName = item.HolderName,
                        StripeCardId = item.StripeCardId,
                        PaymentCardId = item.ID
                    };

                    paymentCardDTOs.Add(cardDtO);
                }
                catch( Exception e )
                {
                    Console.WriteLine(e.Message);
                }
            }

            return paymentCardDTOs;
        }

        // GET: api/PaymentCards
        [HttpGet("[action]/{userId}")]
        [Authorize(Policy = Policies.User)]
        public async Task<IEnumerable<PaymentCard>> GetCardFromUser (Guid userId)
        {
            return await _context.PaymentCards.Where(c => c.UserId == userId).ToListAsync();
        }

        // GET: api/PaymentCards/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PaymentCard>> GetPaymentCard (Guid id)
        {
            var paymentCard = await _context.PaymentCards.FindAsync(id);

            if( paymentCard == null )
            {
                return NotFound();
            }

            return paymentCard;
        }

        // GET: api/PaymentCards
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PaymentCard>>> GetPaymentCards ()
        {
            return await _context.PaymentCards.ToListAsync();
        }

        // POST: api/PaymentCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = Policies.User)]
        public async Task<ActionResult<bool>> PostPaymentCard (PaymentCard paymentCard)
        {
            _context.PaymentCards.Add(paymentCard);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentCard", new { id = paymentCard.ID }, paymentCard);
        }

        // PUT: api/PaymentCards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentCard (Guid id, PaymentCard paymentCard)
        {
            if( id != paymentCard.ID )
            {
                return BadRequest();
            }

            _context.Entry(paymentCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !PaymentCardExists(id) )
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PaymentCardExists (Guid id)
        {
            return _context.PaymentCards.Any(e => e.ID == id);
        }
    }
}