using AutoMapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Mapping cho Tài sản
    /// </summary>
    /// Created by: vtahoang (20/07/2023) 
    public class FixedAssetProfile : Profile
    {
        public FixedAssetProfile()
        {
            CreateMap<FixedAsset, FixedAssetDto>();
            CreateMap<FixedAssetCreateDto, FixedAsset>();
            CreateMap<FixedAssetUpdateDto, FixedAsset>();
        }
    }
}
