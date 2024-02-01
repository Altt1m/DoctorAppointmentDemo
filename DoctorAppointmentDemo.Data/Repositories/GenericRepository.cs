using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace DoctorAppointmentDemo.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource: Auditable
    {
        public abstract string JsonPath { get; set; }
        public abstract string XmlPath { get; set; }
        public abstract int LastId { get; set; }

        public TSource Create(TSource source, string format)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            if (format == "xml")
            {
                CreateAsXml(source);
            }
            else if (format == "json")
            {
                File.WriteAllText(JsonPath, JsonConvert.SerializeObject(GetAll().Append(source), Formatting.Indented));
            }
            
            SaveLastId();

            return source;
        }

        public bool Delete(int id)
        {
            if (GetById(id) is null)
                return false;

            File.WriteAllText(JsonPath, JsonConvert.SerializeObject(GetAll().Where(x => x.Id != id), Formatting.Indented));
            
            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            if(!File.Exists(JsonPath))
            {
                File.WriteAllText(JsonPath, "[]");
            }

            var json = File.ReadAllText(JsonPath);

            if (string.IsNullOrWhiteSpace(json))
            {
                File.WriteAllText(JsonPath, "[]");
                json = "[]";
            }

            return JsonConvert.DeserializeObject<List<TSource>>(json)!;
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            File.WriteAllText(JsonPath, JsonConvert.SerializeObject(GetAll().Select(x => x.Id == id ? source : x), Formatting.Indented));

            return source;
        }

        public abstract void CreateAsXml(TSource source);

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected dynamic ReadFromAppSettings() => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.AppSettingsPath))!;
    }
}
