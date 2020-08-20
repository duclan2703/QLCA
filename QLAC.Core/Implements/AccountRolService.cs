using DAO_QLAC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service_QLAC
{
    public class AccountRolService : DBConnect, IAccountRoleService
    {
        public List<int> GetListRoleIDByUsername(string username)
        {
            return GetListRoleIDByUsernameDAO(string username);
        }

        public List<int> GetListRoleIDByUserID(int ID)
    }
}