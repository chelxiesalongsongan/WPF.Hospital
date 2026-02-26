using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly Repository.IHistoryRepository _historyRepository;
        private readonly IPatientRepository _patientRepository;

        public HistoryService(Repository.IHistoryRepository historyRepository, IPatientRepository patientRepository)
        {
            _historyRepository = historyRepository;
            _patientRepository = patientRepository;
        }

        public void Add(Model.History history)
        {
            var patient = _patientRepository.Get(history.PatientId);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }
            _historyRepository.Add(history);
            _historyRepository.Save();


        }

        public void Delete(int id)
        {
            _historyRepository.Delete(id);
            _historyRepository.Save();
        }

        public void Update(Model.History history)
        {
            var existingHistory = _historyRepository.Get(history.Id);
            if (existingHistory == null)
            {
                throw new Exception("History not found");
            }
            existingHistory.Procedure = history.Procedure;
            existingHistory.PatientId = history.PatientId;
            _historyRepository.Update(existingHistory);
            _historyRepository.Save();
        }

        public Model.History? Get(int id)
        {
            return _historyRepository.Get(id);
        }

        public IEnumerable<Model.History> GetAll()
        {
            return _historyRepository.GetAll();
        }

        public IEnumerable<Model.History> GetByPatientId(int patientId)
        {
            return _historyRepository.GetByPatientId(patientId);
        }

        List<History> IHistoryService.GetAll()
        {
            throw new NotImplementedException();
        }

        public List<History> GetByPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        History? IHistoryService.Get(int id)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Create(History history)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Update(History history)
        {
            throw new NotImplementedException();
        }

        (bool Ok, string Message) IHistoryService.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
