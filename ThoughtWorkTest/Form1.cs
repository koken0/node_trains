using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		const string shortestRoute = "the length of the shortest route (in terms of distance to travel) from ";
		const string numberOfDifferent = "the number of different ";

		private void Form1_Load(object sender, EventArgs e)
		{
			lbl1.Text = "";
			lbl2.Text = "";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
			{
				return;
			}
       string[] lineas = System.IO.File.ReadAllLines(openFileDialog1.FileName);

			foreach (string linea in lineas)
			{
				string[] dato = linea.ToLower().Split(',');
				foreach (string viaje in dato)
				{
					string elemento = viaje.Trim();

					string nombreOrigen = elemento.Substring(0, 1);
					Node _NodoOrigen = myMap.getNodeByName(nombreOrigen);
					if (_NodoOrigen == null)
					{
						_NodoOrigen = new Node(nombreOrigen);
						myMap.addNode(_NodoOrigen);
					}

					string nombreDestino = elemento.Substring(1, 1);
					Node _NodoDestino = myMap.getNodeByName(nombreDestino);
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

		}

		private void button2_Click(object sender, EventArgs e)
		{
      if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
      {
        return;
      }

			lbl2.Text = "";

      string[] lineas = System.IO.File.ReadAllLines(openFileDialog1.FileName);
			listBox1.Items.Clear();

			foreach (string linea in lineas)
			{
				if (linea.Contains("#")) continue;

				string toLowerLinea = linea.ToLower();
				if (toLowerLinea.Contains(distanceRoute))
					listBox1.Items.Add(distanceRouteResult(linea));

				if (toLowerLinea.Contains(shortestRoute))
					listBox1.Items.Add(shortestRouteResult(linea));

				if (
					toLowerLinea.Contains("trips")
					)
				{
					if (toLowerLinea.Contains("exactly"))
						listBox1.Items.Add(numberOfTripsExactsStopsResult(linea));

					if (toLowerLinea.Contains("maximum"))
						listBox1.Items.Add(numberOfTripsExactsStopsResult(linea));

				}

				if (toLowerLinea.Contains(numberOfDifferent))
					listBox1.Items.Add(numberOfDifferentMinimumDistanceResult(linea));

			}

			MessageBox.Show("Procesamiento finalizado");

		}

		private string distanceRouteResult(string linea)
		{
			MatchCollection matches = Regex.Matches(linea, "-[A-Z]| [A-Z]");
			string[] route = new string[matches.Count];
			int index = 0;
			foreach (Match match in matches)
			{
				GroupCollection data = match.Groups;
				route[index++] = data[0].Value.ToString().Trim().Replace("-","");
			}
			
			int resultado = myMap.getDistanceForRoute(route);
			if (resultado == -1)
			{
				return "NO SUCH ROUTE";
			}

			return resultado.ToString();
		}

		private string shortestRouteResult(string linea)
		{
			MatchCollection matches = Regex.Matches(linea, " [A-Z]");
			string startNode = matches[0].Value.Trim();
			string endNode = matches[1].Value.Trim();
			List<Node> ListOfShortestRoute = myMap.getShortestRoute(startNode, endNode);

			if (ListOfShortestRoute == null)
			{
				return "0";
			}

			int total = 0;
			for (int j = 0; j < ListOfShortestRoute.Count - 1; j++)
			{
				total += ListOfShortestRoute[j].getConnectionWith(ListOfShortestRoute[j + 1]).getDistance();
			}
			return total.ToString();
		}

		private string numberOfTripsExactsStopsResult(string linea)
		{
			MatchCollection matches = Regex.Matches(linea, " [A-Z] ");
			string startNode = matches[0].Value.Trim();
			string endNode = matches[1].Value.Trim();
			int algo = 0;
			int maximum = -1;
			int exactly = -1;
			matches = Regex.Matches(linea, "[0-9]+");
			if (linea.Contains("maximum")){
				int.TryParse(matches[0].Value, out maximum);
				algo = myMap.getPathsWithConstrainsStops(myMap.getNodeByName(startNode), myMap.getNodeByName(endNode), 0, maximum);
			}else{
				int.TryParse(matches[0].Value, out exactly);
				algo = myMap.getPathsWithConstrainsStops(myMap.getNodeByName(startNode), myMap.getNodeByName(endNode), exactly,0);
			}
			
			return algo.ToString();
		}

		private string numberOfDifferentMinimumDistanceResult(string linea)
		{
			MatchCollection matches = Regex.Matches(linea, " [A-Z] ");
			string startNode = matches[0].Value.Trim();
			string endNode = matches[1].Value.Trim();
			matches = Regex.Matches(linea, "[0-9]+");
			int maximum = -1;
			int.TryParse(matches[0].Value,out maximum);
			int algo = myMap.getPathsWithLessDistanceThan(myMap.getNodeByName(startNode), myMap.getNodeByName(endNode), maximum);

			return algo.ToString();
		}

	}

}