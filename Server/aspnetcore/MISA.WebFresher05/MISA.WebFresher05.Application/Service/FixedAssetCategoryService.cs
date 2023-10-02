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
    /// Service của loại tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    public class FixedAssetCategoryService : BaseReadOnlyService<FixedAssetCategory, FixedAssetCategoryDto>, IFixedAssetCategoryService
    {
        public FixedAssetCategoryService(IFixedAssetCategoryRepository fixedAssetCategoryRepositoryRepository, IMapper mapper) : base(fixedAssetCategoryRepositoryRepository, mapper)
        {
        }
    }
}
