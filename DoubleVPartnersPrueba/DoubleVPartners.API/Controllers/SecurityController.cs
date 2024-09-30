using AutoMapper;
using DoubleVPartners.API.Models;
using DoubleVPartners.Aplication.Dtos;
using DoubleVPartners.Aplication.Interfaces;
using DoubleVPartners.Common.Crypto;
using DoubleVPartners.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoubleVPartners.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly ITokenService _service;
        private readonly IMapper _mapper;
        private readonly ICrypto _crypto;
        private readonly IConfiguration _configuration;

        public SecurityController(ITokenService service, IMapper mapper, ICrypto crypto, IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _crypto = crypto;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult GenerateToken([FromBody] InfoTokenModel request)
        {
            var options = _configuration.GetSection("Crypto").Get<CryptoDto>();
            if (options.Enabled)
            {
                request.Password = _crypto.Encrypt(request.Password, options);
            }
            var objRequest = _mapper.Map<InfoTokenDto>(request);
            (bool status, string jwt, int idRole) = _service.GenerateToken(objRequest);
            if (status)
            {
                return Ok(new { Status = status, Data = jwt, IdRole = idRole });
            }
            return Ok(new { Status = status, Data = string.Empty, IdRole = 0 });
        }
    }
}
