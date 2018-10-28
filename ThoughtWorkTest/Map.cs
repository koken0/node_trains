using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
  class Map
  {

    List<Node> listOfNode = new List<Node>();

    public void addNode(Node myNodo)
    {
      listOfNode.Add(myNodo);
    }

    public void clear() {
      listOfNode.Clear();
    }

    public Node getNodoByName(string _name)
    {
      foreach (Node _Nodo in listOfNode)
      {
        if (_Nodo.getName() == _name)
        {
          return _Nodo;
        }
      }

      return null;

    }

    public int getCount()
    {
      return listOfNode.Count;
    }

    public int getDistanceForRoute(string[] route)
    {
      int total = 0;
      for (int j = 0; j < route.Length-1; j++)
      {
        Node _NodeFrom = getNodoByName(route[j]);
        if (_NodeFrom == null)
        {
          return -1;
        }
        Node _NodeTo = getNodoByName(route[j + 1]);
        if (_NodeTo == null)
        {
          return -1;
        }
        NodeConnection _NodeConnection = _NodeFrom.getConnectionWith(_NodeTo);
        if (_NodeConnection != null)
        {
          total += _NodeConnection.getDistance();
        }
        else
        {
          return -1;
        }
      }
      return total;
    }

		public List<Node> getShortestRoute(string nodeNameFrom, string nodeNameTo)
		{
			Node NodeFrom = this.getNodoByName(nodeNameFrom);
			if (NodeFrom == null)
			{
				return null;
			}
			Node NodeTo = this.getNodoByName(nodeNameTo);
			if (NodeTo == null)
			{
				return null;
			}

			List<Node> closedSet = new List<Node>();
			List<Node> openSet = new List<Node>(){NodeFrom};
			int totalChecked = 0;

			while (openSet.Count >= 1)
			{
				Node NodeCurrent = openSet[0];
				for (int i = 1; i < openSet.Count; i++)
				{
					if (
						openSet[i].getFCost() < NodeCurrent.getFCost() 
						//|| openSet[i].getFCost() == NodeCurrent.getFCost() 
						//&& openSet[i].getHCost() < NodeCurrent.getHCost()
						//&& openSet[i].getGCost() < NodeCurrent.getGCost()
						)
					{
						NodeCurrent = openSet[i];
					}
				}
				string nodeCurrentName = NodeCurrent.getName();

				if (NodeCurrent == NodeTo && totalChecked >= 1)
				{
					return retrace(NodeFrom, NodeTo);
				}

				openSet.Remove(NodeCurrent);
				closedSet.Add(NodeCurrent);

				totalChecked++;

				List<NodeConnection> ListOfConnections = NodeCurrent.getConnections();
				foreach (NodeConnection _NodeConnection in ListOfConnections)
				{
					Node NodeConnected = _NodeConnection.getNodeTo();
					string nodeConnectedName = NodeConnected.getName();
					if (closedSet.Contains(NodeConnected))
					{
						//continue;
					}

					int distance = _NodeConnection.getDistance();
					int cost = NodeCurrent.getFCost() + distance;
					if (
						cost < NodeConnected.getFCost()
						|| !openSet.Contains(NodeConnected))
					{
						NodeConnected.setParent(NodeCurrent);
						NodeConnected.setHCost(_NodeConnection.getDistance());
						//NodeConnected.setGCost(cost);

						if (!openSet.Contains(NodeConnected))
						{
							openSet.Add(NodeConnected);
						}

					}

				}

			}

			// Path doesn't exist
			return null;

		}

		private List<Node> retrace(Node NodeStart, Node NodeEnd)
		{
			List<Node> ListToRetraceNode = new List<Node>(){NodeEnd};
			Node NodeParent = NodeEnd.getParent();
			while (NodeParent != null)
				//while (NodeCurrent != NodeStart)
			{
				ListToRetraceNode.Add(NodeParent);
				NodeParent = NodeParent.getParent();
				if (NodeParent == NodeStart)
				{
					ListToRetraceNode.Add(NodeParent);
					break;
				}
			}

			ListToRetraceNode.Reverse();

			return ListToRetraceNode;

		}

		public List<Node> ListOfTrip(string nodeNameFrom, string nodeNameTo, List<Node> ListOfIgnoredNode)
		{

			Node NodeFrom = this.getNodoByName(nodeNameFrom);
			if (NodeFrom == null)
			{
				return null;
			}
			Node NodeTo = this.getNodoByName(nodeNameTo);
			if (NodeTo == null)
			{
				return null;
			}

			List<Node> closedSet = new List<Node>();
			List<Node> openSet = new List<Node>() { NodeFrom };
			int totalChecked = 0;

			while (openSet.Count >= 1)
			{
				Node NodeCurrent = openSet[0];
				for (int i = 1; i < openSet.Count; i++)
				{
					if (
						openSet[i].getFCost() < NodeCurrent.getFCost()
						//|| openSet[i].getFCost() == NodeCurrent.getFCost() 
						//&& openSet[i].getHCost() < NodeCurrent.getHCost()
						//&& openSet[i].getGCost() < NodeCurrent.getGCost()
						)
					{
						NodeCurrent = openSet[i];
					}
				}
				string nodeCurrentName = NodeCurrent.getName();

				if (NodeCurrent == NodeTo && totalChecked >= 1)
				{
					return retrace(NodeFrom, NodeTo);
				}

				openSet.Remove(NodeCurrent);
				closedSet.Add(NodeCurrent);

				totalChecked++;

				List<NodeConnection> ListOfConnections = NodeCurrent.getConnections();
				foreach (NodeConnection _NodeConnection in ListOfConnections)
				{
					Node NodeConnected = _NodeConnection.getNodeTo();
					string nodeConnectedName = NodeConnected.getName();
					if (closedSet.Contains(NodeConnected))
					{
						//continue;
					}

					int distance = _NodeConnection.getDistance();
					int cost = NodeCurrent.getFCost() + distance;
					if (
						cost < NodeConnected.getFCost()
						|| !openSet.Contains(NodeConnected))
					{
						NodeConnected.setParent(NodeCurrent);
						NodeConnected.setHCost(_NodeConnection.getDistance());
						//NodeConnected.setGCost(cost);

						if (!openSet.Contains(NodeConnected))
						{
							openSet.Add(NodeConnected);
						}

					}

				}

			}

			// Path doesn't exist
			return null;

		}

  }
}
