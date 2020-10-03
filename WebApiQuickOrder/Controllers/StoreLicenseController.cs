using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        public StoreLicenseController(QOContext context)
        {
            _context = context;
        }

        // GET: api/StoreLicenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreLicense>>> GetStoreLicenses()
        {
            return await _context.StoreLicenses.ToListAsync();
        }

        // GET: api/StoreLicenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreLicense>> GetStoreLicense(Guid id)
        {
            var storeLicense = await _context.StoreLicenses.FindAsync(id);

            if (storeLicense == null)
            {
                return NotFound();
            }

            return storeLicense;
        }

        // PUT: api/StoreLicenses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutStoreLicense( StoreLicense storeLicense)
        {
           

            _context.Entry(storeLicense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreLicenseExists(storeLicense.LicenseId))
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
        public async Task<bool> UpdateLicenceInCode(Guid id)
        {

            var storeLicense = await  _context.StoreLicenses.Where(st => st.LicenseId == id).FirstOrDefaultAsync();

            storeLicense.IsUsed = true;
            _context.Entry(storeLicense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreLicenseExists(storeLicense.LicenseId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }


        }

        // POST: api/StoreLicenses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<StoreLicense>> PostStoreLicense(StoreLicense storeLicense)
        {
            _context.StoreLicenses.Add(storeLicense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreLicense", new { id = storeLicense.LicenseId }, storeLicense);
        }

        [HttpGet("[action]/{email}/{username}")]
        public async Task<bool> PostStoreLicense(string email, string username)
        {

            var newStoreLicense = new StoreLicense()
            {
                LicenseId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                IsUsed = false
            };

            _context.StoreLicenses.Add(newStoreLicense);
            await _context.SaveChangesAsync();

            if (_context.StoreLicenses.Where(l => l.LicenseId == newStoreLicense.LicenseId).FirstOrDefault() != null)
            {

                try
                {

                    //create the mail message 
                    MailMessage mail = new MailMessage();

                    //set the addresses 
                    mail.From = new MailAddress("est.juanpablotorres@gmail.com"); //IMPORTANT: This must be same as your smtp authentication address.
                    mail.To.Add(email);

                    //set the content 
                    mail.Subject = "This is an email";
                    mail.Body = "This is from system.net.mail using C sharp with smtp authentication.";
                    //send the message 
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                    //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                    NetworkCredential Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials;
                    smtp.Port = 587;    //alternative port number is 8889
                    smtp.EnableSsl = true;
                    smtp.Send(mail);







                    //var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
                    //var receiverEmail = new MailAddress(email, username);

                    //var sub = "Quick Order Lincense Code";
                    //var body = "<span>License Code:</span>" + newStoreLicense.LicenseId;
                    //var smtp = new SmtpClient
                    //{
                    //    Host = "smtp.gmail.com",
                    //    Port = 587,
                    //    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt"),
                    //    EnableSsl = true,
                    //    UseDefaultCredentials = false,
                    //    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //};
                    //using (var mess = new MailMessage(senderEmail, receiverEmail)
                    //{
                    //    IsBodyHtml = true,
                    //    Subject = sub,
                    //    Body = body
                    //})
                    //{
                    //    smtp.Send(mess);
                    //}

                    return true;
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                   
                   
                }

                return false;

            }
            else
            {
                return false;
            }

            //return CreatedAtAction("GetStoreLicense", new { id = storeLicense.LicenseId }, storeLicense);
        }

        // DELETE: api/StoreLicenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreLicense>> DeleteStoreLicense(Guid id)
        {
            var storeLicense = await _context.StoreLicenses.FindAsync(id);
            if (storeLicense == null)
            {
                return NotFound();
            }

            _context.StoreLicenses.Remove(storeLicense);
            await _context.SaveChangesAsync();

            return storeLicense;
        }

        [HttpGet("[action]/{license}")]
        public async Task<bool> IsLicenseInUsed(string license)
        {
            var result = await _context.StoreLicenses.Where(l => l.LicenseId.ToString() == license).FirstOrDefaultAsync();

            return result.IsUsed;
        }

        [HttpGet("[action]/{id}")]
        public bool StoreLicenseExists(Guid id)
        {
            return _context.StoreLicenses.Any(e => e.LicenseId == id);
        }
    }
}
