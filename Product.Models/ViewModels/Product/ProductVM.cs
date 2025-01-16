using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Models.ViewModels.Product
{
    public class ProductVM
    {
        public ProductFormModel Form { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; } = [];
    }
}
