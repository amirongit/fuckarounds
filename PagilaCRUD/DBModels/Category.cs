using System;
using System.Collections.Generic;

namespace PagilaCRUD.DBModels;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();
}
