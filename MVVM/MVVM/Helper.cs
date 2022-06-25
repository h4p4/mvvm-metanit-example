using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public static class Helper
    {
        public static PhonesDbContext db = new PhonesDbContext();
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PhonesDb";
    }
}
