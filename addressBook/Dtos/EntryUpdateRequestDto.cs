using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models;

namespace addressBook.Dtos
{
    public class EntryUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
