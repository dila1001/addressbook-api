namespace addressBook.Tests;

using addressBook.Dtos;
using addressBook.Models;
using addressBook.Services;
using AutoMapper;

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
            Name = "Kamelia",
            Address = new Address()
            {
                Street = "Sötvägen 22",
                City = "Cute City",
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
        // Assert.Equal(2, result.Data.Count);        Look at note below
    }

    [Theory]
    [InlineData("26e40c03-1adc-4010-bdbd-306acfb7f05f", true)]
    [InlineData("26e40c03-1adc-4010-bdbd-306acfb7f0ff", false)]
    public async Task GetEntryByIdService(string idString, bool shouldSucceed)
    {
        Guid id = Guid.Parse(idString);
        var expected = new ServiceResponse<EntryResponseDto>()
        {
            Data = new EntryResponseDto
            {
                Id = id,
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

        if (shouldSucceed)
        {
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.IsType<EntryResponseDto>(result.Data);
            Assert.Equal(expected.Data.Id, result.Data.Id); // Look at deep equality comparison with FluentAssertions
        }
        else
        {
            Assert.False(result.Success);
            Assert.Equal($"Entry with id {id} not found", result.Message);
        }
    }

    [Theory]
    [InlineData("26e40c03-1adc-4010-bdbd-306acfb7f05f", true)]
    [InlineData("26e40c03-1adc-4010-bdbd-306acfb7f0ff", false)]
    public async Task DeleteEntry(string idString, bool shouldSucceed)
    {
        Guid id = Guid.Parse(idString);
        var result = await _entryService.DeleteEntry(id);

        if (shouldSucceed)
        {
            Assert.True(result.Success);
            Assert.Equal(1, result.Data.Count);
        }
        else
        {
            Assert.False(result.Success);
            Assert.Equal($"Entry with id {id} not found", result.Message);
        }
    }
}

/* Notes */

/*

All good unit tests should be 100% isolated.
Using shared state (e.g. depending on a static property
that is modified by each test) is regarded as bad practice.

In this code, all tests depend on the shared state of the static field
'entries' in the service. This results in unpredictable results.

In order to make this work, we can instantiate EntryService at every test and
populate the 'entries' instance field in the constructor of EntryService. we would then need
to add EntryService as singleton instead of scoped so that 'entries' are presisted between
requests.

However since this is not really a real implementation and only for learning purposes,
I will leave it like this.

*/
