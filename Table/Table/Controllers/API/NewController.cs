using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Table.Controllers.API
{
    public class NewController : ApiController
    {
        string path = @"C:\Users\Admin\source\repos\Table\data.csv";
        

        // GET: api/New
        public IEnumerable<string[]> Get()
        {
            List<string[]> data = new List<string[]>();
            if (System.IO.File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {              
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        string[] values = line.Split(';');
            
                        data.Add(values);
                    }
                }
            }
            return data;
        }

        // GET: api/New/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/New
        public string[] Post([FromBody] string value)
        {
            //dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(value);
            string name = data[0].value;
            string surname = data[1].value;
            string email = data[2].value;
            string date = data[3].value;
            string city = data[4].value;
            string gender = data[5].value;
            
            string student = String.Format("{0};{1};{2};{3};{4};{5}", name, surname, gender, city, date, email);
            StringBuilder csv = new StringBuilder();
            csv.AppendLine(student);
            File.AppendAllText(path, csv.ToString());

            return student.Split(';');
        }

        // PUT: api/New/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/New/5
        public void Delete(int id)
        {
        }
    }
}
