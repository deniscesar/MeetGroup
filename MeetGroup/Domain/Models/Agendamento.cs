using MeetGroup.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGroup.Domain.Models
{
    public class Agendamento
    {

        public int Id { get; set; }
        public int IdSala { get; private set; }
        public DateTime Data { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }

        public Agendamento(int idSala, DateTime dataInicio, DateTime dataFim)
        {
            IdSala = idSala;
            Data = DateTime.UtcNow.AddHours(-2);
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        public bool Validate()
        {
            DateTime hoje = DateTime.UtcNow.AddHours(-2);

            if (DataInicio.Date == hoje.Date)
            {
                Console.WriteLine("\nErro: As reuniões devem ser agendadas com no mínimo um dia de antecedência");
                return false;
            }

            if (DataInicio.Subtract(hoje).Days > 40)
            {
                Console.WriteLine("\nErro: As reuniões devem ser agendadas com no máximo 40 dia de antecedência");
                return false;
            }

            var dia = (int)DataInicio.DayOfWeek;
            if (dia == 0 || dia == 6)
            {
                Console.WriteLine("\nErro: As reuniões devem ser agendadas apenas para os dias úteis");
                return false;
            }

            if (DataFim.Subtract(DataInicio).TotalHours > 8)
            {
                Console.WriteLine("\nErro: Reuniões não podem durar mais que 8 horas");
                return false;
            }

            if (DataFim.Subtract(DataInicio).Hours < 1)
            {
                Console.WriteLine("\nERRO: Data final das reuniões não podem ser menor que a data incial");
                return false;
            }

            return true;
        }

        public static Sala CheckIsAvaible(List<Sala> salas, DateTime dataInicio, DateTime dataFim)
        {

            for (int i = 0; i < salas.Count; i++)
            {
                Sala sala = salas[i];
                List<Agendamento> agendamentos = AgendamentoRepository.GetBySala(sala.Id);
                if (agendamentos.Count == 0)
                    return sala;

                bool isAgendado = false;
                for (int j = 0; j < agendamentos.Count; j++)
                {
                    Agendamento agendado = agendamentos[j];
                    if(agendado.DataInicio.Date == dataInicio.Date && agendado.DataFim.Date == dataFim.Date)
                    {
                        if (agendado.DataInicio > dataInicio && agendado.DataFim > dataFim || agendado.DataInicio < dataInicio && agendado.DataFim < dataFim)
                        {
                            isAgendado = false;
                        }
                        else
                        {
                            isAgendado = true;
                        }

                    }   
                }

                if (!isAgendado)
                    return sala;

            }

            return null;
        }

        public Agendamento Schedule()
        {
            try
            {
                return AgendamentoRepository.Create(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static void Options(List<Sala> salas, DateTime dataInicio, DateTime dataFim)
        {
            Console.WriteLine("\nSalas indisponivel nesta data veja algumas sugestões \n");
            List<Sala> itens = new List<Sala>();
            List<Agendamento> agendamentos = new List<Agendamento>();
            int i = 1;
            while (itens.Count < 3)
            {
                Sala sala = CheckIsAvaible(salas, dataInicio.AddDays(i), dataFim.AddDays(i));
                if (itens != null)
                {
                    Agendamento agendamento = new Agendamento(sala.Id, dataInicio.AddDays(i), dataFim.AddDays(i));
                    agendamentos.Add(agendamento);
                    itens.Add(sala);
                    i++;
                }
            }

            for (int j = 0; j < itens.Count; j++)
            {
                Sala item = itens[j];
                Agendamento agendamento = agendamentos[j];
                Console.WriteLine(j+1 + " - " + item.Nome + " disponivel " + agendamento.DataInicio + " a " + agendamento.DataFim);
            }
            Console.WriteLine("0 - Nenhuma");
            Console.WriteLine("Qual opção você prefere?");
            var option = Convert.ToInt32(Console.ReadLine());
            if(option != 0)
            {
                Agendamento escolhido = agendamentos[option - 1];
                escolhido.Schedule();
                Console.WriteLine("\nReserva confirmada: " + itens[option - 1].Nome);
            }
            
        }
    }
}
