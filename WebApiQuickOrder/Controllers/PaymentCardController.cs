using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCardController : ControllerBase
    {
        private readonly QOContext _context;

        public PaymentCardController(QOContext context)
        {
            _context = context;
        }

        // GET: api/PaymentCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentCard>>> GetPaymentCards()
        {
            return await _context.PaymentCards.ToListAsync();
        }

        // GET: api/PaymentCards
        [HttpGet("[action]/{userId}")]
        public async Task<IEnumerable<PaymentCard>> GetCardFromUser(Guid userId)
        {
            return _context.PaymentCards.Where(c => c.UserId == userId).ToList();
        }

        // GET: api/PaymentCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentCard>> GetPaymentCard(Guid id)
        {
            var paymentCard = await _context.PaymentCards.FindAsync(id);

            if (paymentCard == null)
            {
                return NotFound();
            }

            return paymentCard;
        }

        // PUT: api/PaymentCards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentCard(Guid id, PaymentCard paymentCard)
        {
            if (id != paymentCard.PaymentCardId)
            {
                return BadRequest();
            }

            _context.Entry(paymentCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentCardExists(id))
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

        // POST: api/PaymentCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<bool>> PostPaymentCard(PaymentCard paymentCard)
        {
            _context.PaymentCards.Add(paymentCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentCard", new { id = paymentCard.PaymentCardId }, paymentCard);
        }

        // DELETE: api/PaymentCards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentCard>> DeletePaymentCard(Guid id)
        {
            var paymentCard = await _context.PaymentCards.FindAsync(id);
            if (paymentCard == null)
            {
                return NotFound();
            }

            _context.PaymentCards.Remove(paymentCard);
            await _context.SaveChangesAsync();

            return paymentCard;
        }

        private bool PaymentCardExists(Guid id)
        {
            return _context.PaymentCards.Any(e => e.PaymentCardId == id);
        }
    }
}
