using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FizickoLiceController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiFizickaLica")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PreuzmiFizickaLica()
    {
        try
        {
            return new JsonResult(DataProvider.VratiSvaFizickaLica());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("DodajFizickoLice")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddFizickoLice([FromBody] FizickoLiceView fizickoLice)
    {
        try
        {
            DataProvider.DodajFizickoLice(fizickoLice);
            return Ok("Uspesno ste dodali novo fizicko lice!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }


    [HttpPut]
    [Route("IzmeniFizickoLice/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateFizickoLice(int id, [FromBody] FizickoLiceView fizickoLice)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid klijent ID.");
            }
            fizickoLice.SetId(id); // Set the ID from the route
            DataProvider.IzmeniFizickoLice(fizickoLice);
            return Ok("Uspesno ste izmenili fizicko lice!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }




    /////
    [HttpDelete]
    [Route("IzbrisiFizickoLice/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteFizickoLice(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiFizickoLice(id);
            if (res == 0)
                return BadRequest("Fizicko lice sa ovim id-jem ne postoji!\n");
            else
                return Ok($"Uspesno obrisano fizicko lice sa id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

}
