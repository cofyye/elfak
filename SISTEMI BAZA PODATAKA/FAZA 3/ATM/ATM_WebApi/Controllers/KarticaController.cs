using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Code;

namespace ATM_WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KarticaController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiKartice")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetKartice()
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveKartice());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiKarticeOdRacuna/{BrRacuna}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetKarticeRacuna(int brRacuna)
    {
        try
        {
            return new JsonResult(DataProvider.PreuzmiKarticeOdRacuna(brRacuna));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiKarticu/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetKarticu(int id)
    {
        try
        {
            return new JsonResult(DataProvider.VratiKarticu(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete]
    [Route("IzbrisiKarticu/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteKarticu(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiKarticu(id);//
            if (res == 0)
                return BadRequest("Kartica sa ovim id-jem ne postoji!\n");
            else
                return Ok($"Uspesno obrisan kartica sa id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }



    [HttpPut]
    [Route("IzmeniKarticu/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeKartica([FromBody] KarticaView kartica, int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalidan Id kartica.");
            }



            kartica.SetRbr(id);
            int res = DataProvider.IzmeniKarticu(kartica);
            if (res == 0)
                return Ok("Uspesno ste izmenili karticu!");
            else if (res == -1)
                return BadRequest("Pokusavate da promenite tip ili atribut gde to nije moguce");
            else
                return BadRequest("Ne postoji");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }


    [HttpPost]
    [Route("DodajKarticu/{brRacuna}/{tipKartice}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddKartica([FromBody] KarticaView kartica, int brRacuna, TipKartice tipKartice)
    {
        try
        {
            kartica.Tip = tipKartice.ToString();
            int res = await DataProvider.DodajKarticu(kartica, brRacuna);
            if (res == 0)
                return Ok("Uspesno ste dodali novu karticu!");
            else
                return BadRequest("Ne postoji racun sa navedenim ID-jem u bazi podataka");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
