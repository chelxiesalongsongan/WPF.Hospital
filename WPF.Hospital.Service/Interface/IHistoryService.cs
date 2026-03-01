using System.Collections.Generic;
using WPF.Hospital.Model;
using DTO = WPF.Hospital.DTO;

namespace WPF.Hospital.Service
{
    public interface IHistoryService
    {
        // Model-based
        List<History> GetAllModel();
        History? GetModel(int id);
        List<History> GetByPatientModel(int patientId);

        // DTO-based
        List<DTO.History> GetAll();
        DTO.History? Get(int id);
        List<DTO.History> GetByPatient(int patientId);

        (bool Ok, string Message) Create(DTO.History dto);
        (bool Ok, string Message) Update(DTO.History dto);
        (bool Ok, string Message) Delete(int id);
    }
}
