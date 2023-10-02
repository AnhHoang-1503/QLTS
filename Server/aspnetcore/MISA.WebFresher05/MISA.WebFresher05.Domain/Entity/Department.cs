using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Entity của phòng ban
    /// </summary>
    /// Created by: vtahoang (13/07/2023)
    public class Department : BaseAuditEntity, IHaskey, IHasCode
    {
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// Created by: vtahoang (13/07/2023)
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã code phòng ban 
        /// </summary>
        /// Created by: vtahoang (13/07/2023)
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: vtahoang (13/07/2023)
        public string? department_name { get; set; }

        /// <summary>
        /// Trả về mã tài sản
        /// </summary>
        /// <returns>Code</returns>
        /// Created by: vtahoang (15/08/2023) 
        public string GetCode()
        {
            return department_code ?? "";
        }

        /// <summary>
        /// Trả về id
        /// </summary>
        /// <returns>Id</returns>
        /// Created by: vtahoang (18/07/2023) 
        public Guid GetKey()
        {
            return department_id;
        }
    }
}
