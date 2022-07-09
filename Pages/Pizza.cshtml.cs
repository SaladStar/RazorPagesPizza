using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesPizza.Pages
{
    using RazorPagesPizza.Models;
    using RazorPagesPizza.Services;

    public class PizzaModel : PageModel
    {
        public List<Pizza> pizzas = new();
        
         // Validate and pass Pizza entries from the Pizza form using the bind property 
        [BindProperty]
        public Pizza NewPizza { get; set; } = new();

        public void OnGet()
        {
            pizzas = PizzaService.GetAll();
        }

        // Utility method to format the boolean value as a string
        public string GlutenFreeText(Pizza pizza)
        {
            if (pizza.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }

        /*
         *  Asynchronous onPost page handler
         */
        public IActionResult OnPostAsync()
        {

            // Verify the user-submitted data posted to the PageModel is valid.
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            PizzaService.Add(NewPizza); 

            return RedirectToAction("Get");
        }

        // Delete buttons in list of pizzas modifies data, an HTTP POST required
        public IActionResult OnPostDelete(int id)
        {
            PizzaService.Delete(id);
            return RedirectToAction("Get");
        }

    }
}
