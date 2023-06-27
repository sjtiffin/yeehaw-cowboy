namespace yeehaw.Helpers;

using AutoMapper;
using yeehaw.Entities;
using yeehaw.Models.Tasks;

public class AutoMapperProfile : Profile {
  public AutoMapperProfile() {
    CreateMap<CreateRequest, Task>();
    CreateMap<UpdateRequest, Task>();
  }
}

