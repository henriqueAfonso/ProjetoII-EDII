﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace apCaminhosMarte
{
    
    class Grafo
    {
        private const int inicioCodOrigem = 0, tamanhoCodOrigem = 3;
        private const int inicioCodDestino = 3, tamanhoCodDestino = 3;
        private const int inicioDistancia = 6, tamanhoDistancia = 5;
        private const int inicioTempo = 11, tamanhoTempo = 4;
        private const int inicioCusto = 15, tamanhoCusto = 5;

        private Caminho[,] matrizAdjacente;// matriz adjacente que ira conter a possibilidade de ir de uma cidade a outra
        private int quantasCidades;

        public Grafo(string nomeDoArquivo, int numeroDeCidades)
        {
            var arquivo = new StreamReader(nomeDoArquivo);
            matrizAdjacente = new Caminho[numeroDeCidades, numeroDeCidades]; //cria a matriz com o numero de cidades existentes
            quantasCidades = numeroDeCidades;

            var linha = arquivo.ReadLine();
            while (linha != null)
            {
                int codOrigem = int.Parse(linha.Substring(inicioCodOrigem, tamanhoCodOrigem));
                int codDestino = int.Parse(linha.Substring(inicioCodDestino, tamanhoCodDestino));
                int distancia = int.Parse(linha.Substring(inicioDistancia, tamanhoDistancia));
                int tempo = int.Parse(linha.Substring(inicioTempo, tamanhoTempo));
                int custo = int.Parse(linha.Substring(inicioCusto, tamanhoCusto));

                matrizAdjacente[codOrigem,codDestino] = new Caminho(codOrigem, codDestino, distancia, tempo,custo); //armazenamos o caminho
                matrizAdjacente[codDestino, codOrigem] = new Caminho(codDestino, codOrigem, distancia, tempo, custo);//guarda o caminho inverso para que seja possivel ir e voltar entre as cidades

                linha = arquivo.ReadLine();
            }
        }

        public Caminho[,] MatrizAdjacente { get => matrizAdjacente; }

        public int QuantasCidades { get => quantasCidades; }
    }
}
