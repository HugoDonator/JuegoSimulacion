using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoSimulacion
{
    internal class Program
    {
        static async Task Main()
        {
            Console.WriteLine("BIENVENIDO a la simulacion de una Granja de Cultivos");

            List<Task<int>> parcelas = new List<Task<int>>();
            int Totalparcelas = 3;

            for (int i = 0; i < Totalparcelas; i++)
            {
                parcelas.Add(SimularParcela(i));
            }

            Task<int> PrimerParcela = Task.WhenAny(parcelas).Result;
            Console.WriteLine($"la primera parcela en terminar fue la parcela {parcelas.IndexOf(PrimerParcela) + 1}");

            await Task.WhenAll(parcelas);


        }

        //Metodo para Simular una parcela
        static async Task<int>SimularParcela(int id) 
        {
            Random random= new Random();
            int CultivosTotales = 0;

            Console.WriteLine($"Parcela {id} inicia su cultivo");
            for (int fase = 1; fase <= 3; fase++) 
            {

                int retraso = random.Next(1000, 3000);
                await Task.Delay(retraso);

                int produccion = random.Next(5, 15);
                CultivosTotales += produccion;

                Console.WriteLine($"Parcela {id}: Fase{fase} completada. Cultivos producidos: {produccion}. Tiempo: {retraso} ms. ");
             }
            Console.WriteLine($"Parcela {id} Termino su cultivos con {CultivosTotales} cultivos");
            return CultivosTotales;
        
        }
    }
}
