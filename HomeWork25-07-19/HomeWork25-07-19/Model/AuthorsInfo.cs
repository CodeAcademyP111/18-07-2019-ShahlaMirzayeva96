//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeWork25_07_19.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuthorsInfo
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<bool> Deleted { get; set; }
    
        public virtual City City { get; set; }
    }
}
