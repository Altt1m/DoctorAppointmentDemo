using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Domain.Enums;
using DoctorAppointmentDemo.Data.Configuration;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DoctorAppointmentDemo.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public override string JsonPath { get; set; }
        public override string XmlPath { get; set; }
        public override int LastId { get; set; }

        public DoctorRepository()
        {
            dynamic result = ReadFromAppSettings();

            JsonPath = result.Database.Doctors.JsonPath;
            XmlPath = result.Database.Doctors.XmlPath;
            LastId = result.Database.Doctors.LastId;
        }

        public override void CreateAsXml(Doctor source)
        {
            //XDocument xdoc = XDocument.Load(XmlPath);
            //XElement? root = xdoc.Element("doctors");

            //if (root != null)
            //{
            //    root.Add(new XElement("doctor",
            //                new XAttribute("Id", source.Id),
            //                new XElement("DoctorType", source.DoctorType),
            //                new XElement("Experience", source.Experience),
            //                new XElement("Salary", source.Salary),
            //                new XElement("Name", source.Name),
            //                new XElement("Surname", source.Surname),
            //                new XElement("Phone", source.Phone),
            //                new XElement("Email", source.Email),
            //                new XElement("CreatedAt", source.CreatedAt),
            //                new XElement("UpdatedAt", source.UpdatedAt)));
            //    xdoc.Save(XmlPath);
            //}

            List<Doctor> doctors;

            try
            {
                doctors = GetAllFromXml().ToList();
            }
            catch
            {
                doctors = new List<Doctor>();
            }

            doctors.Add(source);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Doctor>));
            using (TextWriter writer = new StreamWriter(XmlPath))
            {
                serializer.Serialize(writer, doctors);
            }

        }

        public override IEnumerable<Doctor> GetAllFromXml()
        {
            if (!File.Exists(XmlPath))
            {
                File.WriteAllText(XmlPath, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<ArrayOfDoctor xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n" +
                    "</ArrayOfDoctor>");
            }

            var xml = File.ReadAllText(XmlPath);
            if (string.IsNullOrWhiteSpace(xml))
                File.WriteAllText(XmlPath, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<ArrayOfDoctor xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n" +
                    "</ArrayOfDoctor>");

            var serializer = new XmlSerializer(typeof(List<Doctor>));
            using (var reader = new StreamReader(XmlPath))
            {
                var doctors = (List<Doctor>)serializer.Deserialize(reader)!;
                return doctors;
            }
        }

        public override void ShowInfo(Doctor doctor)
        {
            Console.WriteLine(); // implement view of all object fields
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, result.ToString());
        }

    }
}
