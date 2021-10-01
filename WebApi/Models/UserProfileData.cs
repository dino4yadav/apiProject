using System;

namespace WebApi.Models
{
    public class UserProfileData
    {
        public string SearchText { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}