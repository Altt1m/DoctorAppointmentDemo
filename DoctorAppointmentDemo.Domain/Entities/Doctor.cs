using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorAppointmentDemo.Domain.Enums;

namespace DoctorAppointmentDemo.Domain.Entities
{
    public class Doctor : UserBase
    {
        public DoctorTypes DoctorType { get; set; }
        public byte Experience { get; set; }
        public decimal Salary { get; set; }
    }
}
