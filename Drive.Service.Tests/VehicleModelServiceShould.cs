using Drive.Common.Helpers;
using Drive.Model;
using Drive.Model.Common;
using Drive.Repository.Common;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Drive.Service.Tests
{
    public class VehicleModelServiceShould
    {
        private readonly VehicleModelService _sut;
        private readonly Mock<IVehicleModelRepository> _modelRepoMock = new Mock<IVehicleModelRepository>();
        private readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();

        public VehicleModelServiceShould()
        {
            _sut = new VehicleModelService(_uowMock.Object, _modelRepoMock.Object);
        }

        [Fact]
        public async Task ReturnModelList()
        {
            //Arrange
            var vehicleModels = new List<IVehicleModel>()
            {
                new VehicleModel()
                {
                    ID = 1,
                    Name = "X6",
                    Abbreviation = "BMW",
                    VehicleMakeId = 1
                },
                new VehicleModel()
                {
                    ID = 2,
                    Name = "Golf5",
                    Abbreviation = "VW",
                    VehicleMakeId = 2
                },
                new VehicleModel()
                {
                    ID = 3,
                    Name = "Ford Mustang",
                    Abbreviation = "Ford",
                    VehicleMakeId = 3
                },
                new VehicleModel()
                {
                    ID = 4,
                    Name = "Ford Mondeo",
                    Abbreviation = "Ford",
                    VehicleMakeId = 4
                },
            }.AsEnumerable();

            string searchString = "";
            string sortBy = "";
            int page = 0;

            Filtering filters = new Filtering(searchString);
            Sorting sorting = new Sorting(sortBy);
            Paging paging = new Paging(page);

            _modelRepoMock.Setup(x => x.GetAll(filters, sorting, paging)).Returns(Task.FromResult(vehicleModels));
            //Act
            var result = await _sut.GetVehicleModelsAsync(filters, sorting, paging);

            //Assert
            result.Should().BeEquivalentTo(vehicleModels);
        }

        [Fact]
        public async Task ReturnEmptyModelList()
        {
            //Arrange
            var vehicleModels = new List<IVehicleModel>().AsEnumerable();

            string searchString = "";
            string sortBy = "";
            int page = 0;

            Filtering filters = new Filtering(searchString);
            Sorting sorting = new Sorting(sortBy);
            Paging paging = new Paging(page);

            _modelRepoMock.Setup(x => x.GetAll(filters, sorting, paging)).Returns(Task.FromResult(vehicleModels));
            //Act
            var result = await _sut.GetVehicleModelsAsync(filters, sorting, paging);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task ReturnVehicleModel()
        {
            //Arrange
            var modelId = 3;
            var vehicleModel = new VehicleModel()
            {
                ID = modelId,
                Name = "Car Model",
                Abbreviation = "CM",
                VehicleMakeId = 1
            };

            _modelRepoMock.Setup(x => x.FindById(modelId)).ReturnsAsync(vehicleModel);

            //Act
            IVehicleModel model = await _sut.GetVehicleModelByIDAsync(modelId);

            //Assert
            model.ID.Should().Be(modelId);
        }

        [Fact]
        public async Task CreateModel()
        {
            //Arrange
            var vehicleModel = new VehicleModel()
            {
                ID = 1,
                Name = "Car Model",
                Abbreviation = "CM",
                VehicleMakeId = 1
            };

            _modelRepoMock.Setup(x => x.Create(vehicleModel)).ReturnsAsync(true);

            //Act
            var result = await _sut.CreateVehicleModel(vehicleModel);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task EditModel()
        {
            //Arrange
            var vehicleModel = new VehicleModel()
            {
                ID = 1,
                Name = "Car Model",
                Abbreviation = "CM",
                VehicleMakeId = 1
            };

            _modelRepoMock.Setup(x => x.Edit(vehicleModel)).ReturnsAsync(true);

            //Act
            var result = await _sut.EditVehicleModel(vehicleModel);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteModel()
        {
            //Arrange
            var modelId = 3;

            _modelRepoMock.Setup(x => x.Delete(modelId)).ReturnsAsync(true);

            //Act
            var result = await _sut.DeleteVehicleModel(modelId);

            //Assert
            result.Should().BeTrue();
        }
    }
}
