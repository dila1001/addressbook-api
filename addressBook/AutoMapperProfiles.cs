using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Dtos;
using addressBook.Models;
using AutoMapper;

namespace addressBook
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Entry, EntryResponseDto>();
            CreateMap<EntryRequestDto, Entry>();
            CreateMap<EntryUpdateRequestDto, Entry>();
        }
    }
}
