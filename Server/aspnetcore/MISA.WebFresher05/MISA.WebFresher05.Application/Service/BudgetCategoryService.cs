using AutoMapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    public class BudgetCategoryService : BaseReadOnlyService<BudgetCategory, BudgetCategoryDto>, IBudgetCategoryService
    {
        public BudgetCategoryService(IBudgetCategoryRepository budgetCategoryRepository, IMapper mapper) : base(budgetCategoryRepository, mapper)
        {
        }
    }
}
