using AutoMapper;
using DoubleVPartners.Aplication.Dtos;
using DoubleVPartners.Aplication.Interfaces;
using DoubleVPartners.Domain.Entities;
using DoubleVPartners.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DoubleVPartners.Aplication.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryBase<Domain.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _IUserService;

        public TaskService(IRepositoryBase<Domain.Entities.Task> taskRepository, IMapper mapper, IUserService iUserService)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _IUserService = iUserService;
        }
        public bool Delete(int id)
        {
            return _taskRepository.Delete(id);
        }

        public async Task<TaskDto> Get(int id)
        {
            var result = await _taskRepository.GetByProperty(x => x.IdTask == id);
            return _mapper.Map<TaskDto>(result);
        }

        public async Task<IEnumerable<TaskDto>> GetAll()
        {
            var result = await _taskRepository.GetAll();
            return _mapper.Map<IEnumerable<TaskDto>>(result);
        }

        public bool Post(TaskDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(String.Format(Constants.Constants.EntityIsRequerid, "Task"));

            var obj = _mapper.Map<Domain.Entities.Task>(entity);
            return _taskRepository.Add(obj);
        }

        public bool Put(TaskDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(String.Format(Constants.Constants.EntityIsRequerid, "Task"));

            var obj = _mapper.Map<Domain.Entities.Task>(entity);
            return _taskRepository.Update(obj);
        }

        public bool ChangeAssignTask(TaskDto entity)
        {
            Task<TaskDto> task = Get(entity.IdTask);
            task.Result.StatusTask = entity.StatusTask;
            _taskRepository.Clear();
            var obj = _mapper.Map<Domain.Entities.Task>(task.Result);
            return _taskRepository.Update(obj);
        }

        public async Task<IEnumerable<TaskDto>> TaskByUSer(string email)
        {
            var resultUser = await _IUserService.GetByEmail(email);
            var result = _taskRepository.GetAll().Result.Where(x => x.IdUser == resultUser.IdUser);
            return _mapper.Map<IEnumerable<TaskDto>>(result);
        }
    }
}
