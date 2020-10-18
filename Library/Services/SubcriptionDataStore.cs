using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Library.Services
{
    public class SubcriptionDataStore : DataStoreService<Subcription>, ISubcriptionDataStore
    {
        public SubcriptionDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
