using System.Collections.Generic;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;

        public MedicineService(IMedicineRepository repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        public List<Medicine> GetAll() => _repository.GetAll();

        public Medicine? Get(int id) => _repository.Get(id);

        public (bool Ok, string Message) Create(Medicine medicine)
        {
            if (medicine == null) return (false, "Medicine is null.");
            try
            {
                _repository.Add(medicine);
                return (true, "Medicine created successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error creating medicine: {ex.Message}");
            }
        }

        public (bool Ok, string Message) Update(Medicine medicine)
        {
            if (medicine == null) return (false, "Medicine is null.");
            try
            {
                var existing = _repository.Get(medicine.Id);
                if (existing == null) return (false, "Medicine not found.");
                _repository.Update(medicine);
                return (true, "Medicine updated successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error updating medicine: {ex.Message}");
            }
        }

        public (bool Ok, string Message) Delete(int id)
        {
            try
            {
                var medicine = _repository.Get(id);
                if (medicine == null) return (false, "Medicine not found.");
                _repository.Delete(id);
                return (true, "Medicine deleted successfully.");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error deleting medicine: {ex.Message}");
            }
        }
    }
}