using AvitaMedicalClinic.Models.ViewModels;
using DistanceCalculator.Model.ViewModels;


namespace DistanceCalculator.Service.IService
{
    public interface IDistanceCalculatorService
    {
        Task<ServiceResult> Getdistance(DistanceCalculatorVM distanceCalculatorVM);
    }
}
