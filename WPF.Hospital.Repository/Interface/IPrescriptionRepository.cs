using System.Collections.Generic;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public interface IPrescriptionRepository
    {
        List<Prescription> GetAll();
        List<Prescription> GetByHistory(int historyId);
        Prescription? Get(int id);
        void Add(Prescription prescription);
        void Update(Prescription prescription);
        void Delete(int id);
    }
}
