using System.ComponentModel.DataAnnotations;

namespace Kaizen.Sample.Web.Client.Models;

public record WorkItem(string Code, string Description, int Quantity, decimal Price, string GroupName)
{
    [Required]
    public string Code { get; set; } = Code;
    public string Description { get; set; } = Description;

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; } = Quantity;
    public decimal Price { get; set; } = Price;
    public string GroupName { get; set; } = GroupName;

    public Guid Id { get; } = Guid.NewGuid();
}
