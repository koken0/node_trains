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

    Mapa myMap = new Mapa();
    const string distanceRoute = "The distance of the route";

    private void Form1_Load(object sender, EventArgs e)
    {
      lbl.Text = "";
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
        string[] dato = linea.Split(',');
        foreach (string viaje in dato)
        {
          string elemento = viaje.Trim();

          string nombreOrigen = elemento.Substring(0, 1);
          Nodo _NodoOrigen = myMap.getNodoByName(nombreOrigen);
          if (_NodoOrigen == null)
          {
            _NodoOrigen = new Nodo(nombreOrigen);
            myMap.addNodo(_NodoOrigen);
          }

          string nombreDestino = elemento.Substring(1, 1);
          Nodo _NodoDestino = myMap.getNodoByName(nombreDestino);
          if (_NodoDestino == null)
          {
            _NodoDestino = new Nodo(nombreDestino);
            myMap.addNodo(_NodoDestino);
          }

          int distancia = -1;
          string valor = elemento.Substring(2, elemento.Length - 2);
          if (!int.TryParse(valor, out distancia)){
            MessageBox.Show("Se detectó valor numerico " + valor);
            return;
          }

          if (_NodoOrigen.getConnectionWith(_NodoDestino) == null)
          {
            _NodoOrigen.addConnection(new NodoConexion(_NodoDestino, distancia));
          }

        }
      }


    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
      {
        return;
      }

      string[] lineas = System.IO.File.ReadAllLines(openFileDialog1.FileName);

      foreach (string linea in lineas)
      {
        if (linea.Contains(distanceRoute))
        {

          string[] route = linea.Replace(distanceRoute, "").Trim().Split('-');
          int resultado = myMap.getDistanceForRoute(route);
          if (resultado == -1)
          {
            MessageBox.Show("NO SUCH ROUTE");
          }
          else
          {
            MessageBox.Show(resultado.ToString());
          }

        }
        else
        {
          MessageBox.Show("No se detecta accion");
        }
      }
    }

  }
}
