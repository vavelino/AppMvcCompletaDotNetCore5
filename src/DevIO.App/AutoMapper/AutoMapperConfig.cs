using AutoMapper;
using DevIO.App.Models;
using DevIO.App.ViewModels;
using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.AutoMapper
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {            
            CreateMap<Supplier, SupplierViewModel>()
                .ReverseMap(); // Para ser de mão dupla
            CreateMap<Address, AddressViewModel>()
                .ReverseMap();
            CreateMap<Product, ProductViewModel>()
                .ReverseMap();
        }
    }
}
