using System;
using System.Collections.Generic;
using System.Globalization;

namespace RegistroGastos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dos listas paralelas: mismo índice = mismo gasto
            List<string> descripciones = new List<string>();
            List<decimal> montos = new List<decimal>();

            int opcion;

            do
            {
                Console.WriteLine();
                Console.WriteLine("=== REGISTRO DE GASTOS PERSONALES ===");
                Console.WriteLine("1. Agregar gasto");
                Console.WriteLine("2. Listar gastos");
                Console.WriteLine("3. Buscar gasto");
                Console.WriteLine("4. Salir");
                Console.Write("Elija una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0; // opción no válida
                }

                switch (opcion)
                {
                    case 1:
                        AgregarGasto(descripciones, montos);
                        break;
                    case 2:
                        ListarGastos(descripciones, montos);
                        break;
                    case 3:
                        BuscarGasto(descripciones, montos);
                        break;
                    case 4:
                        Console.WriteLine("Hasta luego.");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

            } while (opcion != 4);
        }

        static void AgregarGasto(List<string> descripciones, List<decimal> montos)
        {
            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine();

            Console.Write("Monto: ");
            string textoMonto = Console.ReadLine();

            decimal monto;
            bool montoValido = decimal.TryParse(textoMonto, NumberStyles.Number,
                CultureInfo.InvariantCulture, out monto);

            if (string.IsNullOrWhiteSpace(descripcion) || !montoValido || monto <= 0)
            {
                Console.WriteLine("Datos inválidos.");
                return; // vuelve al menú sin cerrar el programa
            }

            descripciones.Add(descripcion.Trim());
            montos.Add(monto);
            Console.WriteLine("Gasto agregado.");
        }

        static void ListarGastos(List<string> descripciones, List<decimal> montos)
        {
            if (descripciones.Count == 0)
            {
                Console.WriteLine("No hay gastos.");
                return;
            }

            decimal total = 0;

            Console.WriteLine();
            for (int i = 0; i < descripciones.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {descripciones[i]} - {montos[i]:F2}");
                total += montos[i];
            }

            Console.WriteLine($"Total acumulado: {total:F2}");
        }

        static void BuscarGasto(List<string> descripciones, List<decimal> montos)
        {
            Console.Write("Texto a buscar: ");
            string texto = Console.ReadLine();

            bool hayCoincidencias = false;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                for (int i = 0; i < descripciones.Count; i++)
                {
                    // Sin importar mayúsculas o minúsculas
                    if (descripciones[i].ToLower().Contains(texto.Trim().ToLower()))
                    {
                        Console.WriteLine($"{descripciones[i]} - {montos[i]:F2}");
                        hayCoincidencias = true;
                    }
                }
            }

            if (!hayCoincidencias)
            {
                Console.WriteLine("Sin coincidencias.");
            }
        }
    }
}