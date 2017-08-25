using System;
using System.Collections.Generic;
using System.Linq;
using static ExplorandoMarte.models.Localizacao;

namespace ExplorandoMarte.models
{
    public static class Comandos
    {
        #region Variaveis

        public enum Comando { Esquerda, Direita, Avancar }

        #endregion

        #region Metodos

        public static Localizacao ProcessarComando(Coordenadas Planalto, Localizacao LocalicaoAtual, List<Comando> ComandosExecutados)
        {
            int x = LocalicaoAtual.Coordenadas.x;
            int y = LocalicaoAtual.Coordenadas.y;
            Sentido sentido = LocalicaoAtual.Direcao;

            foreach (Comando comando in ComandosExecutados)
            {
                switch (comando)
                {
                    case Comando.Avancar:
                        switch (sentido)
                        {
                            case Sentido.Sul:
                                y--;
                                break;
                            case Sentido.Norte:
                                y++;
                                break;
                            case Sentido.Leste:
                                x++;
                                break;
                            case Sentido.Oeste:
                                x--;
                                break;
                        }
                        break;
                    default:
                        sentido = MudarDirecao(sentido, comando);
                        break;
                }

                y = ValidarValores(y, Planalto.y, 0);
                x = ValidarValores(x, Planalto.x, 0);
            }

            return new Localizacao(new Coordenadas(x, y), sentido);
        }

        private static int ValidarValores(int Valor, int Maximo, int Minimo)
        {
            if (Valor > Maximo)
            { Valor = Maximo; }

            if (Valor < Minimo)
            { Valor = Minimo; }

            return Valor;
        }

        private static Sentido MudarDirecao(Sentido DirecaoAtual, Comando ComandoAtual)
        {
            int valormaximo = Enum.GetValues(typeof(Sentido)).Cast<int>().Max();
            int valoratual = (int)DirecaoAtual;

            switch (ComandoAtual)
            {
                case Comando.Esquerda:

                    if (valoratual <= 0)
                    { valoratual = valormaximo; }
                    else
                    { valoratual--; }

                    break;
                case Comando.Direita:

                    if (valoratual >= valormaximo)
                    { valoratual = 0; }
                    else
                    { valoratual++; }

                    break;
            }

            Sentido retorno = (Sentido)Enum.ToObject(typeof(Sentido), valoratual);

            return retorno;
        }

        #endregion
    }
}
