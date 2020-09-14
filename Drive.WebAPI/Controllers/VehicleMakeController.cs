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
   
    public class VehicleMakeController : ApiController
    {

        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;



        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {

            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;

        }



        // GET: api/vehiclemake
        [HttpGet]
        public async Task<IHttpActionResult> GetVehicleMakes(string sortBy, string searchString, int? page)
        {
            Filtering filters = new Filtering(searchString );
            Sorting sorting = new Sorting(sortBy);
            Paging paging = new Paging(page);

            var vehicleMakes = await _vehicleMakeService.GetVehicleMakes(filters, sorting, paging);

            List<VehicleMakeViewModel> listVehicleMakeViewModels =
            _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);




            return Ok(new
            {
                vehicles = listVehicleMakeViewModels,
                pagingInfo = new
                {
                    resultsPerPage = paging.NumberOfObjectsPerPage,
                    totalCount = paging.TotalCount,
                    pageNumber = paging.Page,
                }
            });
        }
    

    
    
       
        [ResponseType(typeof(VehicleMakeViewModel))]
        [HttpGet]
        public async Task<IHttpActionResult> GetVehicleMakeByID(int? id)
        {
            

            IVehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMakeByID(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return Ok(vehicleMakeViewModels);
        }


       
        [ResponseType(typeof(VehicleMakeViewModel))]
        [HttpPost]
        public async Task<IHttpActionResult> CreateVehicleMake(VehicleMake vehicleMakeToInsert)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vehicleMakeService.CreateVehicleMake(vehicleMakeToInsert);
            
           VehicleMakeViewModel vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel> (vehicleMakeToInsert);

            

            return CreatedAtRoute("DefaultApi",
                                            new { id = vehicleMakeViewModels.VehicleMakeId }, vehicleMakeViewModels);

        }



        [ResponseType(typeof(VehicleMakeViewModel))]
        [HttpPut]
        public async Task<IHttpActionResult> PutVehicleMake(int id, VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != vehicleMake.VehicleMakeId)
            {
                return BadRequest();
            }

            await _vehicleMakeService.EditVehicleMake(vehicleMake);
            VehicleMakeViewModel vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
           
                return Ok(vehicleMakeViewModels);
            
            


        }


        [ResponseType(typeof(VehicleMakeViewModel))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteVehicleMake(int id)
        {

            IVehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMakeByID(id);

            

            if (vehicleMake == null)
            {
                return NotFound();
            }

            await _vehicleMakeService.DeleteVehicleMake(id);

            VehicleMakeViewModel vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return Ok(vehicleMakeViewModels);


        }

        protected override void Dispose(bool disposing)
        {
            _vehicleMakeService.Dispose();
            base.Dispose(disposing);
        }



    }

}


  