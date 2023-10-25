using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Dtos;
using addressBook.Models;
using AutoMapper;

namespace addressBook.Services
{
    public class EntryService : IEntryService
    {
        private static List<Entry> entries = new List<Entry>()
        {
            new Entry()
            {
                Id = Guid.NewGuid(),
                Name = "Adila",
                Address = new Address()
                {
                    Street = "Geodesivägen 11B",
                    City = "Huddinge",
                    PostalCode = 14137
                }
            },
            new Entry()
            {
                Id = Guid.NewGuid(),
                Name = "Dahlia",
                Address = new Address()
                {
                    Street = "Tätorpsvägen 11",
                    City = "Skarpnäck",
                    PostalCode = 12831
                }
            }
        };
        private readonly IMapper _mapper;

        public EntryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<EntryResponseDto>> CreateNewEntry(
            EntryRequestDto newEntry
        )
        {
            var serviceResponse = new ServiceResponse<EntryResponseDto>();
            var entry = _mapper.Map<Entry>(newEntry);
            // entry.Id = entries.Max(c => c.Id) + 1;
            entry.Id = Guid.NewGuid();
            entries.Add(entry);
            serviceResponse.Data = _mapper.Map<EntryResponseDto>(entry);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EntryResponseDto>>> DeleteEntry(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<EntryResponseDto>>();
            try
            {
                var entry = entries.FirstOrDefault(e => e.Id == id);
                if (entry is null)
                {
                    throw new Exception($"Entry with id {id} not found");
                }
                entries.Remove(entry);
                serviceResponse.Data = entries
                    .Select(e => _mapper.Map<EntryResponseDto>(e))
                    .ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EntryResponseDto>>> GetAllEntries()
        {
            var serviceResponse = new ServiceResponse<List<EntryResponseDto>>();
            serviceResponse.Data = entries.Select(e => _mapper.Map<EntryResponseDto>(e)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<EntryResponseDto>> GetEntryById(Guid id)
        {
            var serviceResponse = new ServiceResponse<EntryResponseDto>();

            try
            {
                var entry = entries.FirstOrDefault(e => e.Id == id);
                if (entry is null)
                {
                    throw new Exception($"Entry with id {id} not found");
                }
                serviceResponse.Data = _mapper.Map<EntryResponseDto>(entry);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<EntryResponseDto>> UpdateEntry(
            EntryUpdateRequestDto updatedEntry
        )
        {
            var serviceResponse = new ServiceResponse<EntryResponseDto>();
            try
            {
                var entry = entries.FirstOrDefault(e => e.Id == updatedEntry.Id);
                if (entry is null)
                {
                    throw new Exception($"Entry with id {updatedEntry.Id} not found");
                }
                _mapper.Map(updatedEntry, entry);
                serviceResponse.Data = _mapper.Map<EntryResponseDto>(entry);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
