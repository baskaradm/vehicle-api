using AutoMapper;
using Drive.Common.Helpers;
using Drive.Model;
using Drive.Model.Common;
using Drive.Service.Common;
using Drive.WebAPI.ViewModels;
using System.Collections.Generic;
using System.Net;
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

           VehicleModelViewModel vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return Ok(vehicleModelViewModel);
        }



        [ResponseType(typeof(VehicleModelViewModel))]
        public async Task<IHttpActionResult> CreateVehicleModel(VehicleModelViewModel vehicleModelViewModel)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }


            VehicleModel vehicleModel = _mapper.Map<VehicleModel>(vehicleModelViewModel);

            await _vehicleModelService.CreateVehicleModel(vehicleModel);

            return CreatedAtRoute("DefaultApi",
                                            new { id = vehicleModelViewModel.ID }, vehicleModelViewModel);

        }




        [ResponseType(typeof(VehicleMakeViewModel))]
        public async Task<IHttpActionResult> PutVehicleModel(int id, VehicleModelViewModel vehicleModelViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }
            if (id != vehicleModelViewModel.ID)
            {
                return BadRequest();
            }

            VehicleModel vehicleModel = _mapper.Map<VehicleModel>(vehicleModelViewModel);
            await _vehicleModelService.EditVehicleModel(vehicleModel);


            return Ok(vehicleModelViewModel);


        }


        
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
            return StatusCode(HttpStatusCode.NoContent);

        }

     



    }

}


