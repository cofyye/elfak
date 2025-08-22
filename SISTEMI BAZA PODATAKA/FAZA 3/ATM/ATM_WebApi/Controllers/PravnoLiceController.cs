using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PravnoLiceController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiPravnoLice")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPravnoLice()
    {
        try
        {
            return new JsonResult(DataProvider.VratiSvaPravnaLica());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    [Route("DodajPravnoLice")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddPravnoLice([FromBody] PravnoLiceView pravnoLice)
    {
        if (pravnoLice == null)
        {
            return BadRequest("PravnoLice data is null.");
        }

        if (string.IsNullOrEmpty(pravnoLice.Poreski_id) || string.IsNullOrEmpty(pravnoLice.Naziv))
        {
            return BadRequest("Required fields are missing.");
        }

        try
        {
            DataProvider.DodajPravnoLice(pravnoLice);
            return Ok("Uspesno ste dodali novo pravno lice!");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }


    [HttpPut]
    [Route("IzmeniPravnoLice/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdatePravnoLice(int id, [FromBody] PravnoLiceView pravnoLice)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid klijent ID.");
            }
            pravnoLice.SetId(id); // Set the ID from the route
            DataProvider.IzmeniPravnoLice(pravnoLice);
            return Ok("Uspesno ste izmenili pravno lice!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }


    [HttpDelete]
    [Route("IzbrisiPravnoLice/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeletePravnoLice(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiPravnoLice(id);
            if (res == 0)
                return BadRequest("Pravno lice sa ovim id-jem ne postoji!\n");
            else
                return Ok($"Uspesno obrisano pravno lice sa id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
