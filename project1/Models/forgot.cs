//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class forgot
    {
        public int id { get; set; }
        public string emailid { get; set; }
        public string securityquestion { get; set; }
        public string securityanswer { get; set; }
        public string newpassword { get; set; }
        public string confirmpassword { get; set; }
    }
}