using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dto
{
    public record CategoryDto(string name,string Description);
    public record UpdateCategoryDto(string name, string Description,int id);



}
