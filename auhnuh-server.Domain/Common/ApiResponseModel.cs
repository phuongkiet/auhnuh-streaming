using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.Common
{
    public class ApiResponseModel
    {
        public bool Success => Errors.Count == 0;
        public List<string> Errors { get; set; }
        public Dictionary<string, string> FormErrors { get; set; }

        public ApiResponseModel()
        {
            Errors = new List<string>();
        }
    }

    public class ApiResponseModel<T> : ApiResponseModel
    {
        public T Data { get; set; }

        public ApiResponseModel()
        {
            Errors = new List<string>();
        }
    }
}
