using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Code;

namespace ATM_WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RacunController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiRacune")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetRacune()
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveRacune());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiRacuneOdBanke/{bankaId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetRacuneBanke(int bankaId)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveRacuneOdBanke(bankaId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiRacun/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetRacun(int id)
    {
        try
        {
            return new JsonResult(DataProvider.VratiRacun(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete]
    [Route("IzbrisiRacun/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteRacun(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiRacun(id);//
            if (res == 0)
                return BadRequest("Racun sa ovim id-jem ne postoji!\n");
            else
                return Ok($"Uspesno obrisan racun sa id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }


    [HttpPut]
    [Route("IzmeniRacun/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeRacun([FromBody] RacunView racun, int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalidan Id racuna.");
            }



            racun.SetRbr(id);
            int res = DataProvider.IzmeniRacun(racun);
            if (res == 0)
                return Ok("Uspesno ste izmenili racun!");
            else if (res == -1)
                return BadRequest("Pokusavate da promenite tip ili valutu gde to nije moguce");
            else
                return BadRequest("Ne postoji");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }


    [HttpPost]
    [Route("DodajRacun/{idBanke}/{idKlijenta}/{tipRacuna}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddRacun([FromBody] RacunView racun, int idBanke,int idKlijenta, TipRacuna tipRacuna)
    {
        try
        {
            racun.Tip = tipRacuna.ToString();
            int res = await DataProvider.DodajRacun(racun, idBanke,idKlijenta);
            if (res == 0)
                return Ok("Uspesno ste dodali novi racun!");
            else
                return BadRequest("Ne postoji banka ili klijent sa navedenim ID-jem u bazi podataka");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Route("ovlascenaLica/VratiSvaOvlascenaLica/{idRacuna}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetLica(int idRacuna)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSvaOvlascenaLica(idRacuna));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("ovlascenaLica/DodajOvlascenoLice/{idRacuna}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddLice([FromBody]RacunOvlascenoLiceView lice, int idRacuna)
    {
        try
        {
            await DataProvider.DodajOvlascenoLice(lice, idRacuna);
            return Ok("Uspesno ste dodali novo ovlasceno lice za racun!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("ovlascenaLica/IzmeniOvlascenoLice/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeLice([FromBody]RacunOvlascenoLiceView lice, int id)
    {
        try
        {
            lice.SetId(id);
            int result = DataProvider.IzmeniOvlascenoLice(lice);
            if (result == 0)
                return BadRequest($"Ovlasceno lice sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno izmenjeno ovlasceno lice sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("ovlascenaLica/IzbrisiOvlascenoLice/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteLice(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiOvlascenoLice(id);//
            if (res == 0)
                return BadRequest($"Ovlasceno lice sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno obrisano ovlasceno lice sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
