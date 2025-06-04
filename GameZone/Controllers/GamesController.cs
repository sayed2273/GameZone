


using System.Diagnostics.CodeAnalysis;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IDeviceService _deviceService;
        private readonly IGameService _gameService;

        public GamesController(ApplicationDbContext context, ICategoryService categoryService, IDeviceService deviceService, IGameService gameService)
        {
            // _context = context;
            _categoryService = categoryService;
            _deviceService = deviceService;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games); 
        }

        public IActionResult Details(int id)
        {
            var game = _gameService.GetById(id);
            if (game is null)
                return NotFound();
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoryService.GetSelectList(),
                Devices = _deviceService.GetSelectList(),
                

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectList();
                model.Devices = _deviceService.GetSelectList();
                return View(model);
            }
            await _gameService.create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameService.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewmodel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoryService.GetSelectList(),
                Devices = _deviceService.GetSelectList(),
                CurrentCover = game.Cover,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewmodel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectList();
                model.Devices = _deviceService.GetSelectList();
                return View(model);
            }
            var game = await _gameService.Update(model);
            if (game is null)
                return BadRequest();
            //await _gameService.create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var isDeleted = _gameService.Delete(id);
            if (isDeleted)
            {
                return RedirectToAction("Index"); // أو الصفحة اللي عايز ترجع لها بعد الحذف
            }
            else
            {
                return NotFound();
            }
        }
    }
}
