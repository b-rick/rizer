using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace rizer.Pages;

public class ViewModel : PageModel
{
    private readonly ILogger<ViewModel> _logger;

    public ViewModel(ILogger<ViewModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}