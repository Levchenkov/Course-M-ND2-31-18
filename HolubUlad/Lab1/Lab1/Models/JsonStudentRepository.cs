using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace Lab1.Models
{
    public class JsonStudentRepository: IRepository
    {
        private readonly string _fileName = HostingEnvironment.MapPath(@"/App_Data/database.json");
                
        public List<JsonStudent> Get()
        {
            var jsonStudentList = new List<JsonStudent>();

            if (File.Exists(_fileName)) { jsonStudentList = GetListFromFile(); }
            return jsonStudentList;
        }

        public void Create(JsonStudent jsonStudent)
        {
            var jsonStudentList = new List<JsonStudent>();

            if (File.Exists(_fileName)) { jsonStudentList = GetListFromFile(); }

            int id = GetMaxIdFromList(jsonStudentList) + 1;
            jsonStudent.Id = id;
            jsonStudentList.Add(jsonStudent);
            SerializeJsonToFile(jsonStudentList);
        }

        public void Update(JsonStudent jsonStudent)
        {
            var jsonStudentList = new List<JsonStudent>();

            if (File.Exists(_fileName)) { jsonStudentList = GetListFromFile(); }

            var index = jsonStudentList.FindIndex(x => x.Id == jsonStudent.Id);
            jsonStudentList[index] = jsonStudent;
            SerializeJsonToFile(jsonStudentList);
        }

        public void Delete(int id)
        {
            List<JsonStudent> jsonStudentList = GetListFromFile();

            var found = jsonStudentList.Find(x => x.Id == id);

            if (found != null) { jsonStudentList.Remove(found); }

            SerializeJsonToFile(jsonStudentList);

        }
        
        private List<JsonStudent> GetListFromFile()
        {
            List<JsonStudent> returnList = null;

            using (StreamReader file = File.OpenText(_fileName))
            {
                var serializer = new JsonSerializer();
                returnList = (List<JsonStudent>)serializer.Deserialize(file, typeof(List<JsonStudent>));
            }

            return returnList;
        }

        private int GetMaxIdFromList(List<JsonStudent> list)
        {
            int maxId = 0;

            foreach(var elem in list)
            {
                if(elem.Id > maxId) { maxId = elem.Id; }
            }

            return maxId;
        }

        private void SerializeJsonToFile(List<JsonStudent> list)
        {
            using (StreamWriter file = File.CreateText(_fileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, list);
            }
        }
    }
}