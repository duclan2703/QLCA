using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLAC.Domain;

namespace QLAC.IServices
{
    public interface IShiftService /*: FX.Data.IBaseService<Shift, int>*/
    {
        Shift Getbykey(int id);
        
        void CreateNew(Shift shift);
        void Update(Shift shift);
        void Delete(int ID);
        List<Shift> GetAll();

    }
}