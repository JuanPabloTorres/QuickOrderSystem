﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApiQuickOrder.Models;

namespace WebApiQuickOrder.Service
{
    public class OktaTokenService : ITokenService
    {
        private readonly IOptions<OktaSettings> oktaSettings;

        private OktaToken token = new OktaToken();

        public OktaTokenService (IOptions<OktaSettings> oktaSettings)
        {
            this.oktaSettings = oktaSettings;
        }

        public async Task<string> GetToken ()
        {
            if( !this.token.IsValidAndNotExpiring )
            {
                this.token = await this.GetNewAccessToken();
            }
            return token.AccessToken;
        }

        private async Task<OktaToken> GetNewAccessToken ()
        {
            var token = new OktaToken();

            var client = new HttpClient();

            var client_id = this.oktaSettings.Value.ClientId;

            var client_secret = this.oktaSettings.Value.ClientSecret;

            var clientCreds = System.Text.Encoding.UTF8.GetBytes($"{client_id}:{client_secret}");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));

            var postMessage = new Dictionary<string, string>();

            postMessage.Add("grant_type", "client_credentials");

            postMessage.Add("scope", "access_token");

            var request = new HttpRequestMessage(HttpMethod.Post, this.oktaSettings.Value.TokenUrl)
            {
                Content = new FormUrlEncodedContent(postMessage)
            };

            var response = await client.SendAsync(request);

            if( response.IsSuccessStatusCode )
            {
                var json = await response.Content.ReadAsStringAsync();

                this.token = JsonConvert.DeserializeObject<OktaToken>(json);

                this.token.ExpiresAt = DateTime.UtcNow.AddSeconds(this.token.ExpiresIn);
            }
            else
            {
                throw new ApplicationException("Unable to retrieve access token from Okta");
            }
            return token;
        }
    }
}