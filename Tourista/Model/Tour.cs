using System;
using System.Collections.Generic;

namespace Tourista;

public partial class Tour
{
    public int TourId { get; set; }

    public string TourName { get; set; } = null!;

    public string TourDescription { get; set; } = null!;

    public decimal TourPrice { get; set; }

    public int CityId { get; set; }

    public int? TourMaxParticipants { get; set; }

    public int? TourCurrentParticipants { get; set; }

    public bool? TourIsActive { get; set; }

    public DateOnly TourStart { get; set; }

    public DateOnly TourEnd { get; set; }

    public string? TourImage { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual City City { get; set; } = null!;

    public string DisplayedImage => (string)("../Resources/" + ((TourImage == null || string.IsNullOrEmpty(TourImage)) ? "picture.png" : TourImage));

}
