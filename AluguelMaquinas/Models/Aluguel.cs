using System.ComponentModel.DataAnnotations;

namespace AluguelMaquinas.Models
{
    public class Aluguel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Endereço do Local de Uso")]
        public string EnderecoAluguel { get; set; }
        [Display(Name = "CEP")]
        public string CepAluguel { get; set; }
        [Display(Name = "Município")]
        public string MunicipioAluguel { get; set; }
        [Display(Name = "Data do Aluguel")]
        [DataType(DataType.Date)]
        public DateTime DataAluguel { get; set; }
        [Display(Name = "Dias de Aluguel (Qtde)")]
        public int DiasAluguel { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<AluguelEquipamento> AluguelEquipamentos { get; set; }
    }
}
