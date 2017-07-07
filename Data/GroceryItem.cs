using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace clean_aspnet_mvc.Data
{
    public class GroceryItem : BaseEntityChildOfLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        public string GroceryItemName { get; set; }


        public GroceryCategory GroceryCategory { get; set; }

        [ForeignKey("GroceryCategory")]
        [UIHint("GroceryCategoryId")]
        public int GroceryCategoryId { get; set; }

    }

}
