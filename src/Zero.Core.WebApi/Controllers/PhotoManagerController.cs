using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [SwaggerTag("轮询图管理")]
    [Route("api/[controller]")]
    //[ApiController]
    public class PhotoManagerController : ControllerBase
    {
        IPhotoManagerService _photo;
        public PhotoManagerController(
            IPhotoManagerService photo
            )
        {
            _photo = photo;
        }

        /// <summary>
        /// 图片列表 IsPage 默认为true分页，false为不分页。
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost("GetDataList")]
        [SwaggerResponse(200, "", typeof(PhotoManager))]
        public async Task<JsonResult> GetDataList(PhotoManagerCondition condition)
        {
            string ip = this.Request.Host.ToUriComponent();
            //获取所有的图片
            string dir = this.Request.Scheme + "://" + ip + "/";
            var data = await _photo.GetDataList(condition);
            foreach (var item in data.Data)
            {
                item.Url = dir + item.Url;
            }
            return AjaxHelper.Seed(Ajax.Ok, data);
        }

        /// <summary>
        /// 新增图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(PhotoManagerInput input)
        {
            IFormFile form = input.Photo;
            //获取文件后缀名
            var suf = Path.GetExtension(form.FileName);


            //var result = FileHelper.Create(form.OpenReadStream(), form.FileName);

            string[] white = { ".PNG", ".JPEG",".JPG" };
            if (!white.Contains(suf.ToUpper()))
            {
                return AjaxHelper.Seed(Ajax.Bad, $"请上传{string.Join(",", white)}图片！");
            }
            //运行路径
            var basePath = AppContext.BaseDirectory;
            //文件名
            string guid = Guid.NewGuid().ToString();
            //虚拟路径
            string vitualPath = "avatar/" + guid + suf;
            //物理路径
            string physicsPath = basePath + vitualPath;
            //文件夹
            string folder = basePath + "avatar/";

            //文件夹是否存在不存在创建
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!System.IO.File.Exists(physicsPath))
            {
                using (var fs = System.IO.File.Create(physicsPath))
                {
                    await form.CopyToAsync(fs);
                    await _photo.AddAsync(new PhotoManager
                    {
                        Link = input.Link,
                        Title = input.Title,
                        PhotoClass = input.PhotoClass,
                        Url = vitualPath
                    });
                }
            }
            return AjaxHelper.Seed(Ajax.Ok);
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete")]
        public async Task<JsonResult> Delete(int id)
        {
            await _photo.DeleteAsync(id);
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }

    public class PhotoManagerInput
    {
        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片，每次上传一张
        /// </summary>
        public IFormFile Photo { get; set; }

        /// <summary>
        /// 图片样式
        /// </summary>
        public string PhotoClass { get; set; }
        /// <summary>
        /// 页面跳转链接
        /// </summary>
        public string Link { get; set; }
    }
}
