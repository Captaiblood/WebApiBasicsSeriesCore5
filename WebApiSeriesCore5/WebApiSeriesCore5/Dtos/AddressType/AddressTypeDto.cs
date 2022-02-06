namespace WebApiSeriesCore5.Dto.AddressType
{
    public class AddressTypeDto
    {
       
        public int Id { get; set; }       
        public string AddressTypeName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
