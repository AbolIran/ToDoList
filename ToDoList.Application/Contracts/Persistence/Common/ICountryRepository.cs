﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Contracts.Persistence.Common
{
    public interface ICountryRepository : IGenericRepository<Domain.Common.Country>
    {
    }
}
