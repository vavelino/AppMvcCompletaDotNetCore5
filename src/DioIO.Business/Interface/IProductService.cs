﻿using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioIO.Business.Interface
{
    interface IProductService
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id);
    }
}