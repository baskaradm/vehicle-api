namespace Drive.Model.Common
{
    public interface IVehicleModel
    {
        int ID { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
        int VehicleMakeId { get; set; }
        
    }
}
