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
    
    public partial class NeuMessages
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public string EmptyMessage { get; set; }
        public Nullable<int> Processed { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Target { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual NueUserProfile NueUserProfile { get; set; }
    }
}