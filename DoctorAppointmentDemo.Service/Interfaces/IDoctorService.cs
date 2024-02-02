using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Service.Interfaces
{
    public interface IDoctorService
    {
        Doctor Create(Doctor doctor, string format);

        IEnumerable<Doctor> GetAll(string format);

        Doctor? Get(int id);

        bool Delete(int id);

        Doctor Update(int id, Doctor doctor);
    }
}
