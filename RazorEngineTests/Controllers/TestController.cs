using Microsoft.AspNetCore.Mvc;
using RazorEngineCore;
using System.Text;
using FileIO = System.IO.File;

namespace RazorEngineTests.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
		[HttpGet]
        public ActionResult Get()
        {
            IRazorEngine razorEngine = new RazorEngineCore.RazorEngine();

            var fileContent = FileIO.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Index.cshtml"));

            var template = razorEngine.Compile<RazorEngineTemplateBase<MyModel>>(fileContent);

            string result = template.Run(instance =>
            {
                instance.Model = new MyModel { Name = "Jef" };
            });

            return File(Encoding.ASCII.GetBytes(result), "application/octect-stream", "html.html");
        }
    }
}