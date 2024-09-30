using DoubleVPartners.Aplication.Dtos;

namespace DoubleVPartners.Aplication.Interfaces
{
    public interface ITaskService
    {
        bool Post(TaskDto entity);
        bool Put(TaskDto entity);
        bool Delete(int id);
        Task<TaskDto> Get(int id);
        Task<IEnumerable<TaskDto>> GetAll();
        bool ChangeAssignTask(TaskDto entity);
        Task<IEnumerable<TaskDto>> TaskByUSer(string id);
    }
}
