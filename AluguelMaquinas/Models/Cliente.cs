using System.ComponentModel.DataAnnotations;

namespace AluguelMaquinas.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        [Display(Name = "Endereço de Cobrança")]
        public string EnderecoCobranca { get; set; }
        [Display(Name = "CEP")]
        public string CepCobranca { get; set; }
        [Display(Name = "Município")]
        public string MunicipioCobranca { get; set; }
        public ICollection<Aluguel> Alugueis { get; set; }
    }
}
