using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_MF
{
    class Program
    {
        static void Main(string[] args)
        {
            /*EFModelContainer container = new EFModelContainer();
            container.Classes.Add(new Class()
            {
                ClassName = "Nursery"
            });
            container.SaveChanges();*/

            EFModelContainer container = new EFModelContainer();
            ICollection<Student> students = new List<Student> {
        new Student() {
            Name = "Mark"
        },
        new Student() {
            Name = "Joe"
        },
        new Student() {
            Name = "Allen"
        }
    };
            container.Classes.Add(new Class()
            {
                ClassName = "KG",
                Students = students
            });
            container.SaveChanges();
        }
    }
}
