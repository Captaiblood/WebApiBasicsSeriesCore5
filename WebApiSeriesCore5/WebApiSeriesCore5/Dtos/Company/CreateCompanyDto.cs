using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Dtos.Company
{
    public class CreateCompanyDto
    {
        [Required(ErrorMessage = "Company name is required")]
        [MinLength(2, ErrorMessage = "Company Name can not be less than two characters")]
        [MaxLength(150, ErrorMessage = "Company Name to long")]
        public string CompanyName { get; set; }

    }
}
