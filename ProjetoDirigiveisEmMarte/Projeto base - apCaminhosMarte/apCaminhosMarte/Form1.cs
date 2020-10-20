//Henrique Afonso MArtins - 19178
//Guilherme Brentan de Oliveira - 19175
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
                return; //retorna para sair do metodo, ja que as cidades selecionadas são inválidas
            }

            solucionador = new Solucionador(grafo);//instancia da classe solucionador, passando como parametro o grafo construido a partir das cidades do aruivo texto
            solucionador.Soluciona(origem.CodCidade, destino.CodCidade);//chama o método que busca todos os caminhos entre a origem e o destino

            List<List<Caminho>> caminhos = solucionador.Caminhos;//lista com todos os caminhos achados pelo solucionador

            dgvCaminhos.Rows.Clear();//remove todas as linhas existentes
            dgvCaminhos.Columns.Clear();//remove todas as colunas existentes
            dgvCaminhos.Columns.Add("origem", "Origem");//adiciona uma coluna para que seja possivel adicionar uma linha
            dgvCaminhos.Columns["origem"].Width = 40;//formata a largura das celulas do datagridview

            int linha = -1;//indice da linha que sera alterada no DataGridView
            int movimentoAtual;//indice da coluna que será alterada no DataGridView

            //algoritmo que exibe todos os caminhos no dgvCaminhos
            foreach (List<Caminho>caminho in caminhos)
            {
                dgvCaminhos.Rows.Add(); //adiciona uma linha para cada caminho
                linha++;//incrementa o valor da linha
                movimentoAtual = 0;//
                foreach (Caminho movimento in caminho)
                {
                    try//tenta adicionar o valor a coluna especificada
                    {
                        dgvCaminhos.Rows[linha].Cells[movimentoAtual].Value = movimento.CodOrigem;
                    }
                    catch(Exception g)//se der exceção porque a coluna não existe
                    {
                        //adiciona uma coluna nova e tenta adicionar o valor novamente
                        dgvCaminhos.Columns.Add("cidade" + movimentoAtual, "Cidade");
                        dgvCaminhos.Columns[movimentoAtual].Width = 40;
                        dgvCaminhos.Rows[linha].Cells[movimentoAtual].Value = movimento.CodOrigem;
                    }
                    
                    movimentoAtual++;
                }
                try//tenta adicionar o valor a coluna especificada
                {
                    //adiciona na ultima coluna de cada linha o codigo da cidade destino
                    dgvCaminhos.Rows[linha].Cells[movimentoAtual].Value = destino.CodCidade;
                }
                catch (Exception g)//se der exceção porque a coluna não existe
                {
                    //adiciona uma coluna nova e tenta adicionar o valor novamente
                    dgvCaminhos.Columns.Add("cidade" + movimentoAtual, "Cidade");
                    dgvCaminhos.Columns[movimentoAtual].Width = 40;
                    dgvCaminhos.Rows[linha].Cells[movimentoAtual].Value = destino.CodCidade;
                }                
            }

            List<Caminho> melhorCaminho = solucionador.MelhorCaminho;//variavel que contem o melhor caminho
            movimentoAtual = 0;//reseta o valor movimentoAtual
            linha = 0;//define a linha como zero, já que só existe um melhor caminho

            dgvMelhorCaminho.Rows.Clear();//remove todas as linhas existentes
            dgvMelhorCaminho.Columns.Clear();//remove todas as colunas existentes
            dgvMelhorCaminho.Columns.Add("origem", "Origem");//adiciona uma coluna para que seja possivel adicionar uma linha
            dgvMelhorCaminho.Columns["origem"].Width = 40;
            dgvMelhorCaminho.Rows.Add(); //adiciona uma linha para o melhor caminho

            foreach (Caminho movimento in melhorCaminho)
            {
                try//tenta adicionar o valor a coluna especificada
                {
                    dgvMelhorCaminho.Rows[linha].Cells[movimentoAtual].Value = movimento.CodOrigem;
                }
                catch (Exception g)//se der exceção porque a coluna não existe
                {
                    //adiciona uma coluna nova e tenta adicionar o valor novamente
                    dgvMelhorCaminho.Columns.Add("cidade" + movimentoAtual, "Cidade");
                    dgvMelhorCaminho.Columns[movimentoAtual].Width = 40;
                    dgvMelhorCaminho.Rows[linha].Cells[movimentoAtual].Value = movimento.CodOrigem;
                }

                movimentoAtual++;
            }
            dgvMelhorCaminho.Columns.Add("destino", "Destino");
            dgvMelhorCaminho.Columns[movimentoAtual].Width = 40;
            dgvMelhorCaminho.Rows[linha].Cells[movimentoAtual].Value = destino.CodCidade;
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

        private void dgvCaminhos_SelectionChanged(object sender, EventArgs e)
        {
            Graphics g = pbMapa.CreateGraphics();
            Pen p = new Pen(Color.Red,2);
            int codCidade, codProx;
            Cidade cidade, prox;

            double fatorDeReducaoX = pbMapa.Width / 4096.0;
            double fatorDeReducaoY = pbMapa.Height / 2048.0;

            g.Clear(Color.Transparent);//limpa o pbMapa
            g.DrawImage(pbMapa.Image, 0, 0, pbMapa.Width, pbMapa.Height);//redesenha o mapa
            arvore.DesenharCidades(fatorDeReducaoX, fatorDeReducaoY, g, arvore.Raiz);//redesenha as cidades

            //pra cada o valor de cada celula na linha selecionada
            for (int i = 0; i<dgvCaminhos.ColumnCount-1; i++)
            {
                try//verifica se tem valor na celula
                {
                    codCidade = (int)dgvCaminhos.CurrentRow.Cells[i].Value;
                    codProx = (int)dgvCaminhos.CurrentRow.Cells[i + 1].Value;
                }
                catch (Exception ex)//se ocorrer erro, é porque não há nenhum valor em alguma das celulas
                {
                    break; //sai da for, já que não haverá mais nenhum valor a partir daqui
                }
                //pega as cidades referentes aos codigos retornados
                cidade = arvore.EnconctrarCidade(codCidade, arvore.Raiz);
                prox = arvore.EnconctrarCidade(codProx, arvore.Raiz);

                //desenha uma linha entre as cidades retornadas
                g.DrawLine(p, (float)(cidade.CoordX* fatorDeReducaoX), (float)(cidade.CoordY* fatorDeReducaoY), (float)(prox.CoordX* fatorDeReducaoX), (float)(prox.CoordY* fatorDeReducaoY));

            }
        }

        private void dgvMelhorCaminho_SelectionChanged(object sender, EventArgs e)
        {
            Graphics g = pbMapa.CreateGraphics();
            Pen p = new Pen(Color.Green, 2);
            int codCidade, codProx;
            Cidade cidade, prox;

            double fatorDeReducaoX = pbMapa.Width / 4096.0;
            double fatorDeReducaoY = pbMapa.Height / 2048.0;

            g.Clear(Color.Transparent);
            g.DrawImage(pbMapa.Image, 0, 0, pbMapa.Width, pbMapa.Height);
            arvore.DesenharCidades(fatorDeReducaoX, fatorDeReducaoY, g, arvore.Raiz);

            for (int i = 0; i < dgvMelhorCaminho.ColumnCount - 1; i++)
            {
                try
                {
                    codCidade = (int)dgvMelhorCaminho.CurrentRow.Cells[i].Value;
                    codProx = (int)dgvMelhorCaminho.CurrentRow.Cells[i + 1].Value;
                }
                catch (Exception ex)
                {
                    break; //só lança excessaõ se um dos valores for null
                }

                cidade = arvore.EnconctrarCidade(codCidade, arvore.Raiz);
                prox = arvore.EnconctrarCidade(codProx, arvore.Raiz);
                g.DrawLine(p, (float)(cidade.CoordX * fatorDeReducaoX), (float)(cidade.CoordY * fatorDeReducaoY), (float)(prox.CoordX * fatorDeReducaoX), (float)(prox.CoordY * fatorDeReducaoY));

            }
            dgvMelhorCaminho.ClearSelection();
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
