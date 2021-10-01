using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Cart
    {
        public string SearchText { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string ManufacturedBy { get; set; }
        public string ManufactureDate { get; set; }
        public string ExpiryDate { get; set; }
        public string SuppliersID { get; set; }
        public string RetailPrice { get; set; }
        public string Discount { get; set; }
        public DateTime TypeOfMedicine { get; set; }
        public string StorageCondition { get; set; }
        public bool IsActive { get; set; }
    }
}