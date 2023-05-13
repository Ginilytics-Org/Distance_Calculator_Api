using DistanceCalculator.Model.Models;
using DistanceCalculator.Model.ViewModels;
using DistanceCalculator.Repository.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DistanceCalculator.Repository.Repository
{
    public class DistanceClculatorRepository : IDistanceClculatorRepository
    {
        private readonly IMongoCollection<CityModel> _distanceClculatorCollection;

        public DistanceClculatorRepository(
            IOptions<Appsetting> DistanceClculatorDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DistanceClculatorDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DistanceClculatorDatabaseSettings.Value.DatabaseName);

            _distanceClculatorCollection = mongoDatabase.GetCollection<CityModel>(
                DistanceClculatorDatabaseSettings.Value.DistanceCalculatorCollectionName);
        }

        public async Task<CityModel> GetCityModel(int uzip) =>
            await _distanceClculatorCollection.Find(x=> x.ZIP == uzip).FirstOrDefaultAsync();

    }
}
