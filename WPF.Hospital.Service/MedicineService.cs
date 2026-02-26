using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IHistoryRepository _historyRepository;

        public MedicineService(IMedicineRepository medicineRepository, IHistoryRepository historyRepository)
        {
            _medicineRepository = medicineRepository;
            _historyRepository = historyRepository;
        }

        public void Add(Model.Medicine medicine)
        {
            _medicineRepository.Add(medicine);
            _medicineRepository.Save();
        }

        public void Delete(int id)
        {
            _medicineRepository.Delete(id);
            _medicineRepository.Save();
        }

        public Model.Medicine? Get(int id)
        {
            return _medicineRepository.Get(id);
        }

        public IEnumerable<Model.Medicine> GetAll()
        {
            return _medicineRepository.GetAll();
        }

        public void Update(Model.Medicine medicine)
        {
            _medicineRepository.Update(medicine);
            _medicineRepository.Save();
        }

        (bool Ok, string Message) IMedicineService.Create(Model.Medicine medicine)
        {
            try
            {
                Add(medicine);
                return (true, "Medicine created successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error creating medicine: {ex.Message}");
            }
        }

        (bool Ok, string Message) IMedicineService.Delete(int id)
        {
            try
            {
                Delete(id);
                return (true, "Medicine deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting medicine: {ex.Message}");
            }

        }

        List<Medicine> IMedicineService.GetAll()
        {
            return GetAll().ToList();
        }

        (bool Ok, string Message) IMedicineService.Update(Medicine medicine)
        {
            try
            {
                Update(medicine);
                return (true, "Medicine updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating medicine: {ex.Message}");
            }
        }
    }
}

