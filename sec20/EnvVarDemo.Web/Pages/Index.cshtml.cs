using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

public class IndexModel : PageModel
{
    public string ApiUrl { get; private set; }

    public IndexModel(IConfiguration config)
    {
        ApiUrl = $"{config["ApiBaseUrl"]}/api/SecureData";
    }

    public void OnGet()
    {
    }
}
