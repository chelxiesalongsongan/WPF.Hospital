using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public interface IHistoryRepository : IRepository<History>
    {
        List<History> GetAll();
        List<History> GetByPatientId(int patientId);
        History? Get(int id);
        void Add(History entity);
        void Update(History entity);
        void Delete(int id);
        int Save();
        
    }
}
