using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.Services;
using System;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    public class FruitController : Controller
    {
        private readonly IFruitService _FruitService;

        public FruitController(IFruitService FruitService)
        {
            _FruitService = FruitService;

        }

        public IActionResult AddFruit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFruit(Fruit fruit)
        {
            try
            {
                if (fruit == null)
                {
                    return BadRequest("Invalid Fruit data");
                }

                var success = _FruitService.AddFruit(fruit);

                if (success)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Failed to add the Fruit. Please try again.");
                return View(fruit);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error response or another appropriate response
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                return View(fruit);
            }
        }

        public IActionResult Index()
        {
            try
            {
                var listFruits = _FruitService.GetAllFruits();
                return View("Index",listFruits);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error view or another appropriate response
                return View("Error"); // Assuming you have an "Error" view
            }
        }


        public IActionResult Delete(int id)
        {
            try
            {
                var success = _FruitService.DeleteFruit(id);

                if (success)
                {
                    // Check if the deletion was successful and return a view or a redirect
                    return RedirectToAction("Index"); // Redirect to the list of Fruits, for example
                }
                else
                {
                    // Handle other error cases
                    return View("Error"); // Assuming you have an "Error" view
                }
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error view or another appropriate response
                return View("Error"); // Assuming you have an "Error" view
            }
        }
    }
}
