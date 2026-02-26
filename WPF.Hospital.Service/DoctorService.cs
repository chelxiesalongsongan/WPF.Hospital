using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IHistoryService _historyService;
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository, IHistoryService historyService)
        {
            _doctorRepository = doctorRepository;
            _historyService = historyService;
        }

        public void Add(Model.Doctor doctor)
        {
            _doctorRepository.Add(doctor);
            _doctorRepository.Save();
        }

        public void Delete(int id)
        {
            _doctorRepository.Delete(id);
            _doctorRepository.Save();
        }

        public Model.Doctor? Get(int id)
        {
            return _doctorRepository.Get(id);
        }

        public IEnumerable<Model.Doctor> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public void Update(Model.Doctor doctor)
        {
            _doctorRepository.Update(doctor);
            _doctorRepository.Save();
        }

        (bool Ok, string Message) IDoctorService.Create(Model.Doctor doctor)
        {
            try
            {
                Add(doctor);
                return (true, "Doctor created successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error creating doctor: {ex.Message}");
            }
        }

        (bool Ok, string Message) IDoctorService.Delete(int id)
        {
            try
            {
                Delete(id);
                return (true, "Doctor deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting doctor: {ex.Message}");
            }
        }
        (bool Ok, string Message) IDoctorService.Update(Model.Doctor doctor)
        {
            try
            {
                Update(doctor);
                return (true, "Doctor updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating doctor: {ex.Message}");
            }
        }
        public (bool Ok, string Message) Create(Model.Doctor doctor)
        {
            try
            {
                Add(doctor);
                return (true, "Doctor created successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error creating doctor: {ex.Message}");
            }


        }

        List<Doctor> IDoctorService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
