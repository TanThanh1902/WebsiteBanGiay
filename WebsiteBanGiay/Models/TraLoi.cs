//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteBanGiay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TraLoi
    {
        public int MaTraLoi { get; set; }
        public int MaBinhLuan { get; set; }
        public int MaNguoiDung { get; set; }
        public string TraLoi1 { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
    
        public virtual BinhLuan BinhLuan { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}