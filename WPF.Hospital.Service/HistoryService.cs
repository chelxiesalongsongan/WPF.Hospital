using System.Collections.Generic;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _repository;

        public HistoryService(IHistoryRepository repository)
        {
            _repository = repository;
        }


        public List<History> GetAll()
        {
            return _repository.GetAll();
        }

        public List<History> GetByPatient(int patientId)
        {
            return _repository.GetByPatientId(patientId);
        }

        public History? Get(int id)
        {
            return _repository.Get(id);
        }

        public (bool Ok, string Message) Create(History history)
        {
            if (history == null)
                return (false, "History is null.");

            try
            {
                _repository.Add(history);
                return (true, "History created successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error creating history: {ex.Message}");
            }
        }


        public (bool Ok, string Message) Update(History history)
        {
            if (history == null)
                return (false, "History is null.");

            try
            {
                var existing = _repository.Get(history.Id);
                if (existing == null)
                    return (false, "History not found.");

                _repository.Update(history);
                return (true, "History updated successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error updating history: {ex.Message}");
            }
        }

        public (bool Ok, string Message) Delete(int id)
        {
            try
            {
                var history = _repository.Get(id);
                if (history == null)
                    return (false, "History not found.");

                _repository.Delete(id);
                return (true, "History deleted successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error deleting history: {ex.Message}");
            }
        }

        List<DTO.History> IHistoryService.GetAll()
        {
            throw new NotImplementedException();
        }

        List<DTO.History> IHistoryService.GetByPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        DTO.History? IHistoryService.Get(int id)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Create(DTO.History history)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Update(DTO.History history)
        {
            throw new NotImplementedException();
        }
    }
}