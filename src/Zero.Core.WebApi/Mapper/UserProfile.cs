﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.WebApi.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
    public class UserDto : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 加密盐
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// 是否被锁定
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

    }
}
