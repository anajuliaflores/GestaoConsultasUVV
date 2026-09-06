using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoConsultasUVV.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(100)]
        public string Especialidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;

        // Relacionamento: cada consulta pertence a um usuário
        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}