using System;
using System.Collections.Generic;

namespace Lab2_no.Models;

public partial class Loan
{
    public int Id { get; set; }

    public int FkUserId { get; set; }

    public int FkBookId { get; set; }

    public DateOnly DateLoans { get; set; }

    public int Rating { get; set; }

    public virtual Book FkBook { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;
}
