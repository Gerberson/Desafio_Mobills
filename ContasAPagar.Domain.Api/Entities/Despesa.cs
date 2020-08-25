using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace ContasAPagar.Domain.Api.Entities
{
    public class Despesa : Notifiable, IValidatable
    {
        public int IdDespesa { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime Data { get; private set; }
        public bool Pago { get; private set; }

        public Despesa(string descricao, decimal valor, DateTime data)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;

            Validate();
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsFalse(Pago, "Recebido", "Por padrão o valor de recebido é falso")
                .HasMinLen(Descricao, 3, "Descricao", "A descrição deve ser maior que três caracteres.")
                .IsGreaterThan(Valor, 0, "Valor", "O valor deve ser maior que 0.")
            );
        }

        public override string ToString()
        {
            return $"{Descricao} - {Valor}";
        }
    }
}
