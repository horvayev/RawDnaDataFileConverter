namespace DnaFormatConverter
{
    public class SNP
    {
        public string Rsid { get; set; }
        public string Chromosome { get; set; } 
        public string Position { get; set; }
        public string Result { get; set; }

        public override string ToString() 
        {
            return $"RSID: {Rsid}, CHROMOSOME: {Chromosome}, POSITION: {Position}, RESULT: {Result}";
        }
    }
}