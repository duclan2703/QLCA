using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC
{
    public interface IRoleService
    {
        List<string> GetListRoleNameByListRoleId(List<int> ListRoleID);
        List<Rol> GetAll();
    }
}
