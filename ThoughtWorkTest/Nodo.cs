using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class Nodo
  {

    string nombre;
    List<NodoConexion> listOfNodoConexion = new List<NodoConexion>();

    public Nodo(string myNombre)
    {
      nombre = myNombre;
    }

    public void addConnection(NodoConexion myNodoConexion)
    {
      listOfNodoConexion.Add(myNodoConexion);
    }

    public string getNombre()
    {
      return nombre;
    }

    public NodoConexion getConnectionWith(Nodo _Nodo)
    {
      foreach (NodoConexion _NodoConexion in listOfNodoConexion)
      {
        if (_NodoConexion.getNodoDestino() == _Nodo)
        {
          return _NodoConexion;
        }
      }

      return null;

    }

  }
}
