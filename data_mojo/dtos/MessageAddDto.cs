using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data_mojo.dtos
{
    public class MessageAddDto
    {
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }
        public int UserId { get; set; }
        public int DiscussionId { get; set; }
    }
}