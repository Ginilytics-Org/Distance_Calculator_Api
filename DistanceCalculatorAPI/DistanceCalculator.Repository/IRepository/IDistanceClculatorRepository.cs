using DistanceCalculator.Model.Models;


namespace DistanceCalculator.Repository.IRepository
{
    public interface IDistanceClculatorRepository
    {
        Task<CityModel> GetCityModel(int uzip);
    }
}
