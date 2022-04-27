using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>(); //Create Map method, Automapper usa Reflection para escanear esses tipos, acha as propriedades e as mapea baseada no nome
            //Convention based mapping tool
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<Movie,MovieDto>(); //Adicional para tratar excessão de update de Id
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<MembershipType, MembershipTypeDto>(); //Adicional para tratar excessão de update de Id
            Mapper.CreateMap<MembershipTypeDto, MembershipType>().ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<GenreDto, Genre>().ForMember(g => g.Id, opt => opt.Ignore());
        }
    }
}