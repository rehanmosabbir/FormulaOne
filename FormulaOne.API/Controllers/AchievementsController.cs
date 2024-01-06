using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : BaseController
    {
        public AchievementsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        [HttpGet]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriversAchievement(Guid driverId)
        {
            var driversAchievement = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId);
            if (driversAchievement == null)
                return NotFound("Achievement not found");

            var result = _mapper.Map<GetDriverAchievementResponse>(driversAchievement);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Achievement>(achievement);
            await _unitOfWork.Achievements.Add(result);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetDriversAchievement), new { driverId = result.DriverId }, result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateAchievement([FromBody] UpdateDriverAchievementRequest achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Achievement>(achievement);
            await _unitOfWork.Achievements.Update(result);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
