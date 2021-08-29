using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDemo.Models
{
    public class EmployeeService
    {
        private static List<Employee> ObjEmployeesList;

        public EmployeeService()
        {
            ObjEmployeesList = new List<Employee>()
            {
                new Employee { Id = 101, Name = "Walid", Age = 22 }
            };

        }

        public List<Employee> GetAll()
        {
            return ObjEmployeesList;
        }

        public bool Add(Employee ObjNewEmployee)
        {
            if (ObjNewEmployee.Age < 21 || ObjNewEmployee.Age > 58)
                throw new ArgumentException("Invalid Age limit for employee");


            ObjEmployeesList.Add(ObjNewEmployee);
            return true;
        }

        public bool Update (Employee ObjEmployeeToUpdate)
        {
            bool isUpdated = false;
            for (int index = 0; index < ObjEmployeesList.Count; index++)
            {
                if (ObjEmployeesList[index].Id == ObjEmployeeToUpdate.Id)
                {
                    ObjEmployeesList[index].Name = ObjEmployeeToUpdate.Name;
                    ObjEmployeesList[index].Age = ObjEmployeeToUpdate.Age;
                    isUpdated = true;
                    break;
                }
            }
            return isUpdated;
        }

        public bool Delete (int id)
        {
            bool isDeleted = false;

            for (int index = 0; index < ObjEmployeesList.Count; index++)
            {
                if(ObjEmployeesList[index].Id == id)
                {
                    ObjEmployeesList.RemoveAt(index);
                    isDeleted = true;
                    break;
                }
            }


            return isDeleted;
        }

        public Employee Search(int id)
        {
            return ObjEmployeesList.FirstOrDefault(e => e.Id == id);

        }
    }
}
