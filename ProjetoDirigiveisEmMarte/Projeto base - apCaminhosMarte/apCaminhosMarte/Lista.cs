using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class Lista<X>
    {
        protected class No
        {
            public X Info { get; set; }
            public No Ante { get; set; }
            public No Prox { get; set; }

            public No (X i, No a, No p)
            {
                Info = i;
                Ante = a;
                Prox = p;
            }

            public No(X i)
            {
                Info = i;
                Ante = null;
                Prox = null;
            }
        }

        protected No primeiro, ultimo;

        public Lista()
        {
            primeiro = ultimo = null;
        }

        public void InserirNoFim(X i)
        {
            if (i == null)
                throw new Exception("Informação ausente");

            if (ultimo == null)
            {
                ultimo = new No(i);
                primeiro = ultimo;
            } else
            {
                No novo = new No(i);
                novo.Ante = ultimo;
                ultimo.Prox = novo;
                ultimo = novo;
            }
        }

        public void InserirNoInicio(X i)
        {
            if (i == null)
                throw new Exception("Informação ausente");

            if (primeiro == null)
            {
                primeiro = new No(i);
                primeiro = ultimo;
            }
            else
            {
                No novo = new No(i);
                novo.Prox = primeiro;
                primeiro.Ante = novo;
                primeiro = novo;
            }
        }

        public void RemoverDoInicio()
        {
            if (primeiro == null)
                throw new Exception("Nada a remover");

            if (primeiro == ultimo) 
            {
                primeiro = null;
                ultimo = null;
                return;
            }

            primeiro = primeiro.Prox;
            primeiro.Ante = null;
        }

        public void RemoverDoFim()
        {
            if (ultimo == null)
                throw new Exception("Nada a remover");

            if (primeiro == ultimo)
            {
                primeiro = null;
                ultimo = null;
                return;
            }

            ultimo = ultimo.Ante;
            ultimo.Prox = null;
        }

        public X GetInicio()
        {
            if (primeiro == null)
                throw new Exception("Nada a obter");

            return primeiro.Info;
        }

        public X GetFim()
        {
            if (ultimo == null)
                throw new Exception("Nada a obter");

            return ultimo.Info;
        }

        public bool Vazia()
        {
            return primeiro == null;
        }

        public int GetQtd()
        {
            if (primeiro == null)
                return 0;

            int ret = 0;

            No atual = primeiro;

            while (atual != null)
            {
                ret++;
                atual = atual.Prox;
            }

            return ret;
        }
    }
}
