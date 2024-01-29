using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Setting;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : MainController
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _settingService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest;

            Setting setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            SettingEditVM model = new()
            {
                Key = setting.Key,
                Value = setting.Value
            };

            return View(model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SettingEditVM setting)
        {
            if (id is null) return BadRequest();

            Setting dbSetting = await _settingService.GetByIdAsync((int)id);

            if (dbSetting is null) return NotFound();

            if (dbSetting.Value.Contains("png") || dbSetting.Value.Contains("jpeg") || dbSetting.Value.Contains("jpg"))
            {

                setting.Value = dbSetting.Value;
                setting.Key = dbSetting.Key;


                if (setting.ImageValue is null)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (!setting.ImageValue.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(setting);
                }

                if (!setting.ImageValue.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(setting);
                }

            }
            else
            {
                if (id is null) return BadRequest();

                Setting dbsetting = await _settingService.GetByIdAsync((int)id);

                if (dbSetting is null) return NotFound();

                setting.Key = dbSetting.Key;


                if (!ModelState.IsValid)
                {
                    return View(setting);
                }

            }


            await _settingService.EditAsync(setting);

            return RedirectToAction(nameof(Index));
        }
    }
}
