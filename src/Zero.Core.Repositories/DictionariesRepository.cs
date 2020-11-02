using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IRepositories.Base;
using Zero.Core.Repositories.Basee;

namespace Zero.Core.Repositories
{
    public class DictionariesRepository : Repository<Dictionaries>, IDictionariesRepository
    {
        public DictionariesRepository(IUnitOfWork unit) : base(unit)
        {
        }


        public async Task<string> GetNameAsync(int? id) 
        {
            var info = await base.FirstAsync(f => f.Id == id);
            if (info == null)
                return "";
            else
                return info.Name;
        }
        public async Task<string> GetNamesAsync(IEnumerable<int> ids, string separator = ",")
        {
            var list = await base.GetAllAsync(w => ids.Contains(w.Id));
            return string.Join(separator, list);
        }
        public async Task<int?> GetIdByNameAsync(string name)
        {
            var info = await base.FirstAsync(f => f.Name == name);
            if (info == null)
                return null;
            else
                return info.Id;
        }
        public async Task<IEnumerable<int>> GetIdsByNamesAsync(IEnumerable<string> names)
        {
            var list = await base.GetAllAsync(w => names.Contains(w.Name));
            return list.Select(s => s.Id);
        }
        public  string GetName(int? id)
        {
            var info = base.First(f => f.Id == id);
            if (info == null)
                return "";
            else
                return info.Name;
        }
        public  string GetNames(IEnumerable<int> ids, string separator = ",")
        {
            var list =  base.GetAll(w => ids.Contains(w.Id));
            return string.Join(separator, list);
        }
        public  int? GetIdByName(string name)
        {
            var info =  base.First(f => f.Name == name);
            if (info == null)
                return null;
            else
                return info.Id;
        }
        public  IEnumerable<int> GetIdsByNames(IEnumerable<string> names, string separator = ",")
        {
            var list = base.GetAll(w => names.Contains(w.Name));
            return list.Select(s => s.Id);
        }
    }
}
