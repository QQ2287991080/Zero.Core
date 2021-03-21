using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Domain.Dtos
{
    public class PhotoManagerDto
    {
     
    }


    public class PhotoManagerCondition : PageCondition
    {

        public PhotoManagerCondition()
        {
            this.IsPage = true;
        }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPage { get; set; }


        public string Path { get; set; }

    }
}
