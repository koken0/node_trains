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

    public Node getNodeByName(string _name)
    {
      foreach (Node _Nodo in listOfNode)
      {
        if (_Nodo.getName().ToLower() == _name.ToLower())
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
        Node _NodeFrom = getNodeByName(route[j]);
        if (_NodeFrom == null)
        {
          return -1;
        }
        Node _NodeTo = getNodeByName(route[j + 1]);
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
			Node NodeStart = this.getNodeByName(nodeNameFrom);
			if (NodeStart == null)
			{
				return null;
			}
			Node NodeEnd = this.getNodeByName(nodeNameTo);
			if (NodeEnd == null)
			{
				return null;
			}

			NodeStart.setParent(null);
			NodeEnd.setParent(null);

			List<Node> closedSet = new List<Node>();
			List<Node> openSet = new List<Node>(){NodeStart};
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

				if (NodeCurrent == NodeEnd && totalChecked >= 1)
				{
					return retrace(NodeStart, NodeEnd);
				}

				openSet.Remove(NodeCurrent);
				closedSet.Add(NodeCurrent);

				totalChecked++;

				List<NodeConnection> ListOfConnections = NodeCurrent.getConnections();
				foreach (NodeConnection _NodeConnection in ListOfConnections)
				{
					Node NodeConnected = _NodeConnection.getNodeConnected();
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

		public int getPathsWithConstrainsStops(Node NodeStart, Node NodeEnd, int exactly, int maximum)
		{

			NodeStart.setParent(null);
			NodeEnd.setParent(null);

			List<Node> closedSet = new List<Node>();
			List<Node> openSet = new List<Node>() { NodeStart };
			int totalRoutes = 0;

			while (openSet.Count >= 1)
			{
				Node NodeCurrent = openSet[0];
				string nodeCurrentName = NodeCurrent.getName();

				openSet.Remove(NodeCurrent);

				int contador = 0;
				Node NodeToCheck = NodeCurrent;
				List<Node> path = new List<Node>();
				while (NodeToCheck.getParent() != null)
				{
					path.Add(NodeToCheck);
					NodeToCheck = NodeToCheck.getParent();
					contador++;

					if (exactly > 0 && contador > exactly) break;

					if (maximum > 0 && contador > maximum) break;

				}

				if (contador >= 1)
				{
					if (exactly > 0)
					{
						if (contador == exactly && path.First().getName() == NodeEnd.getName())
						{
							totalRoutes++;
						}
						if (contador > exactly) continue;
					}

					if (maximum > 0)
					{
						if (contador <= maximum && path.First().getName() == NodeEnd.getName())
						{
							totalRoutes++;
						}
						if (contador > maximum) continue;
					}
				}

				List<NodeConnection> ListOfConnections = NodeCurrent.getConnections();
				foreach (NodeConnection _NodeConnection in ListOfConnections)
				{
					Node NodeConnected = _NodeConnection.getNodeConnected();
					string nodeConnectedName = NodeConnected.getName();
					Node NodeCloned = NodeConnected.getClone();
					
					if (!openSet.Contains(NodeCloned)){
						NodeCloned.setParent(NodeCurrent);
						openSet.Add(NodeCloned);
					}

				}

			}

			return totalRoutes;

		}

		public int getPathsWithLessDistanceThan(Node NodeStart, Node NodeEnd, int maximum)
		{

			NodeStart.setParent(null);
			NodeEnd.setParent(null);

			List<Node> closedSet = new List<Node>();
			List<Node> openSet = new List<Node>() { NodeStart };
			int totalRoutes = 0;

			while (openSet.Count >= 1)
			{
				Node NodeCurrent = openSet[0];
				string nodeCurrentName = NodeCurrent.getName();

				openSet.Remove(NodeCurrent);

				int contador = 0;
				Node NodeToCheck = NodeCurrent;
				List<Node> path = new List<Node>();
				while (NodeToCheck.getParent() != null)
				{
					path.Add(NodeToCheck);
					contador += NodeToCheck.getParent().getConnectionWith(NodeToCheck).getDistance();
					if (contador > maximum)
					{
						break;
					}
					NodeToCheck = NodeToCheck.getParent();
				}

				if (contador >= 1)
				{
					if (contador < maximum && path.First().getName() == NodeEnd.getName())
					{
						totalRoutes++;
					}
					if (contador > maximum)
					{
						continue;
					}
				}


				List<NodeConnection> ListOfConnections = NodeCurrent.getConnections();
				foreach (NodeConnection _NodeConnection in ListOfConnections)
				{
					Node NodeConnected = _NodeConnection.getNodeConnected();
					string nodeConnectedName = NodeConnected.getName();
					Node NodeCloned = NodeConnected.getClone();

					if (!openSet.Contains(NodeCloned))
					{
						NodeCloned.setParent(NodeCurrent);
						openSet.Add(NodeCloned);
					}

				}

			}

			return totalRoutes;

		}

  }
}
