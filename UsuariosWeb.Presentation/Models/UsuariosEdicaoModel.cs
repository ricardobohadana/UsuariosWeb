using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UsuariosWeb.Presentation.Models
{
    public class UsuariosEdicaoModel
    {
        // campo oculto
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string IdPerfil { get; set; }


        #region Campo de seleção do tipo DropDown
        
        public List<SelectListItem>? ListagemPerfis { get; set; }

        #endregion
    }
}
