using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC.BUS
{
    public class RolService : IRoleService
    {
        RolDAO dao = new RolDAO();
        public List<Rol> GetAll()
        {
            return dao.GetAllDAO();
        }

        public List<string> GetListRoleNameByListRoleId(List<int> ListRoleID)
        {
            return dao.GetListRoleNameByListRoleIdDAO(ListRoleID);
        }
    }
}
