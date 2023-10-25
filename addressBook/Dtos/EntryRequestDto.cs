using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models;

namespace addressBook.Dtos
{
    public class EntryRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
