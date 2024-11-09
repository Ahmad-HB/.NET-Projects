using Domain.Constants;
using Domain.Entity;
using Infrastructure.IRepository;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Permissions.Accounts.View)]
    public class CategoriesController : Controller
    {
        private readonly IServicesRepository<Category> _servicesCategory;
        private readonly IServicesLogRepository<LogCategory> _servicesLogLogCategory;
        private readonly UserManager<ApplicationUser> _userManager;


        public CategoriesController(IServicesRepository<Category> servicesCategory,
            IServicesLogRepository<LogCategory> servicesLogRepository, UserManager<ApplicationUser> userManager)
        {
            _servicesCategory = servicesCategory;
            _servicesLogLogCategory = servicesLogRepository;
            _userManager = userManager;
        }


        // GET: CategoriesController
        [Authorize(Permissions.Categories.View)]
        public ActionResult Categories()
        {
            return View(new CategoryViewModel
            {
                Categories = _servicesCategory.GetAll(),
                LogCategories = _servicesLogLogCategory.GetAll(),
                NewCategory = new Category()
            });
        }


        [Authorize(Permissions.Categories.View)]
        public ActionResult LogCategories()
        {
            return View(new CategoryViewModel
            {
                Categories = _servicesCategory.GetAll(),
                LogCategories = _servicesLogLogCategory.GetAll(),
                NewCategory = new Category()
            });
        }

        [Authorize(Permissions.Categories.Delete)]
        public ActionResult DeleteCategories(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesCategory.Delete(Id) && _servicesLogLogCategory.Delete(Id, Guid.Parse(userId)))
            {
                return RedirectToAction(nameof(Categories));
            }

            return RedirectToAction(nameof(Categories));
        }

        [Authorize(Permissions.Categories.Delete)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesLogLogCategory.DeleteLog(Id))
            {
                return RedirectToAction(nameof(LogCategories));
            }

            return RedirectToAction(nameof(LogCategories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Categories.Create)]
        public async Task<ActionResult> SaveCategories(CategoryViewModel model)
        {
            // ModelState.IsValid
            if (true)
            {
                var userId = _userManager.GetUserId(User);
        
                if (model.NewCategory.Id == Guid.Empty)
                {
                    //save
                    if (_servicesCategory.FindByName(model.NewCategory.Name) != null)
                    {
                        TempData["Message"] = "Category already exists!";
                        return RedirectToAction("Categories");
                    }
                    else
                    {
                        if (_servicesCategory.Save(model.NewCategory) &&
                            _servicesLogLogCategory.Save(model.NewCategory.Id, Guid.Parse(userId)))
                        {
                            TempData["Message"] = "Category saved!";
                            return RedirectToAction("Categories");
                        }
                        else
                        {
                            TempData["Message"] = "Category not saved!";
                            return RedirectToAction("Categories");
                        }
                    }
                }
                else
                {
                    //update
                    if (_servicesCategory.Save(model.NewCategory) &&
                        _servicesLogLogCategory.Update(model.NewCategory.Id, Guid.Parse(userId)))
                    {
                        TempData["Message"] = "Category updated!";
                        return RedirectToAction("Categories");
                    }
                    else
                    {
                        TempData["Message"] = "Category not updated!";
                        return RedirectToAction("Categories");
                    }
                }
            }
        
            return RedirectToAction("Categories");
        }


        // [HttpPost]
        // [AutoValidateAntiforgeryToken]
        // public IActionResult Save(CategoryViewModel model)
        // {
        //     if (true)
        //     {
        //         var userId = _userManager.GetUserId(User);
        //
        //         if (model.NewCategory.Id == Guid.Parse(Guid.Empty.ToString()))
        //         {
        //             //Save
        //             if (_servicesCategory.FindByName(model.NewCategory.Name) != null)
        //             {
        //                 TempData["Message"] = "Category already exists!";
        //
        //             }
        //
        //             else
        //             {
        //                 if (_servicesCategory.Save(model.NewCategory)
        //                     && _servicesLogLogCategory.Save(model.NewCategory.Id, Guid.Parse(userId)))
        //                 {
        //                     TempData["Message"] = "Category saved!";
        //                     return RedirectToAction(nameof(Categories));
        //
        //                 }
        //                 else
        //                 {
        //                     TempData["Message"] = "Category not saved!";
        //                     return RedirectToAction(nameof(Categories));
        //
        //                 }
        //             }
        //         }
        //         else
        //         {
        //             //Update
        //             if (_servicesCategory.Save(model.NewCategory)
        //                 && _servicesLogLogCategory.Update(model.NewCategory.Id, Guid.Parse(userId)))
        //             {
        //                 TempData["Message"] = "Category updated!";
        //                 return RedirectToAction("Categories");
        //             }
        //
        //             else
        //             {
        //                 TempData["Message"] = "Category not updated!";
        //                 return RedirectToAction("Categories");
        //             }
        //         }
        //     }
        //     return RedirectToAction(nameof(Categories));
        // }
    }
}