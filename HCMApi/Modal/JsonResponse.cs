using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class JsonResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public object payload { get; set; }

        public JsonResponse() { }
        public JsonResponse(string status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public JsonResponse(string status, string message, object payload)
        {
            this.status = status;
            this.message = message;
            this.payload = payload;
        }
    }
}
