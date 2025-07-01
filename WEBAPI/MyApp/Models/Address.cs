using System;

namespace MyApp.Models;

public class Address
{
    public int Id { get; set; }
    public string City { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }
}
