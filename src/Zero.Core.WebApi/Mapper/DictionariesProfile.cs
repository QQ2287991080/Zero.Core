using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Dictionaries;
using Zero.Core.Domain.Entities;

namespace Zero.Core.WebApi.Mapper
{
    public class DictionariesProfile:Profile
    {
        public DictionariesProfile()
        {
            CreateMap<Dictionaries, OutputDicDto>();
        }
    }
}
