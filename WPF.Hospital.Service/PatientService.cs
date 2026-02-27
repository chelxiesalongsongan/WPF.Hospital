using System;
using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.DTO;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }


        public List<Patient> GetAll()
        {

            return _repository.GetAll()
                              .Select(p => new Patient
                              {
                                  Id = p.Id,
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,
                                  Age = p.Age
                              })
                              .ToList();
        }


        public Patient? Get(int id)
        {
            var entity = _repository.Get(id);
            if (entity == null) return null;

            return new Patient
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age
            };
        }


        public (bool Ok, string Message) Create(Patient patientDto)
        {
            if (patientDto == null)
                return (false, "Patient cannot be null.");

            try
            {
                var entity = new Patients
                {
                    FirstName = patientDto.FirstName,
                    LastName = patientDto.LastName,
                    Age = patientDto.Age
                };

                _repository.Add(entity);
                return (true, "Patient created successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error creating patient: {ex.Message}");
            }
        }


        public (bool Ok, string Message) Update(Patient patientDto)
        {
            if (patientDto == null)
                return (false, "Patient cannot be null.");

            try
            {
                var entity = _repository.Get(patientDto.Id);
                if (entity == null)
                    return (false, "Patient not found.");


                entity.FirstName = patientDto.FirstName;
                entity.LastName = patientDto.LastName;
                entity.Age = patientDto.Age;

                _repository.Update(entity);
                return (true, "Patient updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating patient: {ex.Message}");
            }
        }

        public (bool Ok, string Message) Delete(int id)
        {
            try
            {
                var entity = _repository.Get(id);
                if (entity == null)
                    return (false, "Patient not found.");

                _repository.Delete(id);
                return (true, "Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting patient: {ex.Message}");
            }
        }


        public void Add(Patient patientDto)
        {
            if (patientDto == null) return;

            var entity = new Patients
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Age = patientDto.Age
            };

            _repository.Add(entity);
        }
    }
}