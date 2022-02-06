using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.DtoMapper
{
    public class Mappings:Profile
    {

        public Mappings()
        {
            CreateMap<Entities.Company, Dtos.Company.CompanyDto>().ReverseMap();
            CreateMap<Entities.Company, Dtos.Company.UpdateCompanyDto>().ReverseMap();
        }
        
    }
}
