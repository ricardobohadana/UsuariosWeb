namespace UsuariosWeb.Presentation.Models
{

    /// <summary>
    /// Classe de modelo de dados das informa~ções que serão exibidas na página Views/Usuarios/MinhaConta.cshtml
    /// </summary>
    public class UsuariosMinhaContaModel
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime DataCadastro { get; set; }

        public string Perfil { get; set; }
    }
}
