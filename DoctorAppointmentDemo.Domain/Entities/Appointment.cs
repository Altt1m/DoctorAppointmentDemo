using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentDemo.Domain.Entities
{
    public class Appointment : Auditable
    {
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string? Description { get; set; }
    }
}
