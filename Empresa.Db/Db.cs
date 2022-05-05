using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Db
{
    public static class Db
    {
        public static string Conexao
        {
            get
            {
                //return @"Data Source=DESKTOP-T4EL1IC;Initial Catalog=EmpresaDb;Integrated Security=True;";
                return @"Data Source=192.168.1.71;Initial Catalog=EmpresaDb;User ID=disbbyweb;Password=disbbyweb;";

            }
        }
    }
}
