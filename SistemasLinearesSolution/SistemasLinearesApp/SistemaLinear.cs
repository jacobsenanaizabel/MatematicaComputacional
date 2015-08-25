using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SistemasLinearesApp
{
    class SistemaLinear
    {
        public void eliminacaoGauss(double[] b, double[,]a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double[] x = new double[b.Length];

            int k, i, j; int n = b.Length; double m;

            for (k = 0; k <= n - 2; k++)
            {
                for (i = k + 1; i <= n - 1; i++)
                {
                    m = a[i, k] / a[k, k];

                    for (j = 0; j < n; j++)
                    {
                        a[i, j] = a[i, j] - (m * a[k, j]);
                    }
                    b[i] = b[i] - (m * b[k]);

                }
            }

            x[n - 1] = b[n - 1] / a[(n - 1), (n - 1)];//Aqui já encontramos o Z ou x3 da equação
            for (k = n - 2; k >= 0; k--)
            {
                x[k] = b[k];//O primeiro for faz com que se inicie a busca pelo Y ou x2
                for (i = k + 1; i <= n - 1; i++)
                {
                    x[k] = x[k] - a[k, i] * x[i];
                }
                x[k] = x[k] / a[k, k];
            }

            sw.Stop();
            Console.WriteLine("Com método de Gauss e levou "+ sw.Elapsed + " de tempo ");
            Console.WriteLine("");
            for (i = 0; i < x.Length; i++)
            {
                Console.WriteLine(Math.Round((x[i]), 3));
            }
            Console.WriteLine("");
            Console.ReadKey();
        }
        public void eliminacaoJacobi(double[] b, double[,] a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int i, j, k;
            

            //double[] x0 = new double[a.Length];//Vetor de 0
            double[] x0 = new double[b.Length];//vetor dos termos constantes

       
            //double[] vA = { 1, 2, 3 };//vetor de aproximação inicial
            double[] vA = { 0, 0, 0 };

            const int numDeIteracoes = 20; //Numero maximo de iterações
            //double[] soltem = new double[3];
            //double[] sol = new double[3];

            int numDeLinhas = 3;

            //Cria zeros na Eq.
            for (int w = 0; w <= x0.Length - 1; w++)
            {
                x0[w] = 0;
            }

            k = 1;//Inicia a iteração
            Console.WriteLine("Resultado por Jacobi com " + numDeIteracoes + " iteracoes:");
            Console.WriteLine("");
            while (k <= numDeIteracoes)
            {
                //Iteração de Jacobi
                for (i = 0; i < numDeLinhas; i++)
                {
                    x0[i] = 0;
                    for (j = 0; j <= numDeLinhas - 1; j++)
                    {
                        if (i != j)
                        {
                            Math.Round((x0[i] += a[i, j] * vA[j]), 3);
                        }

                    }
                    x0[i] = Math.Round(((b[i] - x0[i]) / a[i, i]), 3);

                }
                Array.Copy(x0, vA, x0.Length);
                Console.WriteLine("Iteracao " + k);
                k = k + 1;
                for (i = 0; i < numDeLinhas; i++)
                {
                    Console.WriteLine(x0[i]);
                }
                Console.WriteLine("");
            }
            sw.Stop();
            Console.WriteLine("Com método de Jacobi levou " + sw.Elapsed + " de tempo ");
            Console.WriteLine("");
            Console.ReadKey();

        }
        public void eliminacaoGaussSeidel(double[] b, double[,] a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int i, j, k;
            double soma = 0;

            //double[] x0 = { 1, 2, 3 }; //new double[b.Length];
            double[] x0 = { 0, 0, 0 };
            const int numDeIteracoes = 50;
            //double[] soltem = new double[3];
            //double[] sol = new double[3];

            int numDeLinhas = 3;

            k = 1;//Inicia a iteração
            Console.WriteLine("Resultado por Gauss-Seidel com " + numDeIteracoes + " iteracoes:");
            Console.WriteLine("");
            while (k <= numDeIteracoes)
            {

                //Iteração de Gauss-Seidell
                for (i = 0; i < numDeLinhas; i++)
                {
                    soma = 0;
                    for (j = 0; j <= numDeLinhas - 1; j++)
                    {
                        if (i != j)
                        {
                            soma += Math.Round((a[i, j] * x0[j]), 3);
                        }

                    }
                    x0[i] = Math.Round(((b[i] - soma) / a[i, i]), 3);

                }
;
                Console.WriteLine("Iteração " + k);
                k = k + 1;
                for (i = 0; i < numDeLinhas; i++)
                {
                    Console.WriteLine(x0[i]);
                }
                Console.WriteLine("");
            }
            sw.Stop();
            Console.WriteLine("O sistema levou " + sw.Elapsed + " com o metodo de Seidel para ser resolvido");
            Console.WriteLine("");
            Console.ReadKey();
        }

        public static void Main(string[] args)
        {
            //matriz usada de base para todos os sistemas 
            double[] b = { 7, 9, 2 };
            double[,] a = { { 1, 1, 1 }, { 2, 1, -1 }, { 1, -2, 22 } };

            SistemaLinear Sistemas = new SistemaLinear();
            Sistemas.eliminacaoGauss(b,a);
            Sistemas.eliminacaoJacobi(b,a);
            Sistemas.eliminacaoGaussSeidel(b,a);
        }
    }
}
