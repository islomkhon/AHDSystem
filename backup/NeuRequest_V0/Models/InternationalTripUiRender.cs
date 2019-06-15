using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class InternationalTripRequestModal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public int NeedVisiaProcessing { get; set; }
        public string PlaceToVisit { get; set; }
        public string StartDate { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class InternationalTripRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }
        public int NeedVisiaProcessing { get; set; }
        public string PlaceToVisit { get; set; }
        public string StartDate { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class InternationalTripUiRender
    {
        public int NeedVisiaProcessing { get; set; }
        public string PlaceToVisit { get; set; }
        public string StartDate { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public bool isValid()
        {
            if (this.PlaceToVisit != null
                && this.StartDate != null
                && this.PlaceToVisit.Trim() != ""
                && this.StartDate.Trim() != "")
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