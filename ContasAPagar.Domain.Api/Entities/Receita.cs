using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace ContasAPagar.Domain.Api.Entities
{
    public class Receita : Notifiable, IValidatable
    {
        public int IdReceita { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime Data { get; private set; }
        public bool Recebido { get; private set; }



        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsFalse(Recebido, "Recebido", "Por padrão o valor de recebido é falso")
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