namespace addressBook.Tests;

using addressBook.Dtos;
using addressBook.Models;
using addressBook.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

public class EntryServiceTests
{
    private readonly EntryService _entryService;

    public EntryServiceTests()
    {
        _entryService = new EntryService(
            new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()))
        );
    }

    [Fact]
    public async Task CreateEntryService()
    {
        var newEntry = new EntryRequestDto
        {
            Name = "Adila",
            Address = new Address()
            {
                Street = "Geodesivägen 11B",
                City = "Huddinge",
                PostalCode = 14137
            }
        };

        var result = await _entryService.CreateNewEntry(newEntry);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.IsType<EntryResponseDto>(result.Data);
    }

    [Fact]
    public async Task GetEntriesService()
    {
        var result = await _entryService.GetAllEntries();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.IsType<List<EntryResponseDto>>(result.Data);
        Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public async Task GetEntryByIdService()
    {
        Guid id = new Guid("26e40c03-1adc-4010-bdbd-306acfb7f05f");
        var expected = new ServiceResponse<EntryResponseDto>()
        {
            Data = new EntryResponseDto // Initialize the Data property
            {
                Id = new Guid("26e40c03-1adc-4010-bdbd-306acfb7f05f"),
                Name = "Adila",
                Address = new Address()
                {
                    Street = "Geodesivägen 11B",
                    City = "Huddinge",
                    PostalCode = 14137
                }
            },
            Success = true,
            Message = ""
        };
        var result = await _entryService.GetEntryById(id);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.IsType<EntryResponseDto>(result.Data);
        Assert.Equal(expected.Data.Id, result.Data.Id);
    }
}
