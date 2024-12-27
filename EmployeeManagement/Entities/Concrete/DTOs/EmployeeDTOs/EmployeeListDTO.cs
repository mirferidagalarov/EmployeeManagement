using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.EmployeeDTOs
{
    public class EmployeeListDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string DepartmentName { get; set; }  
        public string CompanyName { get; set; } 
        public int DepartmenId { get; set; }
    }
}
