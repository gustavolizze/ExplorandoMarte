using ExplorandoMarte.models;
using System;
using System.Collections.Generic;
using System.Linq;
using static ExplorandoMarte.models.Localizacao;

namespace ExplorandoMarte
{
    class Program
    {
        static List<Sonda> sondas = new List<Sonda>();

        static void Main(string[] args)
        {
            IniciarPrograma();
        }

        static void IniciarPrograma()
        {
            try
            {
                Console.WriteLine("Seja bem Vindo ao Programa de Lançamento de Sondas");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Quantas Sondas Deseja Lançar (Somente Números Espetinho) ?");

                int numerosondas = Convert.ToInt32(Console.ReadLine());
                string xMsg = string.Empty;

                if (!AdicionaSondas(numerosondas, out xMsg))
                {
                    Console.WriteLine(xMsg);
                    Console.Clear();
                    IniciarPrograma();
                    return;
                }

                SondasIniciadas();
            }
            catch (Exception e)
            {
                Console.Write("**** Opa, Acho que esqueci de tratar se erro :( ****");
                return;
            }
        }

        static void SondasIniciadas()
        {

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Digite o nome de uma sonda + ',' (Movimentos) Exemplo Sonda1, LMRMLL - Para Movimentar Uma Sonda, Ou Exit Para Sair");
                string retorno = Console.ReadLine();

                if (retorno.ToLower() == "exit")
                { return; }

                string[] valores = retorno.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (valores.Length < 2)
                { continue; }

                string nome = valores[0];
                string movimentos = valores[1];

                Sonda sondaselecionada = sondas.Where(c => c.NomeSonda == nome)?.FirstOrDefault();

                if (sondaselecionada == null)
                {
                    Console.WriteLine("Sonda Não Encontrada !");
                    continue;
                }

                List<Comandos.Comando> comandosexecutados = new List<Comandos.Comando>();

                foreach (char movs in movimentos)
                {
                    switch (movs)
                    {
                        case 'M':
                            comandosexecutados.Add(Comandos.Comando.Avancar);
                            break;
                        case 'L':
                            comandosexecutados.Add(Comandos.Comando.Esquerda);
                            break;
                        case 'R':
                            comandosexecutados.Add(Comandos.Comando.Direita);
                            break;
                    }
                }

                sondaselecionada.Mover(comandosexecutados);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(sondaselecionada.Status());
            }
        }

        static bool AdicionaSondas (int numeroSondas, out string xMsg)
        {
            if (!ValidarNumeroSondas(numeroSondas, out xMsg))
            { return false; }

            for (int i = 0; i < numeroSondas; i++)
            {
                Console.WriteLine(string.Format("Digite o nome da {0}º Sonda", new object[] { (i + 1) }));
                string nomeSonda = Console.ReadLine();

                if (string.IsNullOrEmpty(nomeSonda))
                {
                    Console.WriteLine("*** AVISO DEVIDO NOMES INVÁLIDOS ESTA SONDA SERÁ IGNORADA ");
                    continue;
                }

                Console.WriteLine("Digite o eixo X do Planalto");
                int xPlanalto = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite o eixo Y do Planalto");
                int yPlanalto = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite o eixo X da Localização da Sonda");
                int xSonda = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite o eixo Y da Localização da Sonda ");
                int ySonda = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Qual a Direção da Sonda ? (N Para norte, S para SUL, L para LESTE , O para Oeste) ");
                string direcao = Console.ReadLine();
                Sentido sentido = Sentido.Norte;

                switch (direcao.ToUpper())
                {
                    case "N":
                        sentido = Sentido.Norte;
                        break;
                    case "S":
                        sentido = Sentido.Sul;
                        break;
                    case "L":
                        sentido = Sentido.Leste;
                        break;
                    case "O":
                        sentido = Sentido.Oeste;
                        break;
                }

                Sonda sonda = Sonda.EfetuarLancamento(nomeSonda, new Coordenadas(xPlanalto, yPlanalto), new Localizacao(new Coordenadas(xSonda, ySonda), sentido));
                sondas.Add(sonda);
            }

            return true;
        }

        static List<Comandos.Comando> LocalizaComandos(string comandos)
        {
            List<Comandos.Comando> comandosexecutados = new List<Comandos.Comando>();

            if (string.IsNullOrEmpty(comandos))
            { return comandosexecutados; }

            foreach (char movs in comandos)
            {
                switch (movs)
                {
                    case 'M':
                        comandosexecutados.Add(Comandos.Comando.Avancar);
                        break;
                    case 'L':
                        comandosexecutados.Add(Comandos.Comando.Esquerda);
                        break;
                    case 'R':
                        comandosexecutados.Add(Comandos.Comando.Direita);
                        break;
                }
            }

            return comandosexecutados;
        }

        static bool ValidarNumeroSondas(int numeroSondas, out string xMsg)
        {
            if (numeroSondas > 5)
            {
                xMsg = "São Muitas Sondas Não é mesmo ?, Digite um número menor aposto que você não vai conseguir acompanha-las";
                return false;
            }
            else if (numeroSondas <= 0)
            {
                xMsg = "Digita um número válido aí, Não Custa nadaaaa !";
                return false;
            }
            else
            {
                xMsg = "Parabéns o número está certinho";
                return true;
            }
        }

        static bool ValidarCoordenadas(int Valor)
        {
            return (Valor > 0);
        } 
    }
}
