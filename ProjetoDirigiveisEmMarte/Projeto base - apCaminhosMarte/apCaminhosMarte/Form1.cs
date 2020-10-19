using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    public partial class Form1 : Form
    {
        Grafo grafo;
        ArvoreBinaria arvore;
        Cidade origem, destino;
        Solucionador solucionador;
        public Form1()
        {
            InitializeComponent();

            MessageBox.Show("Selecione o arquivo texto com as cidades de marte");
            if(openCidades.ShowDialog() == DialogResult.OK)
            {
                arvore = new ArvoreBinaria(openCidades.FileName);
            }

            MessageBox.Show("Selecione o arquivo texto com os caminhos entre as cidades de marte");
            if (openCaminhos.ShowDialog() == DialogResult.OK)
            {
                grafo = new Grafo(openCaminhos.FileName, arvore.Quantos);
            }
        }

        

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (origem == null || destino == null ||origem == destino)
            {
                MessageBox.Show("Selecione corretamente as cidades de origem e destino");
                return; //retorna para sair do metodo
            }

            solucionador = new Solucionador(grafo);
            solucionador.soluciona(origem.CodCidade, destino.CodCidade);

            Lista<Lista<int[]>> caminhos = solucionador.Caminhos;
            while(!caminhos.Vazia())
            {
                Lista<int[]> caminho = caminhos.GetInicio();
                while(!caminho.Vazia())
                {
                    dgvCaminhos.Rows.Add(caminho.GetInicio()[0]);
                    caminho.RemoverDoInicio();
                }
            }
        }

        private void pnlArvore_Paint(object sender, PaintEventArgs e)
        {
            if (arvore != null)
            {
                Graphics g = e.Graphics;
                arvore.DesenharArvore(true, arvore.Raiz, (int)pnlArvore.Width / 2, 0, Math.PI / 2, Math.PI / 2.5, 300, g);
            }
        }

        private void lsbOrigem_SelectedIndexChanged(object sender, EventArgs e)
        {
            origem = arvore.EnconctrarCidade(int.Parse(lsbOrigem.SelectedItem.ToString().Substring(0,2)), arvore.Raiz);
        }

        private void lsbDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            destino = arvore.EnconctrarCidade(int.Parse(lsbDestino.SelectedItem.ToString().Substring(0, 2)), arvore.Raiz);
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            if(arvore != null)
            {
                Graphics g = e.Graphics;
                double fatorDeReducaoX = pbMapa.Width / 4096.0;
                double fatorDeReducaoY = pbMapa.Height / 2048.0;
                arvore.DesenharCidades(fatorDeReducaoX, fatorDeReducaoY, g, arvore.Raiz);
            }
        }
    }
}
