namespace ProyectoAlrod.Models
{
    public class Paciente
    {
        public string Nombre { get; set; }
        public double Registro { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public DateTime FechaDiagnostico { get; set; }

        public Comorbilidades comorbilidades { get; set; }
    }
}
