namespace ExplorandoMarte.models
{
    public class Localizacao
    {
        #region Variaveis

        public enum Sentido { Norte = 0, Leste = 1, Oeste = 3, Sul = 2 }
        public readonly Coordenadas Coordenadas = new Coordenadas(0, 0);
        public readonly Sentido Direcao = Sentido.Norte;

        #endregion

        public Localizacao(Coordenadas Coordenadas, Sentido Direcao)
        {
            this.Coordenadas = Coordenadas;
            this.Direcao = Direcao;
        }
    }
}
