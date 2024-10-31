using AutoMapper;
using MagicVillaApi.Data;
using MagicVillaApi.Models;
using MagicVillaApi.Models.Dto;
using MagicVillaApi.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVillaApi.Controllers
{
    [Route("api/VillaNumberApi")]
    [ApiController]
    public class VillaNumberApiController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaNumberRepository dbVillaNumber;
        private readonly IMapper mapper;

        public VillaNumberApiController(IVillaNumberRepository dbVillaNumber, IMapper mapper)
        {
            this.dbVillaNumber = dbVillaNumber;
            this.mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villasNumbers = await dbVillaNumber.GetAllAsync();
                _response.Result = mapper.Map<List<VillaNumberDTO>>(villasNumbers);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await dbVillaNumber.GetAsync(n => n.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                _response.Result = mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            if (await dbVillaNumber.GetAsync(n=>n.VillaNo == createDTO.VillaNo) != null)
            {
                ModelState.AddModelError("CustomError", "Villa number already exists");
                return BadRequest(ModelState);
            }
            if(createDTO == null)
            {
                return BadRequest(createDTO);
            }
            var villaNumber = mapper.Map<VillaNumber>(createDTO);
            await dbVillaNumber.CreateAsync(villaNumber);
            _response.Result = mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int villaNo)
        {
            if (villaNo == 0)
            {
                return BadRequest();
            }
            var villaNumber = await dbVillaNumber.GetAsync(v => v.VillaNo == villaNo);
            if (villaNumber == null)
            {
                return NotFound();
            }
            await dbVillaNumber.RemoveAsync(villaNumber);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber([FromBody] VillaNumberUpdateDTO updateDTO)
        {
            if (updateDTO == null || updateDTO.VillaNo == 0)
            {
                return BadRequest();
            }
            var villaNumber = mapper.Map<VillaNumber>(updateDTO);
            await dbVillaNumber.UpdateAsync(villaNumber);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = mapper.Map<VillaNumberDTO>(villaNumber);
            return Ok(_response);
        }
    }
}
