using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class Mapa
  {

    List<Nodo> listOfNodo = new List<Nodo>();

    public void addNodo(Nodo myNodo)
    {
      listOfNodo.Add(myNodo);
    }

    public void limpiarMapa() {
      listOfNodo.Clear();
    }

    public Nodo getNodoByName(string _name)
    {
      foreach (Nodo _Nodo in listOfNodo)
      {
        if (_Nodo.getNombre() == _name)
        {
          return _Nodo;
        }
      }

      return null;

    }

    public int getCount()
    {
      return listOfNodo.Count;
    }

    public int getDistanceForRoute(string[] route)
    {
      int total = 0;
      for (int j = 0; j < route.Length-1; j++)
      {
        Nodo _NodoOrigen = getNodoByName(route[j]);
        if (_NodoOrigen == null)
        {
          return -1;
        }
        Nodo _NodoDestino = getNodoByName(route[j + 1]);
        if (_NodoDestino == null)
        {
          return -1;
        }
        NodoConexion _NodoConexion = _NodoOrigen.getConnectionWith(_NodoDestino);
        if (_NodoConexion != null)
        {
          total += _NodoConexion.getDistancia();
        }
        else
        {
          return -1;
        }
      }
      return total;
    }

  }
}
