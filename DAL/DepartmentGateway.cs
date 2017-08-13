using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public class DepartmentGateway
    {
        private DMSDbEntities db;

        public DepartmentGateway()
        {
            db=new DMSDbEntities();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return db.Departments;
        }
    }
}
