using System.Collections.Generic;

namespace BusinessLogicLayer.DataModel
{
    public class TagDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PostDataModel> Posts { get; set; }
    }
}
