namespace b8vB6mN3zAe.Dtos
{
    public class ZipCodeResponse
    {
        public required String ID { get; set; }
        public required String Name { get; set; }
        public required int Code { get; set; }
        public required CityJoinResponse? City { get; set; }
        //TODO: add farmer list
    }
}