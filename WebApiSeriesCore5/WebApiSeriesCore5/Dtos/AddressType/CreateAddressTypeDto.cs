
using System.ComponentModel.DataAnnotations;

namespace WebApiSeriesCore5.Dto.AddressType
{
    public class CreateAddressTypeDto
    {
        [Required]
        public string AddressTypeName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }       
        
    }
}
