using Core.Entities;

namespace Entities.Concrete.TableModels
{
    public class Company:BaseEntity,IEntity
    {
        public Company()
        {
            Departments = new HashSet<Department>();
        }
        /// <summary>
        /// Şirkət adı
        /// </summary>
        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
