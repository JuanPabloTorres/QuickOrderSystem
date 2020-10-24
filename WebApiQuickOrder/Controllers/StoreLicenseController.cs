﻿using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<bool> PutStoreLicense(StoreLicense storeLicense)
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

            var storeLicense = await _context.StoreLicenses.Where(st => st.LicenseId == id).FirstOrDefaultAsync();

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

            if (_context.StoreLicenses.Where(l => l.LicenseId == newStoreLicense.LicenseId).Any())
            {

                //var userResult = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

                //var LicenseRequest = new UserRequest()
                //{
                //    RequestId = Guid.NewGuid(),
                //    ToUser = userResult.UserId,
                //    Type = RequestType.StoreLicensesRequest,
                //    RequestAnswer = Answer.None,
                //    Message = newStoreLicense.LicenseId.ToString()

                //};
                //_context.Requests.Add(LicenseRequest);
                //await _context.SaveChangesAsync();

                //if (_context.Requests.Where(ur=>ur.RequestId == LicenseRequest.RequestId).Any())
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}



                //SmtpClient smtpClient = new SmtpClient("domain.a2hosted.com", 25);

                //smtpClient.Credentials = new System.Net.NetworkCredential("user@example.com", "password");
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                //MailMessage mailMessage = new MailMessage(txtFrom.Text, txtTo.Text);
                //mailMessage.Subject = "Quick Order";
                //mailMessage.Body = txtBody.Text;

                //try
                //{
                //    smtpClient.Send(mailMessage);
                //    Label1.Text = "Message sent";
                //}
                //catch (Exception ex)
                //{
                //    Label1.Text = ex.ToString();
                //}


                try
                {

                    SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587); //587
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential("quickorderpr@outlook.com", "jp199494tt");

                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;

                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("quickorderpr@outlook.com", "Quick Order");
                    mail.To.Add(new MailAddress(email));
                    mail.IsBodyHtml = true;
                    mail.Subject = "Quick Order Lincense Code";
                    mail.Body = "<span>License Code:</span>" + newStoreLicense.LicenseId;

                    smtpClient.Send(mail);

                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    throw;

                }




                //var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
                //var receiverEmail = new MailAddress(email, username);

                //var sub = "Quick Order Lincense Code";
                //var body = "<span>License Code:</span>" + newStoreLicense.LicenseId;
                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt")
                //};
                //using (var mess = new MailMessage(senderEmail, receiverEmail)
                //{
                //    IsBodyHtml = true,
                //    Subject = sub,
                //    Body = body
                //})
                //{
                //    smtp.Send(mess);

                //    return true;
                //}





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
