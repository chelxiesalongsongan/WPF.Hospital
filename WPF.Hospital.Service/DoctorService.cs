using System.Collections.Generic;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public List<Doctor> GetAll() => _repository.GetAll();

        public Doctor? Get(int id) => _repository.Get(id);

        public (bool Ok, string Message) Create(Doctor doctor)
        {
            if (doctor == null)
                return (false, "Doctor is null.");

            try
            {
                _repository.Add(doctor);
                return (true, "Doctor created successfully.");
            }
            catch (System.Exception ex)
            {
                var inner = ex.InnerException?.Message ?? "No inner exception";
                return (false, $"Error creating doctor: {ex.Message} | Inner: {inner}");
            }
        }

        public (bool Ok, string Message) Update(Doctor doctor)
        {
            if (doctor == null)
                return (false, "Doctor is null.");

            try
            {
                var existing = _repository.Get(doctor.Id);
                if (existing == null)
                    return (false, "Doctor not found.");

                _repository.Update(doctor);
                return (true, "Doctor updated successfully.");
            }
            catch (System.Exception ex)
            {
                var inner = ex.InnerException?.Message ?? "No inner exception";
                return (false, $"Error updating doctor: {ex.Message} | Inner: {inner}");
            }
        }

        public (bool Ok, string Message) Delete(int id)
        {
            try
            {
                var doctor = _repository.Get(id);
                if (doctor == null)
                    return (false, "Doctor not found.");

                _repository.Delete(id);
                return (true, "Doctor deleted successfully.");
            }
            catch (System.Exception ex)
            {
                var inner = ex.InnerException?.Message ?? "No inner exception";
                return (false, $"Error deleting doctor: {ex.Message} | Inner: {inner}");
            }
        }
    }
}
