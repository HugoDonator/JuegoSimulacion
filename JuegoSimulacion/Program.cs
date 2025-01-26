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

        
    }
}
