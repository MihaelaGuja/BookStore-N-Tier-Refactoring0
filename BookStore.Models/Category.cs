using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Models
{
    public class Category
    {
        //[Key] //-> not requierd , knows that is pk automaticaly
        public int Id { get; set; } // table primary key //IdCategory

        [Required] // -> sets not null
        [MaxLength(30)] // maximum length of the Name
        public string Name { get; set; }

        [DisplayName("Display Order")] // to display this name inside the Create view and keep asp-for="" inside the label
        [Range(1,100,ErrorMessage = "Display order must be betwen 1-100")] // min and mx range input for the fild // ErrorMessage = diplays message that devloper wants instead of default message
        public int DisplayOrder { get; set; }
    }
}
// 