using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class Employee:BaseEntity,IEntity
    {
        /// <summary>
        /// İşçinin adı
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// İşçinin soyadı
        /// </summary>
        public string Surname { get; set; } 
        /// <summary>
        /// İşçinin doğum tarixi
        /// </summary>
        public DateTime BirthDate { get; set; } 
        public int DepartmenId { get; set; }
        public virtual Department Department { get; set; }
    }
}
