using System;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TokensController : BaseController
    {
        private TokenService TokenService { get; }

        public TokensController(TokenService tokenService)
        {
            TokenService = tokenService;
        }

        [HttpGet]
        [Route("Email/{value}")]
        public async Task HandleEmailToken(string value)
        {
            await TokenService.HandleEmailToken(value);
        }
    }
}
