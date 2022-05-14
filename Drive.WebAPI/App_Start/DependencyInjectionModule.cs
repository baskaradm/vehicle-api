using Autofac;
using Autofac.Integration.WebApi;
using Drive.DAL;
using Drive.Model;
using Drive.Model.Common;
using Drive.Repository;
using Drive.Repository.Common;
using Drive.Service;
using Drive.Service.Common;

namespace Drive.WebAPI.App_Start
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>().InstancePerRequest();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>().InstancePerRequest();
            builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>().InstancePerRequest();
            builder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>().InstancePerRequest();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerRequest();
            builder.RegisterType<DriveContext>().InstancePerRequest();
            builder.RegisterType<VehicleMake>().As<IVehicleMake>().InstancePerRequest();
            builder.RegisterType<VehicleModel>().As<IVehicleModel>().InstancePerRequest();



        }
    }

}

