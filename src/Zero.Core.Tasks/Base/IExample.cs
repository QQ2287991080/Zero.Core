using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Tasks.Base
{
    /// <summary>
    /// 抽象处理任务类
    /// </summary>
    public interface IExample
    {
        Task Run();
    }
}
