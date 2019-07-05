using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMApiPlayground
{
    public class MaterialTableCol
    {
        public string title { get; set; }
        public string field { get; set; }

        public MaterialTableCol()
        {

        }
        public MaterialTableCol(string title, string field)
        {
            this.title = title;
            this.field = field;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            List<MaterialTableCol> hCMApiPlaygrounds = new List<MaterialTableCol>();
            hCMApiPlaygrounds.Add(new MaterialTableCol("Id", "Id"));
            hCMApiPlaygrounds.Add(new MaterialTableCol("Name", "name"));

            var json = JsonConvert.SerializeObject(hCMApiPlaygrounds);

            Console.WriteLine(json);

            Console.ReadKey();
        }
    }
}
