using AutoMapper;
using DoubleVPartners.API.Models;
using DoubleVPartners.Aplication.Dtos;
using DoubleVPartners.Aplication.Interfaces;
using DoubleVPartners.Common.Crypto;
using DoubleVPartners.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DoubleVPartners.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ICrypto _crypto;
        private readonly IConfiguration _configuration;
        public UserController(IUserService service, IMapper mapper, ICrypto crypto, IConfiguration configuration)
        {
            _mapper = mapper;
            _service = service;
            _crypto = crypto;
            _configuration = configuration; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.Get(id);
            var options = _configuration.GetSection("Crypto").Get<CryptoDto>();
            if (options.Enabled)
            {
                result.Password = _crypto.Decrypt(result.Password, options);
            }
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Post([FromBody] UserModel request)
        {
            var options = _configuration.GetSection("Crypto").Get<CryptoDto>();
            if (options.Enabled)
            {
                request.Password = _crypto.Encrypt(request.Password, options);
            }
            request.IsActive = true;
            var objRequest = _mapper.Map<UserDto>(request);
            return Ok(_service.Post(objRequest));
        }


        [HttpPut]
        public IActionResult Put([FromBody] UserUpdateModel request)
        {
            var options = _configuration.GetSection("Crypto").Get<CryptoDto>();
            if (options.Enabled)
            {
                request.Password = _crypto.Encrypt(request.Password, options);
            }
            request.IsActive = true;
            var objRequest = _mapper.Map<UserDto>(request);
            return Ok(_service.Put(objRequest));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}
