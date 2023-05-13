using AvitaMedicalClinic.Models.ViewModels;
using DistanceCalculator.Model.ViewModels;
using DistanceCalculator.Repository.IRepository;
using DistanceCalculator.Service.IService;
using System;
using System.Net;

namespace DistanceCalculator.Service.Services
{
    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        private readonly IDistanceClculatorRepository _distanceClculatorRepository;
        private ServiceResult _serviceResult;
        public DistanceCalculatorService(IDistanceClculatorRepository distanceClculatorRepository)
        {
            _distanceClculatorRepository = distanceClculatorRepository;
            _serviceResult = new ServiceResult { Status = true, Message = "Success", StatusCode = Convert.ToInt32(HttpStatusCode.OK) };
        }
        //Calculting Distance 
        public Task<ServiceResult> Getdistance(DistanceCalculatorVM distanceCalculatorVM)
        {
            try
            {
                var fromCity = _distanceClculatorRepository.GetCityModel(distanceCalculatorVM.FromZip);
                var toCity = _distanceClculatorRepository.GetCityModel(distanceCalculatorVM.ToZip);

                if(fromCity.Result == null || toCity.Result == null)
                {
                    _serviceResult.ResultData = "Zip codes does not exist";
                    return Task.FromResult(_serviceResult);
                }

                double lon1 = toRadians(Convert.ToDouble(fromCity.Result.LNG));
                double lon2 = toRadians(Convert.ToDouble(toCity.Result.LNG));
                double lat1 = toRadians(Convert.ToDouble(fromCity.Result.LAT));
                double lat2 = toRadians(Convert.ToDouble(toCity.Result.LAT));

                // Haversine formula
                double dlon = lon2 - lon1;
                double dlat = lat2 - lat1;
                double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                        Math.Cos(lat1) * Math.Cos(lat2) *
                        Math.Pow(Math.Sin(dlon / 2), 2);

                double c = 2 * Math.Asin(Math.Sqrt(a));

                // Radius of earth in
                // kilometers. Use 3956
                // for miles
                double r = 6371;
                var query = Math.Round(ConvertKilometersToMiles(c * r), 2);
                _serviceResult.ResultData = "The distance between " + fromCity.Result.City  + "(" + fromCity.Result.ZIP + ") " + "to " + toCity.Result.City +"(" + toCity.Result.City +") is "+ query +  " miles";
                return Task.FromResult(_serviceResult);
            }
            catch (Exception ex)
            {              
                _serviceResult.Message = ex.Message;
                _serviceResult.ResultData = "Failed";
                _serviceResult.Status = false;
                _serviceResult.ResultData = Convert.ToInt32(HttpStatusCode.OK);
                return Task.FromResult(_serviceResult);
            }

        }

        #region privateMethods
        static double toRadians(double angleIn10thofaDegree)
        {      
            return (angleIn10thofaDegree *
                        Math.PI) / 180;
        }
        //kilometers to miles
        public static double ConvertKilometersToMiles(double kilometers) 
        { 
            double miles = kilometers / 1.60934; return miles; 
        }

        #endregion
    }
}
