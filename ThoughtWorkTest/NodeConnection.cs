using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class NodeConnection
  {

    Node NodeConnected;
    int distance;

    public NodeConnection(Node _NodoDestino, int _distancia)
    {
      NodeConnected = _NodoDestino;
      distance = _distancia;
    }

    public Node getNodeConnected()
    {
      return NodeConnected;
    }

    public int getDistance()
    {
      return distance;
    }

  }
}
