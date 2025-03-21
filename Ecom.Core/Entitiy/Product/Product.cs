using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entitiy.Product
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public virtual List<Photo> photos { get; set; }
        public int CategoryId { get; set; }

        // why we added virtual ?  to lazy loading and overide 
        // lazy loading "مش بيتم تحميل الكاتيجورى من الداتا بيز الا لما بيكون عليها طلب   "
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
