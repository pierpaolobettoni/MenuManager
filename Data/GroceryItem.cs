using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using clean_aspnet_mvc.Data;
public class GroceryItem: BaseEntityChildOfLocation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    [Required]
    public string GroceryItemName { get; set; }



}


