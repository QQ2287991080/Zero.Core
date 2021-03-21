using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices.Base;

namespace Zero.Core.IServices
{
    public interface IPhotoManagerService : IBaseService<PhotoManager>, ISupportService
    {
        Task<ListResult<PhotoManager>> GetDataList(PhotoManagerCondition condition);

    }
}
