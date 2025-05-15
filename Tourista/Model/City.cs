using System;
using System.Collections.Generic;

namespace Tourista;

public partial class City
{
    public int CityId { get; set; }

    public string RegionName { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
