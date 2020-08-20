using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC
{
    public interface IAccountService
    {
        void CreateNew(Account T);
        void CreateRole(int T, int E);
        void Delete(int ID);
        Account Getbykey(int id);
        void Update(Account account);
        void UpdatePassword(Account account);
        Account GetByUsername(string username);
        int GetUserIDByUsername(string username);
        string GetOfficeNameByOfficeID(int OfficeID);
        Account GetAccountLogin(string username);
        int GetOfficeIDNameByUsername(string username);
        int GetShiftIDByShiftName(string name);
        List<Account> GetNamebyOfficeID(int office);
        int GetUserIDByName(string name);
        int GetOfficeIDByUserID(int id);
        List<Account> GetListAccount();
        string GetNameByUserID(int userid);
        string GetNameByUserName(string username);
    }
}
