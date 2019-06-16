using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class DomesticTripRequestModal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public int Accommodation { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class DomesticTripRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }
        public int Accommodation { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class DomesticTripRequestUiRender
    {
        public int Accommodation { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public bool isValid()
        {
            if (this.LocationFrom != null
                && this.LocationTo != null
                && this.StartDate != null
                && this.EndDate != null
                && this.LocationFrom.Trim() != ""
                && this.LocationTo.Trim() != ""
                && this.StartDate.Trim() != ""
                && this.EndDate.Trim() != "")
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