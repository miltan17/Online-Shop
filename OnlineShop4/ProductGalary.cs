//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShop4
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    
    public partial class ProductGalary
    {
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public int ImageSize { get; set; }

        [Required(ErrorMessage = "Please select image file.")]
        public HttpPostedFileBase File { get; set; }
    }
}
