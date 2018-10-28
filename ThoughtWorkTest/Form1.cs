using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    Map myMap = new Map();
    const string distanceRoute = "the distance of the route";
    const string numberOfTripsStartingAndEndingWithMaximum = "the number of trips starting at @start@ and ending at @end@ with a maximum of @stops@ stops.";
    const string shortestRoute = "the length of the shortest route (in terms of distance to travel) from ";
		const string numberOfTrips = "the number of trips starting at ";

    private void Form1_Load(object sender, EventArgs e)
    {
      lbl1.Text = "";
    }

    private void button1_Click(object sender, EventArgs e)
    {
      // todo
			/*
			"C:\\Users\\jarriaran\\Desktop\\grafo.txt"
			if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
			{
				return;
			}
       string[] lineas = System.IO.File.ReadAllLines(openFileDialog1.FileName);
			*/
			string[] lineas = System.IO.File.ReadAllLines("C:\\Users\\jarriaran\\Desktop\\grafo.txt");

      foreach (string linea in lineas)
      {
        string[] dato = linea.ToLower().Split(',');
        foreach (string viaje in dato)
        {
          string elemento = viaje.Trim();

          string nombreOrigen = elemento.Substring(0, 1);
          Node _NodoOrigen = myMap.getNodoByName(nombreOrigen);
          if (_NodoOrigen == null)
          {
            _NodoOrigen = new Node(nombreOrigen);
            myMap.addNode(_NodoOrigen);
          }

          string nombreDestino = elemento.Substring(1, 1);
          Node _NodoDestino = myMap.getNodoByName(nombreDestino);
          if (_NodoDestino == null)
          {
            _NodoDestino = new Node(nombreDestino);
            myMap.addNode(_NodoDestino);
          }

          int distancia = -1;
          string valor = elemento.Substring(2, elemento.Length - 2);
          if (!int.TryParse(valor, out distancia))
          {
            MessageBox.Show("Se detectó valor numerico " + valor);
            return;
          }

          if (_NodoOrigen.getConnectionWith(_NodoDestino) == null)
          {
            _NodoOrigen.addConnection(new NodeConnection(_NodoDestino, distancia));
          }

        }
      }

      MessageBox.Show(myMap.getCount() + " grafos cargados correctamente");

			// todo
			button2.PerformClick();

    }

    private void button2_Click(object sender, EventArgs e)
    {
			/*
      if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
      {
        return;
      }

			lbl2.Text = "";

      string[] lineas = System.IO.File.ReadAllLines(openFileDialog1.FileName);
			 */
			listBox1.Items.Clear();
			string[] lineas = System.IO.File.ReadAllLines("C:\\Users\\jarriaran\\Desktop\\input.txt");

      foreach (string linea in lineas)
      {
				if (linea.Contains("#"))
				{
					continue;
				}
        string toLowerLinea = linea.ToLower();
        if (toLowerLinea.Contains(distanceRoute))
        {
          listBox1.Items.Add(distanceRouteResult(toLowerLinea));
        }
        
				if (toLowerLinea.Contains(shortestRoute))
        {
					listBox1.Items.Add(shortestRouteResult(toLowerLinea));
        }

				if (toLowerLinea.Contains(numberOfTrips))
				{
					listBox1.Items.Add(shortestRouteResult(toLowerLinea));
				}

      }
    }

    private string distanceRouteResult(string linea)
    {
      string[] route = linea.Replace(distanceRoute, "").Trim().Split('-');
      int resultado = myMap.getDistanceForRoute(route);
      if (resultado == -1)
      {
        return "NO SUCH ROUTE";
      }

      return resultado.ToString();
    }

		private string shortestRouteResult(string linea)
		{
			linea = linea.Replace(shortestRoute, "");
			string startNode = linea.Substring(0, 1);
			string endNode = linea.Substring(linea.Length - 2, 1).Replace(".", "").Replace(" ", "");
			string[] route = linea.Replace(distanceRoute, "").Trim().Split('-');
			List<Node> ListOfShortestRoute = myMap.getShortestRoute(startNode, endNode);

			if (ListOfShortestRoute == null)
			{
				return "0";
			}

			int total = 0;
			for (int j = 0; j < ListOfShortestRoute.Count-1; j++)
			{
				total += ListOfShortestRoute[j].getConnectionWith(ListOfShortestRoute[j + 1]).getDistance();
			}
			return total.ToString();
		}

		private string numberOfTripsResult(string linea)
		{
			linea = linea.Replace(shortestRoute, "");
			string startNode = linea.Substring(0, 1);
			string endNode = linea.Replace("and ending at ", "").Substring(0, 1);
			string maximumStop = "";
			return "Lol";
		}

  }

}