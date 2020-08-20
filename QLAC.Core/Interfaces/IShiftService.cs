using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC.Interfaces
{
    public interface IShiftService
    {
        void CreateNew(Shift shift);
        void Delete(int ID);
        List<Shift> GetAll();
        Shift Getbykey(int id);
        void Update(Shift shift);
        string GetShiftNamebyShiftID(int id);
    }
}
