using System.ComponentModel.DataAnnotations;

namespace AluguelMaquinas.Models
{
    public class Equipamento
    {
        [Key]
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Data de Aquisição")]
        [DataType(DataType.Date)]
        public DateTime DataAquisicao { get; set; }
        public string Estado { get; set; }
        [Display(Name = "Valor por Dia")]
        public decimal ValorDia { get; set; }
        public ICollection<AluguelEquipamento> AluguelEquipamentos { get; set; }
    }
}
