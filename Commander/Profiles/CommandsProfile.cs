using AutoMapper;
using Commander.Dtos;
using Commander.Models;
using Commander.Models.DB;
using System.Collections.Generic;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source ==> Target
            CreateMap<Command, CommandReadDto>();

            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();

            CreateMap<List<SpGetProductByPriceGreaterThan1000>, ProductViewModel>();
        }
    }
}
