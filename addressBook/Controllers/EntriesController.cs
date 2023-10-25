using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Dtos;
using addressBook.Models;
using addressBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace addressBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryService _entryService;

        public EntriesController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<EntryResponseDto>>>> GetAllEntries()
        {
            return Ok(await _entryService.GetAllEntries());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<EntryResponseDto>>> GetEntryById(Guid id)
        {
            var response = await _entryService.GetEntryById(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<EntryResponseDto>>> CreateNewEntry(
            EntryRequestDto newEntry
        )
        {
            var serviceResponse = await _entryService.CreateNewEntry(newEntry);

            if (serviceResponse.Success)
            {
                return CreatedAtAction(
                    nameof(GetEntryById),
                    new { id = serviceResponse.Data.Id },
                    serviceResponse.Data
                );
            }

            return BadRequest(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<EntryResponseDto>>> UpdateEntry(
            EntryUpdateRequestDto updatedEntry
        )
        {
            var response = await _entryService.UpdateEntry(updatedEntry);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<EntryResponseDto>>>> DeleteEntry(
            Guid id
        )
        {
            var response = await _entryService.DeleteEntry(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
