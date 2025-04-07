﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entitiy.Product;
using Microsoft.AspNetCore.Http;

namespace Ecom.Core.Dto
{
    public record ProductDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public virtual List<PhotoDto> photos { get; set; }
        public string CategoryName { get; set; }
    }
    public record PhotoDto
    {
        public string ImageName { get; set; }
        public int CategoryId { get; set; }
    }

    public record AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }

        public int CategoryId { get; set; }
        public IFormFileCollection Photo { get; set; }

    }
    public record UpdateProductDto:AddProductDto
    {
        public int Id { get; set; }
    }
        
}
