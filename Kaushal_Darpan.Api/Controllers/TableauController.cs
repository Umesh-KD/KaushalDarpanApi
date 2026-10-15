//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("api/[controller]")]
////[Authorize] // only logged-in users of your app can get a dashboard URL
//public class TableauController : ControllerBase
//{
//    private readonly IHttpClientFactory _factory;
//    private readonly IConfiguration _config;

//    public TableauController(IHttpClientFactory factory, IConfiguration config)
//    {
//        _factory = factory;
//        _config = config;
//    }

//    //[HttpGet("exam-dashboard-url")]
//    //public async Task<IActionResult> GetExamDashboardUrl()
//    //{
//    //    var baseUrl = _config["Tableau:BaseUrl"];
//    //    var user = _config["Tableau:TrustedUser"];
//    //    var viewPath = _config["Tableau:ViewPath"];

//    //    var client = _factory.CreateClient();
//    //    var body = new FormUrlEncodedContent(new[]
//    //    {
//    //        new KeyValuePair<string, string>("username", user)
//    //    });

//    //    var res = await client.PostAsync($"{baseUrl}/trusted", body);
//    //    var ticket = (await res.Content.ReadAsStringAsync()).Trim();

//    //    if (!res.IsSuccessStatusCode || ticket == "-1" || string.IsNullOrEmpty(ticket))
//    //        return StatusCode(502, new { message = "Could not get Tableau ticket. Check trusted host setup." });

//    //    var url = $"{baseUrl}/trusted/{ticket}/{viewPath}?:embed=y&:showVizHome=no&:toolbar=yes";
//    //    return Ok(new { url });
//    //}
//}