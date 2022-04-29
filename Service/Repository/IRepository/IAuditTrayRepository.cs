﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository.IRepository
{
    public interface IAuditTrayRepository : IRepository<AuditTray>
    {
        void Update(AuditTray audittray);

    }
}
