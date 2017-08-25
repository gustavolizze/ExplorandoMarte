using System.Collections.Generic;

namespace ExplorandoMarte.models
{
    public class Sonda
    {
        #region Variaveis

        public readonly string NomeSonda = string.Empty;
        public readonly Coordenadas Planalto = new Coordenadas(0, 0);
        public readonly Localizacao Aterrissagem = null;
        private Localizacao localizacaoatual = null;

        #endregion

        #region Propriedades

        public Localizacao LocalizacaoAtual
        {
            get
            {
                return localizacaoatual;
            }
        }

        #endregion

        #region Metodos

        public string Status()
        {
            return string.Format("Sonda - {0} Localizada Em X: {1} - Y: {2} Apontada Para o {3}", new object[] { NomeSonda, LocalizacaoAtual.Coordenadas.x, LocalizacaoAtual.Coordenadas.y, LocalizacaoAtual.Direcao });
        }

        public void Mover(List<Comandos.Comando> ComandosExecutados)
        {
            if (ComandosExecutados == null)
            { return; }

            if (ComandosExecutados.Count <= 0)
            { return; }

            localizacaoatual = Comandos.ProcessarComando(Planalto, LocalizacaoAtual, ComandosExecutados);
        }

        public static Sonda EfetuarLancamento (string NomeSonda, Coordenadas Planalto, Localizacao Aterrissagem)
        {
            return new Sonda(NomeSonda, Planalto, Aterrissagem);
        }

        #endregion

        private Sonda(string NomeSonda, Coordenadas Planalto, Localizacao Aterrissagem)
        {
            if (string.IsNullOrEmpty(NomeSonda))
            { throw new System.Exception("Poxa chefe não vai me dar um nome :("); }

            this.localizacaoatual = Aterrissagem;
            this.NomeSonda = NomeSonda;
            this.Aterrissagem = Aterrissagem ?? throw new System.Exception("Informe onde você deseja pousar, Capitão :) ");
            this.Planalto = Planalto ?? throw new System.Exception("Informe as Coordenadas do terreno, Capitão :) ");
        }

    }
}
