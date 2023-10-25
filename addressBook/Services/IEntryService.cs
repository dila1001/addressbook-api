using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Dtos;
using addressBook.Models;

namespace addressBook.Services
{
    public interface IEntryService
    {
        Task<ServiceResponse<List<EntryResponseDto>>> GetAllEntries();
        Task<ServiceResponse<EntryResponseDto>> GetEntryById(Guid id);
        Task<ServiceResponse<EntryResponseDto>> CreateNewEntry(EntryRequestDto newEntry);
        Task<ServiceResponse<EntryResponseDto>> UpdateEntry(EntryUpdateRequestDto updatedEntry);
        Task<ServiceResponse<List<EntryResponseDto>>> DeleteEntry(Guid id);
    }
}
