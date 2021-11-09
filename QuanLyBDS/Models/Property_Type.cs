﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyBDS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Property_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property_Type()
        {
            this.Properties = new HashSet<Property>();
        }
    
        public int ID { get; set; }
        [Required]
        [Display(Name = "Loại Bất động sản")]
        [MinLength(5, ErrorMessage = "Vui lòng nhập nhiều hơn 5 ký tự")]
        [MaxLength(30, ErrorMessage = "Vui lòng nhập ít hơn 5 ký tự")]
        public string Property_Type_Name { get; set; }
        [Required]
        [Display(Name = "Số lượng")]
        [Range(5, 10, ErrorMessage = "Số lượng phải nằm trong khoảng 5 đến 10")]
        public Nullable<int> Property_Amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }
    }
}
