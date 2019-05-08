using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHD.Models
{
    public class JsonResponse
    {
        public string status { get; set; }
        public string message { get; set; }

        public JsonResponse() { }
        public JsonResponse(string status, string message) {
            this.status = status;
            this.message = message;
        }

    }
}