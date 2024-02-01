using DoctorAppointmentDemo.Service.Interfaces;
using DoctorAppointmentDemo.Service.Services;
using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.UI
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;
        private string format;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }

        public void Menu()
        {
            while (true)
            {
                string? respond;
                Console.WriteLine("Choose your save format:\n" +
                    "1 - XML\n" +
                    "2 - JSON\n" +
                    "Type \"0\" to quit.");
                respond = Console.ReadLine();

                if (respond != null)
                {
                    switch (respond)
                    {
                        case "1":
                            format = "xml";
                            Console.Clear();
                            Console.WriteLine("XML save format has been chosen.\n");
                            break;
                        case "2":
                            format = "json";
                            Console.Clear();
                            Console.WriteLine("JSON save format has been chosen.\n");
                            break;
                        case "0":
                            return;
                        default:
                            Console.Clear();
                            continue;
                    }
                }
                else
                {
                    Console.Clear();
                    continue;
                }

                Console.WriteLine("Current doctors list: ");
                var docs = _doctorService.GetAll();

                foreach (var doc in docs)
                {
                    Console.WriteLine(doc.Name);
                }

                Console.WriteLine("Adding doctor...");

                var newDoctor = new Doctor
                {
                    Name = "Vasya",
                    Surname = "Petrov",
                    Experience = 20,
                    DoctorType = Domain.Enums.DoctorTypes.Dentist
                };

                _doctorService.Create(newDoctor, format);

                //Console.WriteLine("Updated doctors list: ");
                //docs = _doctorService.GetAll();
                //foreach (var doc in docs)
                //{
                //    Console.WriteLine(doc.Name);
                //}

                break;
            }

            
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}