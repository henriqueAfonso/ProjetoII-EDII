using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class Solucionador
    {
        private Lista<int[]> caminho;
        private Lista<Lista<int[]>> caminhos;
        private Grafo grafo; //grafo que contem a matriz adjacente com os caminhos disponiveis

        public Lista<Lista<int[]>> Caminhos { get => caminhos; }

        public Solucionador(Grafo grafo)
        {
            this.grafo = grafo;
            caminho = new Lista<int[]>();//inicia a lista com os "movimentos" do caminho
            caminhos = new Lista<Lista<int[]>>();//inicia a lista coms os caminhos encontrados
        }
        public void soluciona(int origem, int destino)
        {
            
            for (int i = 0; i<grafo.QuantasCidades; i++)
            {
                if (grafo.MatrizAdjacente[origem, destino] != 0)
                {
                    int[] movimentoFinal = { origem, destino, grafo.MatrizAdjacente[origem, destino] };
                    caminho.InserirNoInicio(movimentoFinal);//adiciona o movimento a lista, com o codigo de onde saiu, o codigo do destino e a distancia percorrida
                    caminhos.InserirNoFim(caminho);
                    break;//sai do metodo pois achou o caminho ate o destino
                }
                if (grafo.MatrizAdjacente[origem, i] != 0)
                {
                    int[] movimento = { origem, i, grafo.MatrizAdjacente[origem, i] };
                    //caso nao de para andar diretamente
                    caminho.InserirNoFim(movimento);
                    soluciona(i, destino);
                }
            }
        }
    }
}
