using System;
using System.Collections.Generic;

namespace ProgTerms.AppData;

public partial class Term
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Definition { get; set; } = null!;

    public string? AddInformation { get; set; }

    public int IsSave { get; set; }
}
