using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class Node
  {

    private string name;
    private List<NodeConnection> ListOfNodeConnection = new List<NodeConnection>();
		private int gCost, hCost;
		private Node NodeParent;

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
			return this.NodeParent;
		}

		public void setParent(Node myParent)
		{
			this.NodeParent = myParent;
		}

		public int getGCost()
		{
			return gCost;
		}

		public void setGCost(int value)
		{
			int total = value;
			if (this.NodeParent != null)
			{
				total += this.NodeParent.getGCost();
			}
			this.gCost = total;
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
      ListOfNodeConnection.Add(myNodoConexion);
    }

    public string getName()
    {
      return name;
    }

    public NodeConnection getConnectionWith(Node _Nodo)
    {
      foreach (NodeConnection _NodoConexion in ListOfNodeConnection)
      {
        if (_NodoConexion.getNodeConnected().getName() == _Nodo.getName())
        {
          return _NodoConexion;
        }
      }

      return null;

    }

		public List<NodeConnection> getConnections()
		{
			return this.ListOfNodeConnection;
		}

		public Node getClone()
		{
			Node NodeNew = new Node(this.name);

			NodeNew.ListOfNodeConnection = this.ListOfNodeConnection;

			return NodeNew;
		}

  }
}
