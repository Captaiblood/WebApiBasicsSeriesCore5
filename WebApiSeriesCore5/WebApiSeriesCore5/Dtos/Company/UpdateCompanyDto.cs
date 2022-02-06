using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Dtos.Company
{

    public class UpdateCompanyDto
    {
        public Guid GUID { get; set; }
        [Required]
        public string CompanyName { get; set; }       
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}
