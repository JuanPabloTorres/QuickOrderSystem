using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreLicenseController : ControllerBase
    {
        private readonly QOContext _context;

        public StoreLicenseController (QOContext context)
        {
            _context = context;
        }

        // DELETE: api/StoreLicenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreLicense>> DeleteStoreLicense (Guid id)
        {
            var storeLicense = await _context.StoreLicenses.FindAsync(id);
            if( storeLicense == null )
            {
                return NotFound();
            }

            _context.StoreLicenses.Remove(storeLicense);

            await _context.SaveChangesAsync();

            return storeLicense;
        }

        // GET: api/StoreLicenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreLicense>> GetStoreLicense (Guid id)
        {
            var storeLicense = await _context.StoreLicenses.FindAsync(id);

            if( storeLicense == null )
            {
                return NotFound();
            }

            return storeLicense;
        }

        // GET: api/StoreLicenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreLicense>>> GetStoreLicenses ()
        {
            return await _context.StoreLicenses.ToListAsync();
        }

        [HttpGet("[action]/{license}")]
        public async Task<bool> IsLicenseInUsed (string license)
        {
            var result = await _context.StoreLicenses.Where(l => l.ID.ToString() == license).FirstOrDefaultAsync();

            return result.IsUsed;
        }

        // POST: api/StoreLicenses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<StoreLicense>> PostStoreLicense (StoreLicense storeLicense)
        {
            _context.StoreLicenses.Add(storeLicense);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreLicense", new { id = storeLicense.ID }, storeLicense);
        }

        [HttpGet("[action]/{email}/{username}")]
        public async Task<bool> PostStoreLicense (string email, string username)
        {
            var newStoreLicense = new StoreLicense()
            {
                ID = Guid.NewGuid(),
                StartDate = DateTime.Today,
                IsUsed = false
            };

            _context.StoreLicenses.Add(newStoreLicense);

            await _context.SaveChangesAsync();

            if( _context.StoreLicenses.Where(l => l.ID == newStoreLicense.ID).FirstOrDefault() != null )
            {
                var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");

                var receiverEmail = new MailAddress(email, username);

                var sub = "Quick Order Lincense Code";

                var body = "<span>License Code:</span>" + newStoreLicense.ID;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt")
                };

                using( var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    IsBodyHtml = true,
                    Subject = sub,
                    Body = body
                } )
                {
                    smtp.Send(mess);
                }

                return true;
            }
            else
            {
                return false;
            }

            //return CreatedAtAction("GetStoreLicense", new { id = storeLicense.ID }, storeLicense);
        }

        // PUT: api/StoreLicenses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutStoreLicense (StoreLicense storeLicense)
        {
            _context.Entry(storeLicense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !StoreLicenseExists(storeLicense.ID) )
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpGet("[action]/{id}")]
        public bool StoreLicenseExists (Guid id)
        {
            return _context.StoreLicenses.Any(e => e.ID == id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> UpdateLicenceInCode (Guid id)
        {
            var storeLicense = await _context.StoreLicenses.Where(st => st.ID == id).FirstOrDefaultAsync();

            storeLicense.IsUsed = true;

            _context.Entry(storeLicense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !StoreLicenseExists(storeLicense.ID) )
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}