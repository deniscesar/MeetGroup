using MeetGroup.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MeetGroup.Infrastructure
{
    public class AgendamentoRepository
    {
        
        public static List<Agendamento> Get()
        {
            try
            {
                List<Agendamento> agendamentos = new List<Agendamento>();
                string path = System.IO.Directory.GetCurrentDirectory() + @"\agendamentos.json";

                if (File.Exists(path))
                {
                    agendamentos = JsonConvert.DeserializeObject<List<Agendamento>>(File.ReadAllText(path));
                }

                return agendamentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static Agendamento Get(int id)
        {
            try
            {
                List<Agendamento> agendamentos = new List<Agendamento>();
                string path = System.IO.Directory.GetCurrentDirectory() + @"\agendamentos.json";

                if (File.Exists(path))
                {
                    agendamentos = JsonConvert.DeserializeObject<List<Agendamento>>(File.ReadAllText(path));
                }

                for (int i = 0; i < agendamentos.Count; i++)
                {
                    Agendamento agendamento = agendamentos[i];
                    if (agendamento.Id == id)
                        return agendamento;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Agendamento> GetBySala(int idSala)
        {
            try
            {
                List<Agendamento> agendamentos = new List<Agendamento>();
                List<Agendamento> list = new List<Agendamento>();
                string path = System.IO.Directory.GetCurrentDirectory() + @"\agendamentos.json";

                if (File.Exists(path))
                {
                    agendamentos = JsonConvert.DeserializeObject<List<Agendamento>>(File.ReadAllText(path));
                }

                for (int i = 0; i < agendamentos.Count; i++)
                {
                    Agendamento agendado = agendamentos[i];
                    if (agendado.IdSala == idSala)
                        list.Add(agendado);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Agendamento Create(Agendamento entity)
        {
            try
            {
                List<Agendamento> agendamentos = Get();
                entity.Id = agendamentos.Count + 1;

                string path = System.IO.Directory.GetCurrentDirectory() + @"\agendamentos.json";

                if (File.Exists(path))
                {
                    agendamentos = JsonConvert.DeserializeObject<List<Agendamento>>(File.ReadAllText(path));
                    agendamentos.Add(entity);

                    string jsonResult = JsonConvert.SerializeObject(agendamentos);
                    File.WriteAllText(path, jsonResult);
                }
                else
                {
                    agendamentos.Add(entity);
                    string jsonResult = JsonConvert.SerializeObject(agendamentos);
                    File.WriteAllText(path, jsonResult);
                }

                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
       
    }
}
