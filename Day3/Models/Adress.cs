using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;

namespace Day3.Models;

public class Adress
{
    [Key]
    public int AdressId { get; set; }
    public string City { get; set; }

    public string Zip { get; set; }
    public string Street { get; set; }

    public int HouseNumber { get; set; }

}
