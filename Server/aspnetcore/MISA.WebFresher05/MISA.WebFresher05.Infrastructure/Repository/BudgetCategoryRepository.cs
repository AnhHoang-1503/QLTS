using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    public class BudgetCategoryRepository : BaseReadOnlyRepository<BudgetCategory>, IBudgetCategoryRepository
    {
        /// <summary>
        /// Tên bảng
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public override string TableName { get; protected set; } = "budget_category";

        public BudgetCategoryRepository(IUnitOfWork unitOfWork, IFilterObjectHandler filterObjectHandler) : base(unitOfWork, filterObjectHandler)
        {
        }
    }
}
