//------------------------------------------------------------------------------
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

    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            this.Full_Contract = new HashSet<Full_Contract>();
            this.Installment_Contract = new HashSet<Installment_Contract>();
            this.Property_Service = new HashSet<Property_Service>();
        }
        
        
        public int ID { get; set; }
  
        public string Property_Code { get; set; }
        [Required(ErrorMessage = "Tên Bất Động Sản không được để trống")]
        [MinLength(5,ErrorMessage ="Tên bất động sản lớn hơn 5 kí tự")]
        [MaxLength(100,ErrorMessage ="Tên Bất động sản không quá 100 kí tự")]
        public string Property_Name { get; set; }
        public Nullable<int> Property_Type_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mô tả Bất Động Sản")]
        [Display(Name = "Mô Tả Bất Động Sản")]
        [StringLength(255, ErrorMessage = "Mô tả bất động sản không quá 255 kí tự")]   
        public string Description { get; set; }
        public Nullable<int> District_ID { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(100,ErrorMessage = "Địa chỉ Không quá 100 kí tự")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Diện tích không được để trống")]
        [Range(1,10000,ErrorMessage ="Diện tích nằm trong khoảng 1 - 10000(m2) ")]
        public Nullable<int> Area { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng Phòng ngủ")]
        [Range(0,5,ErrorMessage = "Phòng ngủ nằm trong khoảng 0 - 5 (phòng)")]
        public Nullable<int> Bed_Room { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng phòng tắm")]
        [Range(1, 10, ErrorMessage = "Số phòng tắm trong khoảng 1 - 10 (Phòng)")]
        public Nullable<int> Bath_Room { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá trị Bất động sản")]
        [RegularExpression(@"^[0-9]*$")]
        [Range(1000,100000000000,ErrorMessage ="Giá bán tối đa từ 1000 (vnd) - 100 tỷ (vnd)")]
        public Nullable<decimal> Price { get; set; }
        [Display(Name = "Lãi suất trả góp")]
        [Required(ErrorMessage = " Vui lòng nhập Lãi suất trả góp")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{2})$", ErrorMessage = "Phải nhập số thực với 2 chữ số sau dấu phẩy")]
        public Nullable<double> Installment_Rate { get; set; }
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Vui lòng chọn file hình ảnh")]
        public string Avatar { get; set; }
        public string Album { get; set; }
        public Nullable<int> Property_Status_ID { get; set; }
   
        public virtual District District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Full_Contract> Full_Contract { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Installment_Contract> Installment_Contract { get; set; }

        public virtual Property_Status Property_Status { get; set; }
    
        public virtual Property_Type Property_Type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    
        public virtual ICollection<Property_Service> Property_Service { get; set; }

        internal object ToList()
        {
            throw new NotImplementedException();
        }
    }
}



