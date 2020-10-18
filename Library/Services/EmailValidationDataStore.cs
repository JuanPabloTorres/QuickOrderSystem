using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Library.Services
{
    public class EmailValidationDataStore : DataStoreService<EmailValidation>, IEmailValidationService
    {
        public EmailValidationDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
