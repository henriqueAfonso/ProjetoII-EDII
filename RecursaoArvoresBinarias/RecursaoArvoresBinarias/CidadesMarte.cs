using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursaoArvoresBinarias
{
    class CidadesMarte
    {
        private int idCidade, coordenadaX, coordenadaY;
        private string nomeCidade;

        public int IdCidade
        {
            get => idCidade;

            set => idCidade = value;
        }

        public string NomeCidade
        {
            get => nomeCidade;

            set => nomeCidade = value;
        }

        public int CoordenadaX
        {
            get => coordenadaX;

            set => coordenadaX = value;
        }

        public int CoordenadaY
        {
            get => coordenadaY;

            set => coordenadaY = value;
        }
    }
}
