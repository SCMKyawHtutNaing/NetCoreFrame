using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Entities.DTO
{
    public class ResponseModel
    {
        public int MessageType { get; set; } = 0;
        public string Message { get; set; } = "";
    }
}
