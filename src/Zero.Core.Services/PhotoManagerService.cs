using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class PhotoManagerService : BaseService<PhotoManager>, IPhotoManagerService
    {
        public PhotoManagerService(
            IPhotoManagerRepository photo
            )
        {
            _repository = photo;
        }

        public async Task<ListResult<PhotoManager>> GetDataList(PhotoManagerCondition condition)
        {

            Tuple<int, List<PhotoManager>> data = null;
            if (condition.IsPage)
            {
                data = await this.GetPageAsync(p => p.Id > 0, p => p.CreateTime, condition.PageIndex, condition.PageSize);
            }
            else
            {
                List<PhotoManager> managers = await this.GetAllAsync();
                data = new Tuple<int, List<PhotoManager>>(managers.Count, managers);
            }
            return new ListResult<PhotoManager>(condition.PageIndex, condition.PageSize, data.Item1, data.Item2);
        }
    }
}
