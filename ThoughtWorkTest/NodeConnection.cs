using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class NodeConnection
  {

    Node NodeTo;
    int distance;

    public NodeConnection(Node _NodoDestino, int _distancia)
    {
      NodeTo = _NodoDestino;
      distance = _distancia;
    }

    public Node getNodeTo()
    {
      return NodeTo;
    }

    public int getDistance()
    {
      return distance;
    }

  }
}
