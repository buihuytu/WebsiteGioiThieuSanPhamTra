//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanTra.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblNew
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public Nullable<int> IsActive { get; set; }
        public string Summary { get; set; }
    }
}
