
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Common.Units;
using Zero.Core.Common.User;
using Zero.Core.Domain.Dtos.User;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("用户管理")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;
        readonly IUserProvider _userProvider;
        public UserController(
            IUserService userService,
            IUserProvider userProvider
            )
        {
            _userService = userService;
            _userProvider = userProvider;
        }


        #region Account
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Login"), AllowAnonymous]
        public async Task<JsonResult> Login(LoginDto dto)
        {
            var user = await _userService.FirstAsync(f => f.UserName == dto.UserName);
            if (user == null)
            {
                return AjaxHelper.Seed(System.Net.HttpStatusCode.BadRequest, "用户名错误");
            }
            if (user.Password != dto.Password)
            {
                return AjaxHelper.Seed(System.Net.HttpStatusCode.BadRequest, "密码错误");
            }
            if (user.IsLock == true)
            {
                return AjaxHelper.Seed(Ajax.Bad, "您的账号已被限制登录，请联系管理员！");
            }
            //创建token
            var token = _userProvider.CreateJwtToken(new JwtInput() { UserName = dto.UserName });
            //保存用户信息
            await _userProvider.SetToken(dto.UserName, token.Token, token.Time);
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "", new { token.Token });
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        [HttpGet("LoginOut")]
        public async Task<JsonResult> LoginOut()
        {
            await _userProvider.Clear();
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "");
        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public async Task<JsonResult> GetUserInfo()
        {
            var info = await _userService.GetUserInfo();
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        /// <summary>
        /// 获取当前用户的具体信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserDetails")]
        public async Task<JsonResult> UserDetails()
        {
            var info = await _userService.GetCenterInfo();
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        #endregion
        #region User
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost("GetDataList")]
        public async Task<JsonResult> GetDataList(UserCondition condition)
        {
            var data = await _userService.GetDataList(condition);
            return AjaxHelper.Seed(Ajax.Ok, data);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(User user)
        {
            //判断用户是否已存在
            var any = await _userService.IsUserNameExists(user.UserName);
            if (any)
                return AjaxHelper.Seed(Ajax.Bad, "该用户名已存在！");
            if (string.IsNullOrEmpty(user.RealName))
                user.RealName = user.UserName;
            if (string.IsNullOrEmpty(user.Password))
                user.Password = "123456";
            var entity = await _userService.AddAsync(user);
            return AjaxHelper.Seed(Ajax.Ok, entity);
        }
        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isLock"></param>
        /// <returns></returns>
        [HttpGet("ChangeLock")]
        public async Task<JsonResult> ChangeLock(int userId, bool isLock)
        {
            var info = await _userService.FirstAsync(userId);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "用户已经不存在！");
            info.IsLock = isLock;
            await _userService.UpdateAsync(info);
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="id">修改时传id</param>
        /// <returns></returns>
        [HttpGet("IsUserNameExists")]
        public async Task<JsonResult> IsUserNameExists(string userName, int id = 0)
        {
            var any = await _userService.IsUserNameExists(userName, id);
            return AjaxHelper.Seed(Ajax.Ok, any);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<JsonResult> Update(User user)
        {
            var info = await _userService.FirstAsync(f => f.Id == user.Id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "用户已不存在，请刷新！");
            if (await _userService.IsUserNameExists(user.UserName, user.Id))
                return AjaxHelper.Seed(Ajax.Bad, "用户名已存在，请重新输入！");
            //更新用户
            info.UserName = user.UserName;
            info.Phone = user.Phone;
            info.Email = user.Email;
            info.Sex = user.Sex;
            info.Remark = user.Remark;
            await _userService.UpdateAsync(info);
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Details")]
        public async Task<JsonResult> Details(int id)
        {
            var info = await _userService.FirstAsync(f => f.Id == id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "用户已不存在，请刷新！");
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete")]
        public async Task<JsonResult> Delete(int id)
        {
            var info = await _userService.FirstAsync(f => f.Id == id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "用户已不存在，请刷新！");
            //根据id删除实体（软删除）
            await _userService.DeleteAsync(id);
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("SetRole")]
        public async Task<JsonResult> SetRole(int userId, int roleId)
        {
            await _userService.SetRole(userId, roleId);
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("RemoveRole")]
        public async Task<JsonResult> RemoveRole(int userId, int roleId)
        {
            await _userService.RemoveRole(userId, roleId);
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 获取用户绑定的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUserRole")]
        public async Task<JsonResult> GetUserRole(int userId)
        {
            var data = await _userService.GetUserRole(userId);
            return AjaxHelper.Seed(Ajax.Ok, data);
        }

        #endregion
        #region Center
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordCfm"></param>
        /// <returns></returns>
        [HttpGet("ResetPassword")]
        public async Task<JsonResult> ResetPassword(string password, string passwordCfm)
        {
            if (password != passwordCfm)
                return AjaxHelper.Seed(Ajax.Bad, "两个密码不一致！");
            var user = await _userService.FirstAsync(f => f.UserName == _userProvider.UserName);
            user.Password = password;
            await _userService.UpdateAsync(user);
            return AjaxHelper.Seed(Ajax.Ok);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("UploadAvatar")]
        public async  Task<JsonResult> UploadAvatar(IFormFile form)
        {
            if (form == null)
                return AjaxHelper.Seed(Ajax.Bad, "图片不能为空！");
            //user
            var user = await _userService.FirstAsync(f => f.UserName == _userProvider.UserName);
            //获取文件后缀名
            var suf = Path.GetExtension(form.FileName);
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
                using (var fs =System.IO.File.Create(physicsPath))
                {
                    await form.CopyToAsync(fs);
                    //删除之前的图片释放资源
                    if (!string.IsNullOrEmpty(user.Avatar))
                    {
                        var oldAvatar = basePath + user.Avatar;
                        if (System.IO.File.Exists(oldAvatar))
                        {
                            System.IO.File.Delete(oldAvatar);
                        }
                    }
                    user.Avatar = vitualPath;
                    await _userService.UpdateAsync(user);
                }
            }
            return AjaxHelper.Seed(Ajax.Ok);
        }
        #endregion
    }
}
