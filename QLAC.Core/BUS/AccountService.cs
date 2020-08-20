using DAO_QLAC;
using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC.BUS
{
    public class AccountService : IAccountService
    {
        AccountDAO dao = new AccountDAO();
        public void CreateNew(Account T)
        {
            dao.CreateNewDAO(T);
        }

        public void CreateRole(int T, int E)
        {
            dao.CreateRoleDAO(T, E);
        }

        public void Delete(int ID)
        {
            dao.DeleteDAO(ID);
        }

        public Account GetAccountLogin(string username)
        {
            return GetAccountLogin(username);
        }

        public Account Getbykey(int id)
        {
            return dao.GetbykeyDAO(id);
        }

        public Account GetByUsername(string username)
        {
            return dao.GetByUsernameDAO(username);
        }

        public List<Account> GetListAccount()
        {
            return dao.GetListAccountDAO();
        }

        public List<Account> GetNamebyOfficeID(int office)
        {
            return dao.GetNamebyOfficeIDDAO(office);
        }

        public string GetNameByUserID(int userid)
        {
            return dao.GetNameByUserIDDAO(userid);
        }

        public int GetOfficeIDByUserID(int id)
        {
            return dao.GetOfficeIDByUserIDDAO(id);
        }

        public int GetOfficeIDNameByUsername(string username)
        {
            return dao.GetOfficeIDNameByUsernameDAO(username);
        }

        public string GetOfficeNameByOfficeID(int OfficeID)
        {
            return dao.GetOfficeNameByOfficeIDDAO(OfficeID);
        }

        public int GetShiftIDByShiftName(string name)
        {
            return dao.GetShiftIDByShiftNameDAO(name);
        }

        public int GetUserIDByName(string name)
        {
            return dao.GetUserIDByNameDAO(name);
        }

        public int GetUserIDByUsername(string username)
        {
            return dao.GetUserIDByUsernameDAO(username);
        }

        public void Update(Account account)
        {
            dao.UpdateDAO(account);
        }

        public void UpdatePassword(Account account)
        {
            dao.UpdatePasswordDAO(account);
        }
        public string GetNameByUserName(string username)
        {
            return dao.GetNameByUserNameDAO(username);
        }
        public void ResetPassword(Account account)
        {
            string dfpass = "12345";
            account.Password = dfpass;
            dao.UpdatePasswordDAO(account);
        }
    }
}
