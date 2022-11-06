using System;
namespace SegurosUdla.WebApi.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Authenticate(string userName, string password);
        public Task Register(string userName, string password);
    }
}

