using AutoMapper;
using DoubleVPartners.API.Models;
using DoubleVPartners.Aplication.Dtos;
using DoubleVPartners.Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoubleVPartners.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _service;
        private readonly IMapper _mapper;
        public TaskController(ITaskService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Post([FromBody] TaskModel request)
        {
            var objRequest = _mapper.Map<TaskDto>(request);
            return Ok(_service.Post(objRequest));
        }


        [HttpPut]
        public IActionResult Put([FromBody] TaskUpdateModel request)
        {
            var objRequest = _mapper.Map<TaskDto>(request);
            return Ok(_service.Put(objRequest));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }

        [HttpPut("changesAssign")]
        public IActionResult ChangeAssign([FromBody] TaskChangeAssignModel request)
        {
            var objRequest = _mapper.Map<TaskDto>(request);
            return Ok(_service.ChangeAssignTask(objRequest));
        }
        [HttpGet("taskByUser/{email}")]
        public async Task<IActionResult> TaskByUser(string email)
        {
            var result = await _service.TaskByUSer(email);
            return Ok(result);
        }
    }
}
