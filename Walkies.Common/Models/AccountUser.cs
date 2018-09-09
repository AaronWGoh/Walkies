using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Walkies.Common.Models
{
    public class AccountUser
    {
        public Guid Id { get { return AccountUserId; } set { AccountUserId = value; } }
        [Display(Name = "AccountUser Id")]
        public Guid AccountUserId { get; set; }
        [Required, Display(Name = "UserType Code"), MaxLength(10)]
        public string UserTypeCode { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Login Email"), MaxLength(100)]
        public string LoginEmail { get; set; }
        [Required, Display(Name = "Recovery Phone"), MaxLength(20)]
        public string RecoveryPhone { get; set; }
        [Required, Display(Name = "Password"), MaxLength(70), DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        public string Password { get; set; }
        [Display(Name = "Can Login")]
        public bool? CanLogin { get; set; }
        public DateTime? IsLockedDateTime { get; set; }
        [Display(Name = "Reset Token"), MaxLength(50)]
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }
    }
}
