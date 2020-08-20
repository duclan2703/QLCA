using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC
{
    public interface IOfficeService
    {
        void CreateNew(Office office);
        void Delete(int ID);
        List<Office> GetAll();
        Office Getbykey(int id);
        void Update(Office office);
        List<Office> GetOfficeName();
    }
}
