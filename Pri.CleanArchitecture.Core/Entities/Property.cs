﻿namespace Pri.CleanArchitecture.Core.Entities
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}