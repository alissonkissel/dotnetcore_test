using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IApplication _application;

        public AppController(IApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apps = await _application.GetAll();
            return Ok(apps);
        }

        [HttpGet("application")]
        public async Task<IActionResult> GetID(int application)
        {
            var app = await _application.GetID(application);

            if (app == null)
                return NotFound();
            return Ok(app);
        }

        [HttpPost("application")]
        public async Task<IActionResult> Post([FromBody] App.Domain.Entities.App app)
        {
            var value = await _application.Post(app);
            return Ok(value);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int application, [FromBody] App.Domain.Entities.App app)
        {
            var value = await _application.Put(application, app);
            return Ok(value);
        }

        [HttpPatch("{application}")]
        public async Task<IActionResult> Patch(int application, [FromBody] JsonPatchDocument<App.Domain.Entities.App> app)
        {
            //var value = await _application.Path(application, app);
            //return Ok(value);

            //if (app == null)
            //    return BadRequest();

            //var appID = await _application.GetID(application);

            //if (appID == null)
            //    return NotFound();

            //app.ApplyTo(appID);

            //var isValid = TryValidateModel(appID);
            //if (!isValid)
            //    return BadRequest(ModelState);

            //await _application.SavePatch();

            //return NoContent();

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int application)
        {
            var value = await _application.Delete(application);
            if (value)
                return Ok(value);
            return NotFound();
        }
    }
}
