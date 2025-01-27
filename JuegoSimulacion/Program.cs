using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            Random random = new Random();

            for (int i = 1; i <= Totalparcelas; i++)
            {
                int ParcelasId = i;

                Task<int> parcela = Task.Factory.StartNew(() =>
                {
                    return SimularParcela(ParcelasId, random); 
                }, TaskCreationOptions.AttachedToParent); ;

                parcelas.Add(parcela);
            }

            Task<Task<int>> primeraParcela=Task.Factory.ContinueWhenAny(parcelas.ToArray(), t =>
            {
                Console.WriteLine($"¡La primera parcela en terminar fue la Parcela {parcelas.IndexOf(t) + 1}!");
                return t;
            });

            await Task.WhenAll(parcelas);

   

            MostrarResultado(parcelas);
            Console.WriteLine("FIN DE LA SIMULACION DE LA GRANJA");
            Console.ReadLine();

        }

        //Metodo para Simular una parcela
        static  int SimularParcela(int id,Random random) 
        {
        int CultivosTotales = 0;
        Console.WriteLine($"Parcela {id} ha comenzado a cultivar.....");
            Console.WriteLine("=================================================");
        for (int fase=1; fase<=3;fase++)
        {
            int retraso = random.Next(1000, 3000);
            Task.Delay(retraso).Wait();

            int produccion = random.Next(5,15);
            CultivosTotales += produccion;
            Console.WriteLine($"Parcela {id} ha completado la fase {fase} y ha producido {produccion} cultivos. Tiempo: {retraso} ms.");
                
        }

        Console.WriteLine($"Parcela {id} ha Completado todas sus fases con  {CultivosTotales} cultivos.");
        
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
