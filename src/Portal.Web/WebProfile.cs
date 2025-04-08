using AutoMapper;
using Portal.Domain.Entities;
using Portal.Web.Areas.Dashboard.Models;

namespace Portal.Web
{
    public class WebProfile : Profile
    {
        public WebProfile() 
        {
            CreateMap<AddStudentModel, Student>().ReverseMap();
            CreateMap<UpdateStudentModel, Student>().ReverseMap();
        }
    }
}
