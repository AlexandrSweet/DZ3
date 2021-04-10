using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class HiringHistories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string EmployeesId { get; set; }
        public ICollection<Achievements> Achievements { get; set; }

    }
}
