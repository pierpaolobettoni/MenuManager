using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using clean_aspnet_mvc.Data;

public class MealItem : BaseEntityChildOfLocation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    [Required]
    public string MealItemName { get; set; }

    public string MealItemDescription { get; set; }

    [Required]
    public int Quantity { get; set; }

    public string MeasureType { get; set; }


}


