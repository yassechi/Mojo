using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_mojo.dtos
{
    public class AccessoireUpdateDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }

    }
}