using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Entities
{
    public class AddressType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AddressTypeName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}
