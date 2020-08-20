using FX.Data;
using QLAC.Domain;
using QLAC.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLAC.Services
{
    public class StaffService : BaseService<Staff, int>,IStaffService
    {
        public StaffService(string sessionFactoryConfigPath, string connectionString = null)
            : base(sessionFactoryConfigPath, connectionString)
        { }

        //public IList<Staff> Getbygender(string name)
        //{
        //    return Query.Where(x => x.Name == name).ToList();
        //}
    }
}