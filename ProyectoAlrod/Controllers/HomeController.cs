using Microsoft.AspNetCore.Mvc;
using ProyectoAlrod.Models;
using System.Data;
using System.Diagnostics;
using System.Xml;

namespace ProyectoAlrod.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<Paciente> _pacientes = new List<Paciente>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            
            
            System.Data.DataTable dataTable = PacientesConnection.ViewPacients();

            foreach (DataRow lRow in dataTable.Rows)
            {
                _pacientes.Add(new Paciente()
                {
                    Nombre = Convert.ToString(lRow["Nombre"]),
                    Registro = Convert.ToDouble(lRow["Registro"]),
                    Sexo = lRow["Sexo"].ToString().Equals("0") ? "Masculino" : "Femenino",
                    Edad = Convert.ToString(lRow["Edad"]),
                    FechaDiagnostico = Convert.ToDateTime(lRow["Fecha_diagnostico"]),
                    comorbilidades = new Comorbilidades()
                    {
                        ICC = Convert.ToInt32(lRow["ComorbilidadeICC"]),
                        FA = Convert.ToInt32(lRow["PrevioFA"]),
                        Trombofilia = Convert.ToInt32(lRow["Trombofilia"]),
                        Endocarditis = Convert.ToInt32(lRow["Endocarditi"]),
                        HP_Previa = Convert.ToInt32(lRow["HP_previa"]),
                        TEP_Previa = Convert.ToInt32(lRow["TEP_previa"]),
                        TVP_Previa = Convert.ToInt32(lRow["TVP_previa"]),
                        TEP_Cronica = Convert.ToInt32(lRow["TEP_CRONICA"]),

                    }
                    
                });

            }
            ViewData["Pacientes"] = _pacientes;
            return View();
        }



        public IActionResult ExportToXml()
        {
            // Obtén los datos para la tabla (puedes reutilizar el código que utilizaste para mostrar la tabla en la vista).

            // Crea un objeto XmlDocument y agrega los datos en formato XML.
            XmlDocument xmlDoc = new XmlDocument();

            // Crear el elemento raíz
            XmlElement root = xmlDoc.CreateElement("Pacientes");
            xmlDoc.AppendChild(root);

            // Itera a través de los datos de los pacientes y crea elementos para cada uno.
            foreach (Paciente p in _pacientes)
            {
                XmlElement pacienteElement = xmlDoc.CreateElement("Paciente");

                // Agrega las propiedades del paciente como elementos dentro del elemento "Paciente".
                pacienteElement.AppendChild(CreateXmlElement(xmlDoc, "Nombre", p.Nombre));
                pacienteElement.AppendChild(CreateXmlElement(xmlDoc, "Registro", p.Registro.ToString()));
                // Agrega otras propiedades aquí...

                root.AppendChild(pacienteElement);
            }

            // Guarda el documento XML en una ubicación temporal o genera el archivo para la descarga.
            string xmlFilePath = "D:\\Alrod\\ProyectoAlrod\\archivo.xml";
            xmlDoc.Save(xmlFilePath);

            // Prepara la respuesta para la descarga.
            byte[] fileBytes = System.IO.File.ReadAllBytes(xmlFilePath);
            System.IO.File.Delete(xmlFilePath);

            return File(fileBytes, "application/xml", "pacientes.xml");
        }

        private XmlElement CreateXmlElement(XmlDocument xmlDoc, string name, string value)
        {
            XmlElement element = xmlDoc.CreateElement(name);
            element.InnerText = value;
            return element;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}