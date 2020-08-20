using ProtoQLCA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtoQLCA.IService
{
    public interface IAdminService /*: FX.Data.IBaseService<Admin, int>*/
    {
        //Admin GetByUsername(string username);
        Admin Getbykey(int id);
        void CreateNew(Admin admin);
        void Update(Admin admin);
        void Delete(int ID);
        List<Admin> GetAll();
    }
}