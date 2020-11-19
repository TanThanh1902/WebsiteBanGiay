using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanGiay.Models
{
    public class GioHangs
    {
        public int maGiay { get; set; }
        public string tenGiay { get; set; }
        public string hinhAnh { get; set; }
        public decimal donGia { get; set; }
        public int soLuong { get; set; }
        public double thanhTien 
        { 
            get
            {
                return (double)soLuong * (double)donGia;
            }
        }

    }
}