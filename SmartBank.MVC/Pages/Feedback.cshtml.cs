using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBank.MVC.Models;
using SmartBank.MVC.Services;

namespace SmartBank.MVC.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly FeedbackService _service;

        public FeedbackModel(FeedbackService service)
        {
            _service = service;
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = new Feedback();

        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"{state.Key}: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
                return Page();

            await _service.CreateAsync(Feedback);

            SuccessMessage = "Feedback submitted successfully!";
            ModelState.Clear();

            return Page();
        }
    }
}
