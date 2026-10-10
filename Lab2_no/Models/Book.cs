using System;
using System.Collections.Generic;

namespace Lab2_no.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int PublishedYear { get; set; }

    public int FkGenreId { get; set; }

    public virtual Genre FkGenre { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<Author> FkAuthors { get; set; } = new List<Author>();
}
