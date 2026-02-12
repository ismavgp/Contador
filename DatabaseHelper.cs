using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace WinContador
{
    public class DatabaseHelper
    {
        private readonly string dataPath;

        public DatabaseHelper()
        {
            dataPath = Path.Combine(Application.StartupPath, "GameData.xml");
            InitializeDataFile();
        }

        private void InitializeDataFile()
        {
            if (!File.Exists(dataPath))
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("Games");
                doc.AppendChild(root);
                doc.Save(dataPath);
            }
        }

        public bool SaveGame(int numero1, int numero2, int resultado, string operacion, int tiempoContador)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(dataPath);

                XmlElement gameElement = doc.CreateElement("Game");
                gameElement.SetAttribute("Id", Guid.NewGuid().ToString());
                gameElement.SetAttribute("Numero1", numero1.ToString());
                gameElement.SetAttribute("Numero2", numero2.ToString());
                gameElement.SetAttribute("Resultado", resultado.ToString());
                gameElement.SetAttribute("Operacion", operacion);
                gameElement.SetAttribute("TiempoContador", tiempoContador.ToString());
                gameElement.SetAttribute("FechaHora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                doc.DocumentElement.AppendChild(gameElement);
                doc.Save(dataPath);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetGames()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(string));
                dataTable.Columns.Add("Numero1", typeof(int));
                dataTable.Columns.Add("Numero2", typeof(int));
                dataTable.Columns.Add("Resultado", typeof(int));
                dataTable.Columns.Add("Operacion", typeof(string));
                dataTable.Columns.Add("TiempoContador", typeof(int));
                dataTable.Columns.Add("FechaHora", typeof(string));

                if (File.Exists(dataPath))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(dataPath);

                    XmlNodeList gameNodes = doc.SelectNodes("//Game");
                    
                    foreach (XmlNode gameNode in gameNodes)
                    {
                        DataRow row = dataTable.NewRow();
                        row["Id"] = gameNode.Attributes["Id"].Value;
                        row["Numero1"] = int.Parse(gameNode.Attributes["Numero1"].Value);
                        row["Numero2"] = int.Parse(gameNode.Attributes["Numero2"].Value);
                        row["Resultado"] = int.Parse(gameNode.Attributes["Resultado"].Value);
                        row["Operacion"] = gameNode.Attributes["Operacion"].Value;
                        row["TiempoContador"] = int.Parse(gameNode.Attributes["TiempoContador"].Value);
                        row["FechaHora"] = gameNode.Attributes["FechaHora"].Value;
                        
                        dataTable.Rows.Add(row);
                    }
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}