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

            for (int i = 1; i <=Totalparcelas; i++)
            {
                parcelas.Add(SimularParcela(i));
            }

            Task<int> PrimerParcela = Task.WhenAny(parcelas).Result;
            Console.WriteLine($"la primera parcela en terminar fue la parcela {parcelas.IndexOf(PrimerParcela) + 1}");

            Console.WriteLine("Esperando que todas las parcelas completen sus fases...");
            await Task.WhenAll(parcelas);
            Console.WriteLine("=========================================================");
            Console.WriteLine("¡Todas las parcelas han terminado su cultivo!");
            MostrarResultado(parcelas);
            Console.WriteLine("FIN DE LA SIMULACION");
            Console.ReadLine();


        }

        //Metodo para Simular una parcela
        static async Task<int>SimularParcela(int id) 
        {
            Random random= new Random();
            int CultivosTotales = 0;

            Console.WriteLine($"Parcela {id} inicia su cultivo.....");
            Console.WriteLine("=========================================================");
            for (int fase = 1; fase <= 3; fase++) 
            {

                int retraso = random.Next(1000, 3000);
                await Task.Delay(retraso);

                int produccion = random.Next(5, 15);
                CultivosTotales += produccion;

                Console.WriteLine($"Parcela {id}: Fase {fase} completada. Cultivos producidos: {produccion}. (Tiempo: {retraso} ms.) ");
             }
            
            Console.WriteLine($"Parcela {id} Termino su cultivos con {CultivosTotales} cultivos");
            return CultivosTotales;
        
        }
        //Metodo para mostrar los resultados finales
        static void MostrarResultado(List<Task<int>> parcelas) 
        {
            Console.WriteLine("\nResultados Finales: ");
            int TotalCultivos = 0;

            for (int i = 0; i < parcelas.Count; i++)
            {
                int cultivos = parcelas[i].Result;
                TotalCultivos += cultivos;
                Console.WriteLine($"Parcela {i + 1}: {cultivos} cultivos");
            }

            Console.WriteLine($"Total de cultivos Producidos por toda la parcela es {TotalCultivos} cultivos");
        }
    }
}
