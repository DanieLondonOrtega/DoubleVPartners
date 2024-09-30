using DoubleVPartners.API.Models;
using DoubleVPartners.Aplication.Dtos;
using DoubleVPartners.Domain.Entities;

namespace DoubleVPartners.API.Mapper
{
    public class AutoMapping : AutoMapper.Profile
    {
        public AutoMapping()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserUpdateModel, UserDto>().ReverseMap();

            CreateMap<TaskModel, TaskDto>().ReverseMap();
            CreateMap<TaskDto, Domain.Entities.Task>().ReverseMap();
            CreateMap<TaskUpdateModel, TaskDto>().ReverseMap();

            CreateMap<InfoTokenModel, InfoTokenDto>().ReverseMap();
            CreateMap<TaskChangeAssignModel, TaskDto>().ReverseMap();            
        }
    }
}
