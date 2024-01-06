﻿using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : BaseController
    {
        public DriversController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);
            if(driver == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetDriverResponse>(driver);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest driver)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Driver>(driver);
            await _unitOfWork.Drivers.Add(result);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetDriver),new {driverId = result.Id},result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest driver)
        {
            if(!ModelState.IsValid)
            { 
                return BadRequest(); 
            }

            var result = _mapper.Map<Driver>(driver);
            await _unitOfWork.Drivers.Update(result);
            await _unitOfWork.SaveAsync();

            return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _unitOfWork.Drivers.GetAll();
            
            var result = _mapper.Map<IEnumerable<GetDriverResponse>>(drivers);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);
            if(driver == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Drivers.Delete(driverId);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}