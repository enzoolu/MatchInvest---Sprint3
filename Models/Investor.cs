namespace MatchInvest.Models
{
    public class Investor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal CapitalDisponivel { get; set; }
        public string ApetiteRisco { get; set; } // Pode ser um Enum
        public string Objetivos { get; set; }
        public string Preferencias { get; set; }
    }
}