using System;
using System.Collections.Generic;

namespace PagilaCRUD.DBModels;

public partial class ActorInfo
{
    public int? ActorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FilmInfo { get; set; }
}
