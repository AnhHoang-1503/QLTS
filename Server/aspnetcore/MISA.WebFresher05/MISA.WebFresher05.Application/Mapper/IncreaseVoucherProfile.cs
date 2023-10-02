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
    /// Maping cho chứng từ ghi tăng
    /// </summary>
    /// Created by: vtahoang (23/08/2023) 
    public class IncreaseVoucherProfile : Profile
    {
        public IncreaseVoucherProfile()
        {
            CreateMap<IncreaseVoucher, IncreaseVoucherDto>();
            CreateMap<IncreaseVoucherCreateDto, IncreaseVoucher>();
            CreateMap<IncreaseVoucherUpdateDto, IncreaseVoucher>();
        }
    }
}
