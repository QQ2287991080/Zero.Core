using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Dictionaries;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class DictionariesService:BaseService<Dictionaries>,IDictionariesService
    {
        readonly IDictionariesRepository _dictionaries;
        readonly IMapper _mapper;
        public DictionariesService(
            IDictionariesRepository dictionaries,
            IMapper mapper )
        {
            _repository = dictionaries;
            _dictionaries = dictionaries;
            _mapper = mapper;
        }



        public async Task<bool> NameContains(string name, int? idParent, int id = 0)
        {
            if (id == 0)
            {
                return await base.AnyAsync(f => f.IdParent == idParent && f.Name == name);
            }
            else
            {
                return await base.AnyAsync(f => f.IdParent == idParent && f.Name == name && f.Id != id);
            }
        }



        public async Task<List<OutputDicDto>> GetTree()
        {
            //获取全部数据
            var list = await base.GetAllAsync();
            return Resolver(list);
        }


        private List<OutputDicDto> Resolver(IEnumerable<Dictionaries> dictionaries, int? idParent = null)
        {
            List<OutputDicDto> dtos = new List<OutputDicDto>();
            var list = dictionaries.Where(w => w.IdParent == idParent).OrderBy(ob => ob.Sort);
            foreach (var item in list)
            {
                var dto = _mapper.Map<OutputDicDto>(item);
                var childrenList = dictionaries.Where(c => c.IdParent == item.Id);
                if (childrenList.Count() > 0)
                {
                    dto.Childrens = Resolver(childrenList, item.Id);
                }
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
