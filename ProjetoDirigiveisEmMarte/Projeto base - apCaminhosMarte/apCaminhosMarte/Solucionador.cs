using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class Solucionador
    {
        //variavel que armazena o caminho atual
        private List<int[]> caminho;

        //variaveis que armazenam o melhor caminho e sua distancia total
        private List<int[]> melhorCaminho;
        private int menorDistancia;

        //lista com todos os caminhos possiveis
        private List<List<int[]>> caminhos;

        private Grafo grafo; //grafo que contem a matriz adjacente com os caminhos disponiveis

        private bool[] visitou;//vetor que guarda quais cidades ja foram visitadas
        

        public List<List<int[]>> Caminhos { get => caminhos; }

        public Solucionador(Grafo grafo)
        {
            this.grafo = grafo;
            caminho = new List<int[]>();//inicia uma lista vazia com os "movimentos" do caminho atual
            melhorCaminho = new List<int[]>();//inicia uma lista com os melhores movimentos, de acordo com o criterio de distancia percorrida
            menorDistancia = 0;
            caminhos = new List<List<int[]>>();//inicia a lista coms os caminhos encontrados
            visitou = new bool[grafo.QuantasCidades];//inicia um vetor com o numero de cidades respectivas
        }
        
        public void Soluciona(int origem, int destino)
        {
            //usa o 'for' para verificar para quais cidades se pode andar
            //sendo 'i' o codigo da cidade no grafo
            for (int i = 0; i<grafo.QuantasCidades; i++)
            {
                //algoritmo antigo
                /*if (grafo.MatrizAdjacente[origem, destino] != 0)
                {
                    int[] movimentoFinal = { origem, destino, grafo.MatrizAdjacente[origem, destino] };
                    caminho.Add(movimentoFinal);//adiciona o movimento a lista, com o codigo de onde saiu, o codigo do destino e a distancia percorrida
                    
                    caminhos.Add(new List<int[]>(caminho));
                    
                    visitou[origem] = false;
                    caminho.Remove(movimentoFinal); //remove o ultimo movimento (que foi o final) e continua a busca
                    break;//sai do metodo pois achou o caminho ate o destino
                }*/

                //algoritmo que funciona
                if (grafo.MatrizAdjacente[origem, i] != 0 && visitou[i] == false)//se pode andar para a cidade 'i' e ela não foi visitada
                {
                    //se a cidade i é o destino
                    if (i == destino)
                    {                        
                        int[] movimento = { origem, destino, grafo.MatrizAdjacente[origem, destino] };
                        caminho.Add(movimento);//adiciona o movimento a lista, com o codigo de onde saiu, o codigo do destino e a distancia percorrida
                        caminhos.Add(new List<int[]>(caminho));//adiciona um novo caminho à lista 'caminhos'

                        int distanciaDesse = 0;
                        foreach(int[] movimentoDaqui in caminho)//verifica a distancia total do ultimo caminho adicionadod                      
                            distanciaDesse += movimentoDaqui[2];

                        if (menorDistancia == 0)//se a menor distancia é zero, ou seja, nenhum valor foi atribuido 
                        {
                            menorDistancia = distanciaDesse;
                            melhorCaminho = new List<int[]>(caminho);
                        }
                        else
                            if (distanciaDesse < menorDistancia)
                            {
                                menorDistancia = distanciaDesse;
                                melhorCaminho = new List<int[]>(caminho);
                            }
                            

                        visitou[origem] = false;
                        caminho.Remove(movimento); //remove o ultimo movimento (que foi o final) e continua a busca
                    }
                    //se a cidade i não for o destino
                    else
                    {
                        int[] movimento = { origem, i, grafo.MatrizAdjacente[origem, i] };
                        //caso nao de para andar pro destino, anda para outra cidade e reinicia a busca

                        visitou[origem] = true;//marca que ja visitou a cidade atual
                        caminho.Add(movimento);//adiciona o movimento ao caminho
                        Soluciona(i, destino);//refaz a busca a partir da nova cidade
                        caminho.Remove(movimento);//remove o ultimo movimento feito
                        visitou[origem] = false;
                    }
                }
            }
        }
    }
}
