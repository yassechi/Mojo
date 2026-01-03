using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_mojo.data.models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }
    }
}