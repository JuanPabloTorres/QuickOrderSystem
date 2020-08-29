using Library.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Library.Factories
{
    public interface IAuthCodeFactory
    {
        AuthCode CreateAuthCode(string code, string email);
    }
    public class AuthCodeFactory : IAuthCodeFactory
    {
        public AuthCode CreateAuthCode(string code, string email)
        {
            return new AuthCode
            {
                Code = code,
                Email = email,
                IsAlive = true,
                CreatedAt = DateTime.Now,
                EndAt = DateTime.Now.AddHours(0.5)
            };
        }
    }
}
