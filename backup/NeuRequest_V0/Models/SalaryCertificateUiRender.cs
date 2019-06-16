﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class SalaryCertificateModal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class SalaryCertificate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class SalaryCertificateUiRender
    {
        public string message { get; set; }
        public bool isValid()
        {
            if (this.message != null
                && this.message.Trim() != "")
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}