using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class BinaryMaterial
{
    public int MaterialId { get; set; }

    public byte[] Content { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;
}
