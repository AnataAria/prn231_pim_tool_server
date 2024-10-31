using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Response
{
    public class ResponsePageObject <T> : ResponseObject<T> where T : class
    {
        public int Page { get; set; }
        public int TotalElement { get; set; }
        public int TotalPage { get; set; }
        public int Size { get; set; }
    }
}
