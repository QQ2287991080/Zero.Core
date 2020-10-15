using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.User
{
    public interface IUserProvider
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get;}

    }
}
