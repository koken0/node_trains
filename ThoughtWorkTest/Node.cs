using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class Node
  {

    string name;
    List<NodeConnection> listOfNodeConnection = new List<NodeConnection>();
		int gCost, hCost;
		Node Parent;

    public Node(string myNombre)
    {
      name = myNombre;
    }

		public int getFCost()
		{
			return this.gCost + this.hCost;
		}

		public Node getParent()
		{
			return this.Parent;
		}

		public void setParent(Node myParent)
		{
			this.Parent = myParent;
		}

		public int getGCost()
		{
			return gCost;
		}

		public void setGCost(int value)
		{
			this.gCost = value;
		}

		public int getHCost()
		{
			return hCost;
		}

		public void setHCost(int value)
		{
			this.hCost = value;
		}

    public void addConnection(NodeConnection myNodoConexion)
    {
      listOfNodeConnection.Add(myNodoConexion);
    }

    public string getName()
    {
      return name;
    }

    public NodeConnection getConnectionWith(Node _Nodo)
    {
      foreach (NodeConnection _NodoConexion in listOfNodeConnection)
      {
        if (_NodoConexion.getNodeTo() == _Nodo)
        {
          return _NodoConexion;
        }
      }

      return null;

    }

		public List<NodeConnection> getConnections()
		{
			return this.listOfNodeConnection;
		}

  }
}
