using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IHistoryRepository _historyRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository, IHistoryRepository historyRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _historyRepository = historyRepository;
        }

        public void Add(Model.Prescription prescription)
        {
                _prescriptionRepository.Add(prescription);
                _prescriptionRepository.Save();
        }

        public void Delete(int id)
        {
            _prescriptionRepository.Delete(id);
            _prescriptionRepository.Save();
        }

        public Model.Prescription? Get(int id)
        {
            return _prescriptionRepository.Get(id);
        }

        public IEnumerable<Model.Prescription> GetAll()
        {
            return _prescriptionRepository.GetAll();
        }

        public List<Prescription> GetByHistory(int historyId)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Prescription prescription)
        {
            _prescriptionRepository.Update(prescription);
            _prescriptionRepository.Save();
        }

        (bool Ok, string Message) IPrescriptionService.Create(Model.Prescription prescription)
        {
            try
            {
                Add(prescription);
                return (true, "Prescription created successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error creating prescription: {ex.Message}");
            }
        }

        (bool Ok, string Message) IPrescriptionService.Delete(int id)
        {
            try
            {
                Delete(id);
                return (true, "Prescription deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting prescription: {ex.Message}");
            }
        }

        List<Prescription> IPrescriptionService.GetAll()
        {
            return _prescriptionRepository.GetAll();
        }

        (bool Ok, string Message) IPrescriptionService.Update(Prescription prescription)
        {
            try
            {
                Update(prescription);
                return (true, "Prescription updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating prescription: {ex.Message}");
            }
        }
    }
}
