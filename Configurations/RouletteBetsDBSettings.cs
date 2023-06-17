namespace RouletteBetsApi.Configurations
{
    public class RouletteBetsDBSettings
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string RoulettesCollectionName { get; set; }
        public string BetsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}
