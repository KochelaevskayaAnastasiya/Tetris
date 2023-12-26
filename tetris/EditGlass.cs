using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using tetris.Add_classes;

namespace tetris
{
	public class EditGlass
	{
        [Required (ErrorMessage ="Необходимо заполнить поле \"Высота\"")]
        [Range(20,40, ErrorMessage = "Значение поля \"Высота\" должно находиться в диапозоне от {1} до {2}")]
        public string Height { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле \"Ширина\"")]
        [Range(10, 20, ErrorMessage = "Значение поля \"Ширина\" должно находиться в диапозоне от {1} до {2}")]
        public string Width { get; set; }

        public string Id { get; set; }

        [MyAnnotationAttribute]
        public string HW { get; set; }
        public int err { get; set; }

    }
}
