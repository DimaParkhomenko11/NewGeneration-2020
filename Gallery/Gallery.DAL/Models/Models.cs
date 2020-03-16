using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models
{
    public class LoginModel
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Line length must be between 3 and 30 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Line length must be between 3 and 30 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
