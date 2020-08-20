using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC
{
    public class DBConnect
    {
        protected SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-MSQB3CL; Initial Catalog=QLAC; User ID=sa;Password=1234567;Integrated Security=True");
    }
}
