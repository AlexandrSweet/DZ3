using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class EmployeesDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<HiringHistoriesDto> HiringHistoriesDto { get; set; }
    }
}
