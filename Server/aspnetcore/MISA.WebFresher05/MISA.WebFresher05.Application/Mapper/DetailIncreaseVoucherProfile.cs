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
    /// Maping cho chi tiết chứng từ ghi tăng
    /// </summary>
    /// Created by: vtahoang (23/08/2023) 
    public class DetailIncreaseVoucherProfile : Profile
    {
        public DetailIncreaseVoucherProfile()
        {
            CreateMap<FixedAssetUpdateDto, DetailIncreaseVoucher>();
            CreateMap<DetailIncreaseVoucher, DetailIncreaseVoucherDto>();
            CreateMap<DetailIncreaseVoucherCreateDto, DetailIncreaseVoucher>();
            CreateMap<DetailIncreaseVoucherUpdateDto, DetailIncreaseVoucher>();
        }
    }
}
