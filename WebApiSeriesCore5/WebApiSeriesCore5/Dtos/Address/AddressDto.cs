using WebApiSeriesCore5.Dto.AddressType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSeriesCore5.Dtos.Company;

namespace WebApiSeriesCore5.Dto.Address
{
    public class AddressDto
    {

       
        public int Id { get; set; }
        public Guid GUID { get; set; }
        [Required]
        public Guid CompanyGUID { get; set; }
        [Required]
        // The default allowed characters :
        //abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789-.
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
       
        public CompanyDto Company { get; set; }

        //Forign Key realtion
        [Required]
        public int AddressTypeId { get; set; }

       
        public AddressTypeDto AddressType { get; set; }
    }
}
