using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ServisController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiSveServiseBankomata/{idBankomata}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetServisiBankomata(int idBankomata)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveServiseOdBankomata(idBankomata));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("DodajServis/{idBankomata}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddServis([FromBody] ServisView servis, int idBankomata)
    {
        try
        {
            await DataProvider.DodajServis(servis, idBankomata);
            return Ok($"Uspesno ste dodali novi servis za bankomat sa id-jem {idBankomata}!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("IzmeniServis/{kodServisa}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeServis([FromBody] ServisView servis, int kodServisa)
    {
        try
        {
            servis.SetKod(kodServisa); // Ensure the ID from the route is set in the BankaView object
            int result = DataProvider.IzmeniServis(servis);
            if(result == 0)
                return BadRequest($"Servis sa kodom {kodServisa}  ne postoji!\n");
            else
                return Ok($"Uspesno obrisan servis sa kodom {kodServisa}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("IzbrisiServis/{kod}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteServis(int kod)
    {
        try
        {
            int res = DataProvider.IzbrisiServis(kod);//
            if (res == 0)
                return BadRequest($"Servis sa ovim kodom {kod} ne postoji!\n");
            else
                return Ok($"Uspesno obrisan servis sa kodom {kod}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Route("PreuzmiServis/{kod}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBankomat(int kod)
    {
        try
        {
            return new JsonResult(DataProvider.VratiServis(kod));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    [Route("PreuzmiSveOtklonjeneGreske/{kod}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetOtklonjeneGreske(int kod)
    {
        try
        {
            return new JsonResult(DataProvider.VratiOtklonjeneGreske(kod));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("DodajOtklonjenuGreskuServisa/{kod}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddOtklonjenaGreska([FromBody]OtklonjenaGreskaServisaView greska,int kod)
    {
        try
        {
            return new JsonResult(DataProvider.DodajOtklonjenuGresku(greska, kod));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("IzmeniOtklonjejuGresku/{idGreske}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeOtklonjenuGresku([FromBody]OtklonjenaGreskaServisaView greska, int idGreske)
    {
        try
        {
            greska.SetId(idGreske);
            int result = DataProvider.IzmeniOtklonjenuGresku(greska);
            if (result == 0)
                return BadRequest($"Greska sa kodom {idGreske}  ne postoji!\n");
            else
                return Ok($"Uspesno izmenjena greska sa kodom {idGreske}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("IzbrisiOtklonjenuGresku/{kod}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteOtklonjenaGreska(int kod)
    {
        try
        {
            int res = DataProvider.IzbrisiOtklonjenuGresku(kod);//
            if (res == 0)
                return BadRequest($"Greska sa kodom {kod} ne postoji!\n");
            else
                return Ok($"Uspesno obrisan greska sa kodom {kod}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
