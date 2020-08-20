using DAO_QLAC.Interfaces;
using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC.BUS
{
    public class ShiftService : IShiftService
    {
        ShiftDAO dao = new ShiftDAO();
        public void CreateNew(Shift shift)
        {
            dao.CreateNewDAO(shift);
        }

        public void Delete(int ID)
        {
            dao.DeleteDAO(ID);
        }

        public List<Shift> GetAll()
        {
            return dao.GetAllDAO();
        }

        public Shift Getbykey(int id)
        {
            return dao.GetbykeyDAO(id);
        }

        public string GetShiftNamebyShiftID(int id)
        {
            return dao.GetShiftNamebyShiftIDDAO(id);
        }

        public void Update(Shift shift)
        {
            dao.UpdateDAO(shift);
        }
    }
}
