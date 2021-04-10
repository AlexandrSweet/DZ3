using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class HiringHistoriesDto
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string EmployeesId { get; set; }
        public List<AchievementsDto> AchievementsDto { get; set; }

    }
}
