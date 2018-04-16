using System.ComponentModel.DataAnnotations;

namespace ReTwitter.Web.Models.SearchViewModels
{
    public class SearchViewModel
    {
        [Required]
        [MinLength(1)]
        [DataType(DataType.Text)]
        public string SearchInput { get; set; }
    }
}
