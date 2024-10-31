using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Response
{
    public class ResponseObject<T> where T : class
    {
        public string? Message { get; set; }
        public T? Data {get; set; }
        public ICollection<String> Errors { get; set; } = [];
        public int Status { get; set; }
    }
}
