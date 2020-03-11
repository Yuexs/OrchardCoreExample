using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCoreExample.DataTable.Module.Indexes;
using OrchardCoreExample.DataTable.Module.Models;
using YesSql;

namespace OrchardCoreExample.DataTable.Module.Controllers
{
    [Route("api/data/[action]")]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    [ApiController]
    public class ApiController: Controller
    {
        private readonly ISession _session;

        public ApiController(ISession session)
        {
            _session = session;
        }

        [HttpGet]
        public async Task<ActionResult> Test1()
        {
            var bookQuery = _session.Query<BookModel, BookIndex>();
            var dataList = await bookQuery.ListAsync();
            return Json(new { result = dataList });
        }

        [HttpGet]
        public async Task<IActionResult> Test2(int id)
        {
            var model = await _session.GetAsync<BookModel>(id);

            return Json(new { result = model });
        }
    }
}
