using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGroup.Domain.Models
{
    public class Atendimento
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Lotacao { get; set; }
        public bool Internet { get; set; }
        public bool Tv { get; set; }
        public bool Webcam { get; set; }

        public static Atendimento InitAtendimento()
        {
            Atendimento atendimento = new Atendimento();

            //Inicia atendiemento
            Console.WriteLine("\nFormato dos dados\n");
            Console.WriteLine("07-01-2019;13:00;07-01-2019;17:00;10;Sim;Sim \n");
            Console.WriteLine("Preencha as informações para agendar");
            var infos = Console.ReadLine();
            string[] dados = infos.Split(';');

            var dataI = dados[0];
            var horaI = dados[1];
            string dateTimeInicio = dataI + " " + horaI;
            atendimento.DataInicio = DateTime.ParseExact(dataI + " " + horaI, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            var dataF = dados[2];
            var horaF = dados[3];
            string dateTimeFim = dataF + " " + horaF;
            atendimento.DataFim = DateTime.ParseExact(dataF + " " + horaF, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            atendimento.Lotacao = Convert.ToInt32(dados[4]);

            var isInternet = dados[5];
            atendimento.Internet = (isInternet.ToLower() == "sim") ? true : false;

            var isTvWebCam = dados[6];
            atendimento.Tv = (isTvWebCam.ToLower() == "sim") ? true : false;
            atendimento.Webcam = (isTvWebCam.ToLower() == "sim") ? true : false;

            return atendimento;
        }
    }


}
