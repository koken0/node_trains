using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class NodoConexion
  {

    Nodo nodoDestino;
    int distancia;

    public NodoConexion(Nodo _NodoDestino, int _distancia)
    {
      nodoDestino = _NodoDestino;
      distancia = _distancia;
    }

    public Nodo getNodoDestino()
    {
      return nodoDestino;
    }

    public int getDistancia()
    {
      return distancia;
    }

  }
}
