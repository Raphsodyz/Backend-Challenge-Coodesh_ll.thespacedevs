﻿using Business.Interface;
using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Services.Controllers
{
    [ApiController]
    [Route("api")]
    public class LaunchController : ControllerBase
    {
        private readonly ILaunchApiBusiness _launchApiBusiness;
        public LaunchController(ILaunchApiBusiness launchApiBusiness)
        {
            _launchApiBusiness = launchApiBusiness;
        }

        [HttpGet]
        public IActionResult GetServiceRunning()
        {
            return Ok("REST Back-end Challenge 20201209 Running");
        }

        [HttpGet]
        [Route("launchers/{launchId}")]
        public IActionResult GetById(int launchId)
        {
            try
            {
                var launch = _launchApiBusiness.GetOneLaunch(launchId);
                return Ok(launch);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpGet]
        [Route("launchers")]
        public IActionResult GetAllPaged(int page)
        {
            try
            {
                Pagination<Launch> pagedLaunchList = _launchApiBusiness.GetAllLaunchPaged(page);
                return Ok(new { CurrentlyPage = pagedLaunchList.CurrentPage, TotalRegisters = pagedLaunchList.NumberOfEntities, Pages = pagedLaunchList.NumberOfPages, Data = pagedLaunchList.Entities });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpDelete]
        [Route("launchers/hard-delete/{launchId}")]
        public IActionResult HardDelete(int launchId)
        {
            try
            {
                _launchApiBusiness.HardDeleteLaunch(launchId);
                return Ok(SuccessMessages.DeletedEntity);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpDelete]
        [Route("launchers/soft-delete/{launchId}")]
        public IActionResult SoftDelete(int launchId)
        {
            try
            {
                _launchApiBusiness.SoftDeleteLaunch(launchId);
                return Ok(SuccessMessages.DeletedEntity);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpPut]
        [Route("launchers/{launchId}")]
        public async Task<IActionResult> Edit(int launchId)
        {
            try
            {
                Launch updatedLaunch = await _launchApiBusiness.UpdateLaunch(launchId);
                return Ok(updatedLaunch);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpPost]
        [Route("launchers")]
        public async Task<IActionResult> UpdateData()
        {
            try
            {
                bool updated = await _launchApiBusiness.UpdateDataSet();
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }
    }
}