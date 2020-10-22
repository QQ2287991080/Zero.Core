using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Menu;
using Zero.Core.Domain.Entities;

namespace Zero.Core.WebApi.Mapper
{
    public class MenuProfile:Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, OutputMenu>();
        }
    }
}
