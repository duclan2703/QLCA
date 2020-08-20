using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC.BUS
{
    public class AccountRolService : IAccountRoleService
    {
        AccountRolDAO dao = new AccountRolDAO();
        public List<int> GetListRoleIDByUserID(int ID)
        {
            return dao.GetListRoleIDByUserIDDAO(ID);
        }
    }
}
