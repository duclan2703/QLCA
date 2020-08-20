using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC.BUS
{
    public class OfficeService : IOfficeService
    {
        OfficeDAO dao = new OfficeDAO();

        public void CreateNew(Office office)
        {
            dao.CreateNewDAO(office);
        }

        public void Delete(int ID)
        {
            dao.DeleteDAO(ID);
        }

        public List<Office> GetAll()
        {
            return dao.GetAllDAO();
        }

        public Office Getbykey(int id)
        {
            return dao.GetbykeyDAO(id);
        }

        public List<Office> GetOfficeName()
        {
            return dao.GetOfficeNameDAO();
        }

        public void Update(Office office)
        {
            dao.UpdateDAO(office);
        }
    }
}
