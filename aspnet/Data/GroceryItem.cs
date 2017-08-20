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
        [Display(Name = "Grocery Item")]
        public string GroceryItemName { get; set; }

        [Display(Name = "Grocery Category")]
        public GroceryCategory GroceryCategory { get; set; }

        [ForeignKey("GroceryCategory")]
        [UIHint("GroceryCategoryId")]
        [Display(Name = "Grocery Category")]
        public int GroceryCategoryId { get; set; }

        [NotMapped]
        public string NameWithCategoryAtTheEnd {
            get {
                if (this.GroceryCategory != null)
                {
                    return this.GroceryItemName + "  - " + GroceryCategory.GroceryCategoryName.ToLower();
                }
                else
                {
                    return this.GroceryItemName + " - category not loaded";
                }
            }
        }

    }

}
