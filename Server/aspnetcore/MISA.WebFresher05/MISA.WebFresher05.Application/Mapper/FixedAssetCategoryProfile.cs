using AutoMapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application.Mapper
{
    /// <summary>
    /// Mapping cho loại tài sản
    /// </summary>
    /// Created by: vtahoang (20/07/2023) 
    public class FixedAssetCategoryProfile : Profile
    {
        public FixedAssetCategoryProfile()
        {
            CreateMap<FixedAssetCategory, FixedAssetCategoryDto>();
            CreateMap<FixedAssetCategoryCreateDto, FixedAssetCategory>();
            CreateMap<FixedAssetCategoryUpdateDto, FixedAssetCategory>();
        }
    }
}
