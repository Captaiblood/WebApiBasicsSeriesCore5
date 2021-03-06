using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Dto.Address
{
    public class CreateAddressDto
    {
        
        
      
        [Required]
        public Guid CompanyGUID { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_.-]{2,150}",
             ErrorMessage = "The {0} must be 2 to 150 valid characters which are any digit, any letter and -._@+.")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "HouseNoName")]
        public string HouseNoName { get; set; }
        [Required]
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        [Required]
        public string PostCode { get; set; }
        public DateTimeOffset Createddate { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        
        //Forign Key realtion
        [Required]
        public int CompanyId { get; set; }     

        //Forign Key realtion
        [Required]
        public int AddressTypeId { get; set; }   


    }
}
