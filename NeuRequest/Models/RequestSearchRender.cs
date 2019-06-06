using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class RequestSearchRender
    {
        public UserRequest userRequest { get; set; }
        public bool ishcm { get; set; }
        public bool isOwner { get; set; }
        public bool isApprover { get; set; }

        public RequestSearchRender(UserRequest userRequest, bool ishcm, bool isOwner, bool isApprover)
        {
            this.userRequest = userRequest;
            this.ishcm = ishcm;
            this.isOwner = isOwner;
            this.isApprover = isApprover;
        }
    }
}