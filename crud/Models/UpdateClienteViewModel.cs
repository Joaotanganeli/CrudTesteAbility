namespace crud.Models
{
    public class UpdateClienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string cpf { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
