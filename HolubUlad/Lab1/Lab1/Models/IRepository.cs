using System.Collections.Generic;

namespace Lab1.Models
{
    interface IRepository
    {
        List<JsonStudent> Get();
        void Create(JsonStudent item);
        void Update(JsonStudent item);
        void Delete(int Id);
    }
}
