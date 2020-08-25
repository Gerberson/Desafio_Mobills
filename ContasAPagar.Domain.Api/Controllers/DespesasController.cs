using ContasAPagar.Domain.Api.DataContext;
using ContasAPagar.Domain.Api.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;

namespace ContasAPagar.Domain.Api.Controllers
{
    public class DespesasController
    {
        private Context _context = new Context();

        [HttpGet]
        [Route("/despesas")]
        public IEnumerable Get()
        {
            try
            {
                return _context.Connection
                    .Query("SELECT * FROM Despesas");
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        [HttpGet]
        [Route("/despesas/{id}")]
        public IEnumerable GetById(int id)
        {
            try
            {
                IEnumerable despesa = _context.Connection
                    .QueryFirstOrDefault("SELECT * FROM Despesas WHERE IdDespesa = @Id", new { Id = id });

                if (despesa == null)
                    return "Despesa não cadastrada.";

                return despesa;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost]
        [Route("/despesas")]
        public IEnumerable PostDespesas([FromBody] Despesa despesa)
        {
            try
            {
                if (despesa.Valid)
                {
                    _context.Connection.Execute(
                    @"
                        INSERT INTO Despesas(Descricao, Valor, Data, Pago) 
                        VALUES(@descricao, @valor, @data, @pago)
                    ",
                    new
                    {
                        descricao = despesa.Descricao,
                        valor = despesa.Valor,
                        data = despesa.Data,
                        pago = despesa.Pago
                    });

                    return _context.Connection.Query("SELECT TOP 1 * FROM Despesas ORDER BY IdDespesa DESC");
                }
                else
                {
                    return despesa.Notifications;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPut]
        [Route("/despesas/{id}")]
        public IEnumerable Put([FromBody] Despesa despesa, int id)
        {
            try
            {
                if (despesa.Valid)
                {
                    IEnumerable Despesa = _context.Connection
                    .QueryFirstOrDefault("SELECT * FROM Despesas WHERE IdDespesa = @Id", new { Id = id });

                    if (Despesa == null)
                        return "Despesa não cadastrada.";

                    _context.Connection.Execute(
                        @"
                        UPDATE Despesas 
                        SET Descricao = @descricao, Valor = @valor, Data = @data, Pago = @pago
                        Where IdDespesa = @Id
                    ",
                        new
                        {
                            Id = id,
                            descricao = despesa.Descricao,
                            valor = despesa.Valor,
                            data = despesa.Data,
                            pago = despesa.Pago
                        });

                    return "OK";
                }
                else
                {
                    return despesa.Notifications;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpDelete]
        [Route("/despesas/{id}")]
        public IEnumerable Delete(int id)
        {
            try
            {
                IEnumerable despesa = _context.Connection
                    .QueryFirstOrDefault("SELECT * FROM Despesas WHERE IdDespesa = @Id", new { Id = id });

                if (despesa == null)
                    return "Despesa não cadastrada.";

                _context.Connection.Execute("DELETE FROM Despesas Where IdDespesa = @Id", new { Id = id });
                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Route("/despesas/pagas")]
        public IEnumerable GetReceitasPagas()
        {
            try
            {
                return _context.Connection
                    .Query(@"
                        SELECT * ,
                            (SELECT Sum(DE.Valor) FROM Despesas DE WHERE DE.Pago = 1) as Total 
                        FROM Despesas D
                        WHERE D.Pago = 1
                    ");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Route("/despesas/pendentes")]
        public IEnumerable GetReceitasPendentes()
        {
            try
            {
                return _context.Connection
                    .Query(@"
                        SELECT * ,
                            (SELECT Sum(DE.Valor) FROM Despesas DE WHERE DE.Pago = 0) as Total 
                        FROM Despesas D
                        WHERE D.Pago = 0
                    ");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
