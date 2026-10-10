using System;
using System.Collections.Generic;

namespace Lab2_no.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public virtual ICollection<Book> FkBooks { get; set; } = new List<Book>();
}
