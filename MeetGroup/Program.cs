using MeetGroup.Domain.Models;
using MeetGroup.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetGroup
{
    class Program
    {
        static void Main(string[] args)
        {

            // Cria json de salas
            Sala.CreateSalas();

            Console.WriteLine("Bem Vindo!");

            var option = 1;
            while (option != 0)
            {
                
                Console.WriteLine("\nEscolha uma opção:\n");
                Console.WriteLine("1 - Reservar sala");
                Console.WriteLine("2 - Listar salas");
                Console.WriteLine("3 - Listar reservas");
                Console.WriteLine("0 - Sair");
                option = Convert.ToInt32(Console.ReadLine());

                if(option == 1)
                {
                    Reservar();
                }
                else if(option == 2)
                {
                    Salas();
                }
                else if(option == 3)
                {
                    Reservas();
                }
            }

            Console.WriteLine("\nAté logo\n");

        }

        private static void Reservar()
        {
            // Inicia atendimento
            Atendimento atendimento = Atendimento.InitAtendimento();
            Agendamento item = new Agendamento(0, atendimento.DataInicio, atendimento.DataFim);

            if (item.Validate())
            {
                // Encontrar salas compativeis
                List<Sala> salas = Sala.Find(atendimento.Lotacao, atendimento.Internet, atendimento.Tv, atendimento.Webcam);
                Sala sala = Agendamento.CheckIsAvaible(salas, atendimento.DataInicio, atendimento.DataFim);
                if (sala != null)
                {
                    Agendamento agendamento = new Agendamento(sala.Id, atendimento.DataInicio, atendimento.DataFim);
                    agendamento.Schedule();
                    Console.WriteLine("Agendamento confirmado: " + sala.Nome);
                }
                else
                {
                    Agendamento.Options(salas, atendimento.DataInicio, atendimento.DataFim);
                }
            }
            else
            {
                Console.WriteLine("Tente novamente");
            }
        }

        private static void Salas()
        {
            Console.WriteLine("\nSalas cadastradas\n");
            List<Sala> salas = Sala.Get();

            for (int i = 0; i < salas.Count; i++)
            {
                Sala sala = salas[i];
                Console.WriteLine(sala.Nome + "  " + sala.Lotacao + "   " + sala.Computador + " " + sala.Internet + " " + sala.Tv + " " +  sala.WebCam);
            }

        }

        private static void Reservas()
        {
            Console.WriteLine("\nReservas cadastradas\n");

            List<Agendamento> agendamentos = AgendamentoRepository.Get();
            List<Sala> salas = SalaRepository.Get();
            var resultList = agendamentos.Join(salas, a => a.IdSala, s => s.Id, (a, s) => s);

            for (int i = 0; i < agendamentos.Count; i++)
            {
                Agendamento agendado = agendamentos[i];    
                Console.WriteLine(agendado.Id + " - " + resultList.ToList()[i].Nome + " - " + agendado.DataInicio.ToString("dd/MM/yyyy - HH:mm") + " até " + agendado.DataFim.ToString("dd/MM/yyyy - HH:mm"));
            }
        }
    }
}
