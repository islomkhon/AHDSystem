using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DBF
{
    class Program
    {
        static void Main(string[] args)
        {
            /*StudentDBEntities container = new StudentDBEntities();
            ICollection<Students> students = new List<Students> {
                new Students() {
                    Name = "Harry"
                },
                new Students() {
                    Name = "Jane"
                },
                new Students() {
                    Name = "Nick"
                }
            };
            container.Classes.Add(new Classes()
            {
                ClassName = "Class 1",
                Students = students
            });
            container.SaveChanges();
            container.Students.Add(new Students()
            {
                Class_Id = 1,
                Name = "Ben"
            });
            container.SaveChanges();*/

            StudentDBEntities container = new StudentDBEntities();
            var classData = container.Classes;
            foreach (var item in classData)
            {

            }
        }
    }
}
