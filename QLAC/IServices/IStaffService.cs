using QLAC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLAC.IServices
{
    public interface IStaffService : FX.Data.IBaseService<Staff, int>
    {
        //IList<Staff> Getbygender(string name);
    }
}