using System;
using System.Collections.Generic;

namespace Tourista;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int TourId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? BookingStatus { get; set; }

    public decimal BookingTotalPrice { get; set; }

    public int? BookingParticipants { get; set; }

    public virtual Tour Tour { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
