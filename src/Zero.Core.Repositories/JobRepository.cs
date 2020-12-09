﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IRepositories.Base;
using Zero.Core.Repositories.Basee;

namespace Zero.Core.Repositories
{
    public class JobRepository : Repository<Jobs>, IJobRepository
    {
        public JobRepository(IUnitOfWork unit) : base(unit)
        {
            
        }
    }
}
