using System.ComponentModel.DataAnnotations;

public class Currency
{
    [Display(Name = "شناسه")]
    public int Id { get; set; }

    [Display(Name = "نام")]
    public string Name { get; set; }
    public string? Code { get; set; }

}
