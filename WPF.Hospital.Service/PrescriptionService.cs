using System.Collections.Generic;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _repository;

        public PrescriptionService(IPrescriptionRepository repository)
        {
            _repository = repository;
        }

  
        public List<Prescription> GetAll()
        {
            return _repository.GetAll();
        }

        public List<Prescription> GetByHistory(int historyId)
        {
            return _repository.GetByHistory(historyId);
        }


        public Prescription? Get(int id)
        {
            return _repository.Get(id);
        }


        public (bool Ok, string Message) Create(Prescription prescription)
        {
            if (prescription == null)
                return (false, "Prescription cannot be null.");

            try
            {
                _repository.Add(prescription);
                return (true, "Prescription created successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error creating prescription: {ex.Message}");
            }
        }


        public (bool Ok, string Message) Update(Prescription prescription)
        {
            if (prescription == null)
                return (false, "Prescription cannot be null.");

            try
            {
                var existing = _repository.Get(prescription.Id);
                if (existing == null)
                    return (false, "Prescription not found.");

                _repository.Update(prescription);
                return (true, "Prescription updated successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error updating prescription: {ex.Message}");
            }
        }


        public (bool Ok, string Message) Delete(int id)
        {
            try
            {
                var prescription = _repository.Get(id);
                if (prescription == null)
                    return (false, "Prescription not found.");

                _repository.Delete(id);
                return (true, "Prescription deleted successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error deleting prescription: {ex.Message}");
            }
        }
    }
}