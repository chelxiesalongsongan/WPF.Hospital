using System;
using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;
using DTO = WPF.Hospital.DTO;

namespace WPF.Hospital.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _repository;

        public HistoryService(IHistoryRepository repository)
        {
            _repository = repository;
        }

        // ---------------------- Model-based methods ----------------------
        public List<History> GetAllModel() => _repository.GetAll();

        public History? GetModel(int id) => _repository.Get(id);

        public List<History> GetByPatientModel(int patientId) => _repository.GetByPatientId(patientId);

        // ---------------------- DTO-based methods ----------------------
        public List<DTO.History> GetAll()
        {
            return _repository.GetAll()
                              .Select(h => new DTO.History
                              {
                                  Id = h.Id,
                                  PatientId = h.PatientId,
                                  DoctorId = h.DoctorId,
                                  Procedure = h.Procedure
                              })
                              .ToList();
        }

        public DTO.History? Get(int id)
        {
            var h = _repository.Get(id);
            if (h == null) return null;

            return new DTO.History
            {
                Id = h.Id,
                PatientId = h.PatientId,
                DoctorId = h.DoctorId,
                Procedure = h.Procedure
            };
        }

        public List<DTO.History> GetByPatient(int patientId)
        {
            return _repository.GetByPatientId(patientId)
                              .Select(h => new DTO.History
                              {
                                  Id = h.Id,
                                  PatientId = h.PatientId,
                                  DoctorId = h.DoctorId,
                                  Procedure = h.Procedure
                              })
                              .ToList();
        }

        public (bool Ok, string Message) Create(DTO.History dto)
        {
            if (dto == null) return (false, "History is null.");
            try
            {
                var model = new History
                {
                    PatientId = dto.PatientId,
                    DoctorId = dto.DoctorId,
                    Procedure = dto.Procedure
                };
                _repository.Add(model);
                return (true, "History created successfully.");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? "No inner exception";
                return (false, $"Error creating history: {ex.Message} | Inner: {inner}");
            }
        }

        public (bool Ok, string Message) Update(DTO.History dto)
        {
            if (dto == null) return (false, "History is null.");
            try
            {
                var existing = _repository.Get(dto.Id);
                if (existing == null) return (false, "History not found.");

                existing.PatientId = dto.PatientId;
                existing.DoctorId = dto.DoctorId;
                existing.Procedure = dto.Procedure;

                _repository.Update(existing);
                return (true, "History updated successfully.");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? "No inner exception";
                return (false, $"Error updating history: {ex.Message} | Inner: {inner}");
            }
        }

        public (bool Ok, string Message) Delete(int id)
        {
            try
            {
                var history = _repository.Get(id);
                if (history == null) return (false, "History not found.");

                _repository.Delete(id);
                return (true, "History deleted successfully.");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? "No inner exception";
                return (false, $"Error deleting history: {ex.Message} | Inner: {inner}");
            }
        }
    }
}
