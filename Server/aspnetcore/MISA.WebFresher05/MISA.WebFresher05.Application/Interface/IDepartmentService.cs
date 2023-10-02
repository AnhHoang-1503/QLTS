﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Interface service của phòng ban
    /// </summary>
    /// Created by: vtahoang (20/07/2023)  
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}
