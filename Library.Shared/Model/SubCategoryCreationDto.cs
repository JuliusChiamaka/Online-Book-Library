using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Shared.Model
{
    public class SubCategoryCreationDto
    {
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
    }
}
