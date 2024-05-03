using System;
using System.Collections.Generic;

namespace AnimalAPI.Database;

public partial class Animal
{
    public int Id { get; set; }

    public string AnimalName { get; set; } = null!;

    public string? Url { get; set; }

    public string? VideoUrl { get; set; }
}
