using AutoMapper;
using Drive.DAL;
using Drive.Model;
using Drive.WebAPI.ViewModels;


//Registering objects to be mapped with AutoMapper  using  a Profile and Here we have created 
//mappings for the objects below.

namespace Drive.WebAPI.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
            CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
        }

    }
}