using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class Department:BaseEntity,IEntity
    {
        public Department()
        {
            Employees=new HashSet<Employee>();  
        }
        /// <summary>
        /// Şöbə adı
        /// </summary>
        public string Name { get; set; }    
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
