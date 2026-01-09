using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Storage;

namespace WebApplication2.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class WidgetsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateWigdet(WidgetDTO widget)
        {
            try
            {
                var created = WidgetStorage.Add(widget);
                return CreatedAtAction("GetWidgetById", "Widgets", new { id = widget.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetAllWidget()
        {

            try
            {
                return Ok(WidgetStorage.GetAllWidgets());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{ID}")]
        public IActionResult GetWidgetByID(Guid ID)
        {
            try
            {
                var fondwidget = Storage.WidgetStorage.getWidgetByID(ID);
                if (fondwidget == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Widget not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, fondwidget);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
