using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.DataTransferObject
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string TitlePost { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreate { get; set; }
        public string Author { get; set; }
        public bool OperationRecord { get; set; }
    }
}
