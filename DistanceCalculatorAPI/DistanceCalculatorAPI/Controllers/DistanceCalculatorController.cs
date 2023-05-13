using DistanceCalculator.Model.ViewModels;
using DistanceCalculator.Service.IService;
using Microsoft.AspNetCore.Mvc;



namespace DistanceCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceCalculatorController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDistanceCalculatorService _distanceClculatorService;

        public DistanceCalculatorController(IWebHostEnvironment webHostEnvironment, IDistanceCalculatorService distanceClculatorService)
        {
            _webHostEnvironment = webHostEnvironment;
            _distanceClculatorService = distanceClculatorService;
        }
        [HttpPost]
        public IActionResult Getdistance(DistanceCalculatorVM distanceCalculatorVM)
        {
            
            return Ok(_distanceClculatorService.Getdistance(distanceCalculatorVM));
           
        }
    }
}
