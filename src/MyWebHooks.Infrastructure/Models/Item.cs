using System.ComponentModel.DataAnnotations;

namespace MyWebHooks.Infrastructure.Models;

public class Item
{
    public string? Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public int Quantity { get; set; }
}