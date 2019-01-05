using MeetGroup.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MeetGroup.Infrastructure
{
    public class SalaRepository
    {
        public static void Create()
        {
            try
            {
                List<Sala> lista = new List<Sala>();

                for (int i = 1; i <= 12; i++)
                {
                    if (i < 6)
                    {
                        Sala sala = new Sala(i, "Sala " + i, 10, true, true, true, true);
                        lista.Add(sala);
                    }
                    else if (i > 5 && i < 8)
                    {
                        Sala sala = new Sala(i, "Sala " + i, 10, false, true, false, false);
                        lista.Add(sala);
                    }
                    else if (i > 7 && i < 11)
                    {
                        Sala sala = new Sala(i, "Sala " + i, 3, true, true, true, true);
                        lista.Add(sala);
                    }
                    else if (i >= 11 && i < 13)
                    {
                        Sala sala = new Sala(i, "Sala " + i, 20, false, false, false, false);
                        lista.Add(sala);
                    }
                }

                string jsonResult = JsonConvert.SerializeObject(lista);
                string path = System.IO.Directory.GetCurrentDirectory() + @"\salas.json";
                File.WriteAllText(path, jsonResult);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Sala> Get()
        {
            try
            {
                List<Sala> salas = new List<Sala>();
                string path = System.IO.Directory.GetCurrentDirectory() + @"\salas.json";
                salas = JsonConvert.DeserializeObject<List<Sala>>(File.ReadAllText(path));

                return salas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
