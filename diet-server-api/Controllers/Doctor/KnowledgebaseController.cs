using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/knowledgebase")]
    public class KnowledgebaseController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly ISupplementRepository _supplementRepo;
        private readonly IParameterRepository _paramRepo;
        private readonly IMealRepository _mealRepo;
        private readonly IDiseaseRepository _diseaseRepo;

        public KnowledgebaseController(IProductRepository productRepo, ISupplementRepository supplementRepo, IParameterRepository paramRepo, IMealRepository mealRepo, IDiseaseRepository diseaseRepo)
        {
            _productRepo = productRepo;
            _supplementRepo = supplementRepo;
            _paramRepo = paramRepo;
            _mealRepo = mealRepo;
            _diseaseRepo = diseaseRepo;
        }

        [HttpPost]
        [Route("disease")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddDisease(AddDiseaseRequest request)
        {
            try
            {
                var response = await _diseaseRepo.AddDisease(request);
                return CreatedAtAction(nameof(AddDisease), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("supplement")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddSupplement(AddSupplementRequest request)
        {
            try
            {
                var response = await _supplementRepo.AddSupplement(request);
                return CreatedAtAction(nameof(AddSupplement), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("supplements")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSupplements(int page)
        {
            var response = await _supplementRepo.GetSupplements(page);
            return Ok(response);
        }
        [HttpGet]
        [Route("diseases")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDiseases(int page)
        {
            var response = await _diseaseRepo.GetDiseases(page);
            return Ok(response);
        }

        [HttpGet]
        [Route("diseases/search")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchDisease([FromQuery] string diseaseName)
        {
            try
            {
                var response = await _diseaseRepo.SearchDisease(diseaseName);
                return Ok(response);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("supplement/search")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> SearchSupplement([FromQuery] string supplementName)
        {
            try
            {
                var response = await _supplementRepo.SearchSupplement(supplementName);
                return Ok(response);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Route("product")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {
            try
            {
                var response = await _productRepo.AddProduct(request);
                return CreatedAtAction(nameof(AddProduct), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("products")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProducts([FromQuery] int page)
        {
            try
            {
                var response = await _productRepo.GetProducts(page);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("parameters")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetParameters()
        {
            try
            {
                var response = await _paramRepo.GetParameters();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("parameter")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddParameter(AddParameterRequest request)
        {
            try
            {
                var response = await _paramRepo.AddParameter(request);
                return CreatedAtAction(nameof(AddParameter), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("supplement")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSupplement(UpdateSupplementRequest request)
        {
            try
            {
                var response = await _supplementRepo.UpdateSupplement(request);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("disease")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDisease(UpdateDiseaseRequest request)
        {
            try
            {
                var response = await _diseaseRepo.UpdateDisease(request);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("product/search")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchProduct([FromQuery] string productName)
        {
            try
            {
                var response = await _productRepo.SearchProduct(productName);
                return Ok(response);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("product")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            try
            {
                var response = await _productRepo.UpdateProduct(request);
                return Ok(response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [Route("meal")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddMeal(AddMealRequest request)
        {
            try
            {
                var response = await _mealRepo.AddMeal(request);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("product/parameters")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductParams([FromQuery] int IdProduct)
        {
            try
            {
                var response = await _productRepo.GetProductParams(IdProduct);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("meals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMeals([FromQuery] int page)
        {
            try
            {
                var response = await _mealRepo.GetMeals(page);
                return Ok(response);
            }
            catch (NotFound ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("meal/search")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchMeal([FromQuery] string mealName)
        {
            try
            {
                var response = await _mealRepo.SearchMeal(mealName);
                return Ok(response);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}