using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonksInn.Domain.Enums
{
    public enum Role
    {
        Admin,
        Manager,
        [Display(Name ="Staff Member")]
        StaffMember
    }
}
