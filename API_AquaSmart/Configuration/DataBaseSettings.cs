namespace API_AquaSmart.Configuration
{
    public class DataBaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;    
        public required Dictionary<string, string> Collections { get; set; }
    }
}
