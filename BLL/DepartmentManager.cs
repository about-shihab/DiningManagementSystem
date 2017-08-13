using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class DepartmentManager
    {
        private DepartmentGateway departmentGateway;

        public DepartmentManager()
        {
            departmentGateway=new DepartmentGateway();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }
    }
}
