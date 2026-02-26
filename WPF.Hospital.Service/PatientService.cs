using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IHistoryRepository _historyRepository;

        public PatientService(IPatientRepository patientRepository, IHistoryRepository historyRepository)
        {
            _patientRepository = patientRepository;
            _historyRepository = historyRepository;
        }

        public Patient Get(int id)
        {
            Model.Patients patient = _patientRepository.Get(id);

            return new Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Birthdate = patient.Birthdate,
                History = _historyRepository.GetByPatientId(id).Select(h => new History
                {
                    Id = h.Id,
                    Procedure = h.Procedure
                }).ToList()
            };
        }

        public IEnumerable<Patient> GetAll()
        {             var patients = _patientRepository.GetAll();
            return patients.Select(patient => new Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Birthdate = patient.Birthdate,
                History = _historyRepository.GetByPatientId(patient.Id).Select(h => new History
                {
                    Id = h.Id,
                    Procedure = h.Procedure
                }).ToList()
            }).ToList();
        }

        public void Add (Patient patient)
        {
            _patientRepository.Add(new Model.Patients
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Birthdate = patient.Birthdate,

            });

            _patientRepository.Save();
        }

        public void Delete (int id)
        {
            _patientRepository.Delete(id);
            _patientRepository.Save();
        }

        List<Patient> IPatientService.GetAll()
        {
            return _patientRepository.GetAll().Select(patient => new Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Birthdate = patient.Birthdate,
                History = _historyRepository.GetByPatientId(patient.Id).Select(h => new History
                {
                    Id = h.Id,
                    Procedure = h.Procedure
                }).ToList()
            }).ToList();
        }

        public (bool Ok, string Message) Create(Patient patient)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Update(Patient patient)
        {
            throw new NotImplementedException();
        }

        (bool Ok, string Message) IPatientService.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
