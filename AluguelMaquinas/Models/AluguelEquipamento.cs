using System.ComponentModel.DataAnnotations;

namespace AluguelMaquinas.Models
{
    public class AluguelEquipamento
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Aluguel")]
        public int AluguelId { get; set; }
        [Display(Name = "Aluguel")]
        public Aluguel Aluguel { get; set; }
        [Display(Name = "Máquina Id")]
        public int EquipamentoId { get; set; }
        [Display(Name = "Máquina")]
        public Equipamento Equipamento { get; set; }
        [Display(Name = "Valor por Dia")]
        public decimal ValorDia { get; set; }
    }
}
