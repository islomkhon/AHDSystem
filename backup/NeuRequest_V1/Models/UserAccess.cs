using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class UserAccess
    {
        public int MapperId { get; set; }
        public int AccessItemId { get; set; }
        public string AccessDesc { get; set; }
    }
}