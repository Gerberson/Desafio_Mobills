using ContasAPagar.Domain.Api.DataContext;
using ContasAPagar.Domain.Api.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;

namespace ContasAPagar.Domain.Api.Controllers
{
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        private Context _context = new Context();

        [HttpGet]
        [Route("/receitas")]
        public IEnumerable Get()
        {
            try
            {
                return _context.Connection
                    .Query("SELECT * FROM Receitas");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Route("/receitas/{id}")]
        public IEnumerable GetById(int id)
        {
            try
            {
                IEnumerable receita = _context.Connection
                    .QueryFirstOrDefault("SELECT * FROM Receitas WHERE IdReceita = @Id", new { Id = id });

                if (receita == null)
                    return "Receita não cadastrada.";

                return receita;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost]
        [Route("/receitas")]
        public IEnumerable Post([FromBody] Receita receita)
        {
            try
            {
                if (receita.Valid)
                {
                    _context.Connection
                        .Execute(
                        @"
                            INSERT INTO Receitas(Descricao, Valor, Data, Recebido) 
                            VALUES(@descricao, @valor, @data, @recebido)
                        ",
                        new
                        {
                            descricao = receita.Descricao,
                            valor = receita.Valor,
                            data = receita.Data,
                            recebido = receita.Recebido
                        });

                    return _context.Connection.Query("SELECT TOP 1 * FROM Receitas ORDER BY IdReceita DESC");
                }
                else
                {
                    return receita.Notifications;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPut]
        [Route("/receitas/{id}")]
        public IEnumerable Put([FromBody] Receita receita, int id)
        {
            try
            {
                if (receita.Valid)
                {
                    if (receita.Valid)
                    {
                        IEnumerable Receita = _context.Connection
                        .QueryFirstOrDefault("SELECT * FROM Receitas WHERE IdReceita = @Id", new { Id = id });

                        if (Receita == null)
                            return "Receita não cadastrada.";

                        _context.Connection.Execute(
                            @"
                                UPDATE Receitas 
                                SET Descricao = @descricao, Valor = @valor, Data = @data, Recebido = @recebido
                                Where IdReceita = @Id
                            ",
                            new
                            {
                                Id = id,
                                descricao = receita.Descricao,
                                valor = receita.Valor,
                                data = receita.Data,
                                recebido = receita.Recebido
                            });

                        return "OK";
                    }
                    else
                    {
                        return receita.Notifications;
                    }
                }
                else
                {
                    return receita.Notifications;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpDelete]
        [Route("/receitas/{id}")]
        public IEnumerable Delete(int id)
        {
            try
            {
                IEnumerable receita = _context.Connection
                    .QueryFirstOrDefault("SELECT * FROM Receitas WHERE IdReceita = @Id", new { Id = id });

                if (receita == null)
                    return "Receita não cadastrada.";

                _context.Connection.Execute("DELETE FROM Receitas Where IdReceita = @Id", new { Id = id });
                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Route("/receitas/recebidas")]
        public IEnumerable GetReceitasPagas()
        {
            try
            {
                return _context.Connection
                    .Query(@"
                        SELECT * ,
                            (SELECT Sum(RE.Valor) FROM Receitas RE WHERE RE.Recebido = 1) as Total 
                        FROM Receitas R
                        WHERE R.Recebido = 1
                    ");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Route("/receitas/pendentes")]
        public IEnumerable GetReceitasPendentes()
        {
            try
            {
                return _context.Connection
                    .Query(@"
                        SELECT * ,
                            (SELECT Sum(RE.Valor) FROM Receitas RE WHERE RE.Recebido = 0) as Total 
                        FROM Receitas R
                        WHERE R.Recebido = 0
                    ");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
