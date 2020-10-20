//Henrique Afonso MArtins - 19178
//Guilherme Brentan de Oliveira - 19175
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class Caminho
    {
        private int codOrigem;
        private int codDestino;
        private int distancia;
        private int tempo;
        private int custo;

        public Caminho(int codOrigem, int codDestino, int distancia, int tempo, int custo)
        {
            this.codOrigem = codOrigem;
            this.codDestino = codDestino;
            this.distancia = distancia;
            this.tempo = tempo;
            this.custo = custo;
        }

        public int CodOrigem { get => codOrigem; set => codOrigem = value; }
        public int CodDestino { get => codDestino; set => codDestino = value; }
        public int Distancia { get => distancia; set => distancia = value; }
        public int Tempo { get => tempo; set => tempo = value; }
        public int Custo { get => custo; set => custo = value; }
    }
}
