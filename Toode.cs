using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Andmebas;Integrated Security=True

namespace Andmebaas
{
    public class Toode
    {
        public int ID {  get; set; }
        public string Nimetus { get; set; }
        public int Kogus { get; set; }
        public float Hind { get; set; }
    }
}
