using AutoMapper;
using Drive.Common.Helpers;
using Drive.Model;
using Drive.Model.Common;
using Drive.Service.Common;
using Drive.WebAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Drive.WebAPI.Controllers
{

    public class VehicleModelController : ApiController
    {

        private readonly IVehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;



        public VehicleModelController(IVehicleModelService vehicleModelService, IMapper mapper)
        {

            _vehicleModelService = vehicleModelService;
            _mapper = mapper;

        }


        // GET: api/vehiclemodel
        [HttpGet]
        public async Task<IHttpActionResult> GetVehicleModels(string sortBy, string searchString, int? page)
        {
            Filtering filters = new Filtering(searchString);
            Sorting sorting = new Sorting(sortBy);
            Paging paging = new Paging(page);

            var vehicleModels = await _vehicleModelService.GetVehicleModelsAsync(filters, sorting, paging);

            List<VehicleModelViewModel> listVehicleModelViewModels =
            _mapper.Map<List<VehicleModelViewModel>>(vehicleModels);




            return Ok(new
            {
                vehicles = listVehicleModelViewModels,
                pagingInfo = new
                {
                    resultsPerPage = paging.NumberOfObjectsPerPage,
                    totalCount = paging.TotalCount,
                    pageNumber = paging.Page,
                }
            });
        }





        [ResponseType(typeof(VehicleModelViewModel))]
        public async Task<IHttpActionResult> GetVehicleModelByID(int id)
        {


            IVehicleModel vehicleModel = await _vehicleModelService.GetVehicleModelByIDAsync(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModelViewModels = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return Ok(vehicleModelViewModels);
        }



        [ResponseType(typeof(VehicleModelViewModel))]
        public async Task<IHttpActionResult> CreateVehicleModel(VehicleModel vehicleModelToInsert)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _vehicleModelService.CreateVehicleModel(vehicleModelToInsert);

            VehicleModelViewModel vehicleModelViewModels = _mapper.Map<VehicleModelViewModel>(vehicleModelToInsert);



            return CreatedAtRoute("DefaultApi",
                                            new { id = vehicleModelViewModels.ID }, vehicleModelViewModels);

        }




        [ResponseType(typeof(VehicleMakeViewModel))]
        public async Task<IHttpActionResult> PutVehicleModel(int id, VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }
            if (id != vehicleModel.ID)
            {
                return BadRequest();
            }

            var vehicleisUpdated = await _vehicleModelService.EditVehicleModel(vehicleModel);
            VehicleModelViewModel vehicleModelViewModels = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            if (vehicleisUpdated == true)
            {

                return Ok(vehicleModelViewModels);
            }
            else
            {
                return BadRequest();
            }


        }


        [ResponseType(typeof(VehicleMakeViewModel))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteVehicleModel(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            IVehicleModel vehicleModel = await _vehicleModelService.GetVehicleModelByIDAsync(id);



            if (vehicleModel == null)
            {
                return NotFound();
            }

            await _vehicleModelService.DeleteVehicleModel(id);

            VehicleModelViewModel vehicleModelViewModels = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return Ok(vehicleModelViewModels);


        }

        protected override void Dispose(bool disposing)
        {
            _vehicleModelService.Dispose();
            base.Dispose(disposing);
        }



    }

}


