using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Dtos.Jobs;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class JobService:BaseService<Jobs>, IJobService
    {
        public JobService(
            IJobRepository job
            )
        {
            base._repository = job;
        }

        public async Task<ListResult<Jobs>> GetDataList(JobCondition condition)
        {
            Expression<Func<Jobs, bool>> exp = w =>
            string.IsNullOrEmpty(condition.Name)
            || w.Name.Contains(condition.Name);
            var data = await base.GetPageAsync(exp, w => w.CreateTime, condition.PageIndex, condition.PageSize);
            return new ListResult<Jobs>(condition.PageIndex, condition.PageSize, data.Item1, data.Item2);
        }

        public async Task<bool> JobName(string name,int id=0)
        {
            if (id == 0)
                return await base.AnyAsync(a => a.Name == name);
            else
                return await base.AnyAsync(a => a.Name == name && a.Id != id);
        }
    }
}
 