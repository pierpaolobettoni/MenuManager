using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using clean_aspnet_mvc.Data;
public class GroceryCategory : BaseEntityChildOfLocation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    [Required]
    [Display(Name = "Grocery Category")]
    public string GroceryCategoryName { get; set; }

    [Display(Name = "Description")]

    public string GroceryCategoryDescription { get; set; }



}


