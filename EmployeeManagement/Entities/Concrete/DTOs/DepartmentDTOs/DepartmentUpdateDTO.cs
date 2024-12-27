using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.DepartmentDTOs
{
    public class DepartmentUpdateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}
