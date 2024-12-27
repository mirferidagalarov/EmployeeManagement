using Entities.Concrete.DTOs.EmployeeDTOs;
using Entities.Concrete.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ViewModel
{
    public class EmployeeGetAllPageViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<EmployeeListDTO> Employees { get; set; }
    }
}
