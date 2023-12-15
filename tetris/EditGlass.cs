using System.ComponentModel.DataAnnotations;

namespace tetris
{
	public class EditGlass
	{
        [Required]
        public string Height { get; set; }

        [Required]
        public string Width { get; set; }

    }
}
