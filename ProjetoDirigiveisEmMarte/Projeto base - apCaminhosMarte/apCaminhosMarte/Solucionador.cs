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
        private List<Caminho> caminho;

        //variaveis que armazenam o melhor caminho e sua distancia total
        private List<Caminho> melhorCaminho;
        private int menorDistancia;

        //lista com todos os caminhos possiveis
        private List<List<Caminho>> caminhos;

        private Grafo grafo; //grafo que contem a matriz adjacente com os caminhos disponiveis

        private bool[] visitou;//vetor que guarda quais cidades ja foram visitadas
        

        public List<List<Caminho>> Caminhos { get => caminhos; }
        public List<Caminho> MelhorCaminho { get => melhorCaminho; }

        public Solucionador(Grafo grafo)
        {
            this.grafo = grafo;
            caminho = new List<Caminho>();//inicia uma lista vazia com os "movimentos" do caminho atual
            melhorCaminho = new List<Caminho>();//inicia uma lista com os melhores movimentos, de acordo com o criterio de distancia percorrida
            menorDistancia = 0;
            caminhos = new List<List<Caminho>>();//inicia a lista coms os caminhos encontrados
            visitou = new bool[grafo.QuantasCidades];//inicia um vetor com o numero de cidades respectivas
        }
        
        public void Soluciona(int origem, int destino)
        {
            //usa o 'for' para verificar para quais cidades se pode andar
            //sendo 'i' o codigo da cidade no grafo
            for (int i = 0; i<grafo.QuantasCidades; i++)
            {
                //algoritmo que funciona
                if (grafo.MatrizAdjacente[origem, i] != null && visitou[i] == false)//se pode andar para a cidade 'i' e ela não foi visitada
                {
                    //se a cidade i é o destino
                    if (i == destino)
                    {                        
                        caminho.Add(grafo.MatrizAdjacente[origem, destino]);//adiciona o movimento a lista, com o codigo de onde saiu, o codigo do destino e a distancia percorrida
                        caminhos.Add(new List<Caminho>(caminho));//adiciona um novo caminho à lista 'caminhos'

                        int distanciaDesse = 0;
                        foreach(Caminho movimentoDaqui in caminho)//verifica a distancia total do ultimo caminho adicionadod                      
                            distanciaDesse += movimentoDaqui.Distancia;

                        if (menorDistancia == 0)//se a menor distancia é zero, ou seja, nenhum valor foi atribuido 
                        {
                            menorDistancia = distanciaDesse;
                            melhorCaminho = new List<Caminho>(caminho);
                        }
                        else
                            if (distanciaDesse < menorDistancia)
                            {
                                menorDistancia = distanciaDesse;
                                melhorCaminho = new List<Caminho>(caminho);
                            }
                            
                        visitou[origem] = false;
                        caminho.Remove(grafo.MatrizAdjacente[origem, i]); //remove o ultimo movimento (que foi o final) e continua a busca
                    }
                    //se a cidade i não for o destino
                    else
                    {
                        //caso nao de para andar pro destino, anda para outra cidade e reinicia a busca

                        visitou[origem] = true;//marca que ja visitou a cidade atual
                        caminho.Add(grafo.MatrizAdjacente[origem, i]);//adiciona o movimento ao caminho
                        Soluciona(i, destino);//refaz a busca a partir da nova cidade
                        caminho.Remove(grafo.MatrizAdjacente[origem, i]);//remove o ultimo movimento feito
                        visitou[origem] = false;
                    }
                }
            }
        }
    }
}
