//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NeuRequest.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class NeuEmployeeVerificationRequest
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Message { get; set; }
        public System.DateTime AddedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual NueUserProfile NueUserProfile { get; set; }
    }
}
