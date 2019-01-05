using MeetGroup.Infrastructure;
using System.Collections.Generic;

namespace MeetGroup.Domain.Models
{
    public class Sala
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Lotacao { get; private set; }
        public bool Computador { get; private set; }
        public bool Internet { get; private set; }
        public bool Tv { get; private set; }
        public bool WebCam { get; private set; }


        public Sala(int id, string nome, int lotacao, bool computador, bool internet, bool tv, bool webcam)
        {
            Id = id;
            Nome = nome;
            Lotacao = lotacao;
            Computador = computador;
            Internet = internet;
            Tv = tv;
            WebCam = webcam;
        }


        public static void CreateSalas()
        {
            SalaRepository.Create();
            
        }

        public static List<Sala> Get()
        {
            return SalaRepository.Get();

        }

        public static List<Sala> Find(int lotacao, bool internet, bool tv, bool webcam)
        {
            List<Sala> salas = SalaRepository.Get();
            List<Sala> salasCompativel = new List<Sala>();

            for (int i = 0; i < salas.Count; i++)
            {
                Sala sala = salas[i];
                if (sala.Lotacao == lotacao && sala.Internet == internet && sala.Tv == tv && sala.WebCam == webcam)
                    salasCompativel.Add(sala);
            }

            return salasCompativel;
        }

    }
}

