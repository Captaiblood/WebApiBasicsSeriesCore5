using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Dtos.Company
{
    public class CompanyDto
    {

        public int Id { get; set; }
        public Guid GUID { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9._@+-]{2,150}",
              ErrorMessage = "The {0} must be 1 to 150 valid characters which are any digit, any letter and -._@+.")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "CompanyName")]
        public string CompanyName { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}
