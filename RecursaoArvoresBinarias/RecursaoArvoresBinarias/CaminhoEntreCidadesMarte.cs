using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursaoArvoresBinarias
{
    class CaminhoEntreCidadesMarte
    {
        int idCidadeOrigem, idCidadeDestino, distancia, tempo, custo;

        public int IdCidadeOrigem
        {
            get => idCidadeOrigem;
            set => idCidadeOrigem = value;
        }

        public int IdCidadeDestino
        {
            get => idCidadeDestino;
            set => idCidadeDestino = value;
        }
        public int Distancia
        {
            get => distancia;
            set => distancia = value;
        }
        public int Tempo
        {
            get => tempo;
            set => tempo = value;
        }
        public int Custo
        {
            get => custo;
            set => custo = value;
        }
    }
}
