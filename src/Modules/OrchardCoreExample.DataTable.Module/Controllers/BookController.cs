using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Admin;
using OrchardCoreExample.DataTable.Module.Indexes;
using OrchardCoreExample.DataTable.Module.Models;
using OrchardCoreExample.DataTable.Module.ViewModels;
using System.Threading.Tasks;
using YesSql;

namespace OrchardCoreExample.DataTable.Module.Controllers
{
    [Admin]
    public class BookController: Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISession _session;
        public BookController(IAuthorizationService authorizationService, ISession session)
        {
            _authorizationService = authorizationService;
            _session = session;
        }

        public async Task<ActionResult> Index()
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.BookAccess))
            {
                return Unauthorized();
            }

            var bookQuery = _session.Query<BookModel, BookIndex>();
            var model = new BookIndexViewModel();
            foreach (var book in await bookQuery.ListAsync())
            {
                model.Books.Add(new BookModelEntry
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    CoverPhotoUrl = book.CoverPhotoUrl,
                    Description = book.Description
                });
            }
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.BookAccess))
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> CreatePost(BookModelEntry model)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.BookAccess))
            {
                return Unauthorized();
            }

            _session.Save(new BookModel
            {
                Title = model.Title,
                Author = model.Author,
                CoverPhotoUrl = model.CoverPhotoUrl,
                Description = model.Description
            });

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.BookAccess))
            {
                return Unauthorized();
            }

            var model = await _session.GetAsync<BookModel>(id);

            return View(new BookModelEntry
            {
                Id = id,
                Title = model.Title,
                Author = model.Author,
                CoverPhotoUrl = model.CoverPhotoUrl,
                Description = model.Description
            });
        }

        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(BookModelEntry model)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.BookAccess))
            {
                return Unauthorized();
            }

            _session.Save(new BookModel
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                CoverPhotoUrl = model.CoverPhotoUrl,
                Description = model.Description
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.BookAccess))
            {
                return Unauthorized();
            }

            var model = await _session.GetAsync<BookModel>(id);

            _session.Delete(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
