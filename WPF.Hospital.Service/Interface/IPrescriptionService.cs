using System.Collections.Generic;
using WPF.Hospital.Model;

namespace WPF.Hospital.Service
{
    public interface IPrescriptionService
    {
        List<Prescription> GetAll();
        List<Prescription> GetByHistory(int historyId);
        Prescription? Get(int id);

        (bool Ok, string Message) Create(Prescription prescription);
        (bool Ok, string Message) Update(Prescription prescription);
        (bool Ok, string Message) Delete(int id);
    }
}
