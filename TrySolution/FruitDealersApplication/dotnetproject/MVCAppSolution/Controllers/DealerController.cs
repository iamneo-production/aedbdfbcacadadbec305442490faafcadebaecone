using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.Services;
using System;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    public class DealerController : Controller
    {
        private readonly IDealerService _DealerService;

        public DealerController(IDealerService DealerService)
        {
            _DealerService = DealerService;

        }

        public IActionResult AddDealer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDealer(Dealer dealer)
        {
            try
            {
                if (dealer == null)
                {
                    return BadRequest("Invalid Dealer data");
                }

                var success = _DealerService.AddDealer(dealer);

                if (success)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Failed to add the Dealer. Please try again.");
                return View(dealer);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error response or another appropriate response
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                return View(dealer);
            }
        }

        public IActionResult Index()
        {
            try
            {
                var listDealers = _DealerService.GetAllDealers();
                return View("Index",listDealers);
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
                var success = _DealerService.DeleteDealer(id);

                if (success)
                {
                    // Check if the deletion was successful and return a view or a redirect
                    return RedirectToAction("Index"); // Redirect to the list of Dealers, for example
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
