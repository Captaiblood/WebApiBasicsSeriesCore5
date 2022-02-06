
using System.ComponentModel.DataAnnotations;


namespace WebApiSeriesCore5.Dto.AddressType
{
    public class UpdateAddressTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string AddressTypeName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }       
        
    }
}
