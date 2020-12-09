using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Domain.Dtos.Jobs
{
    public class JobsDto
    {
        
    }

    public class JobCondition:PageCondition
    {
        /// <summary>
        /// Job名称
        /// </summary>
        public string Name { get; set; }
    }
}
