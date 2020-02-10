using System;
using System.Collections.Generic;
namespace asp.net_simple_onlineshop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }

    }
}
