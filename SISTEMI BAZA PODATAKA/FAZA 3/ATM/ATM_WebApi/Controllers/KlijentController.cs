using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KlijentController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiKlijente")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetKlijente()
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveKlijente());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("IzbrisiKlijenta/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteKlijent(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiKlijenta(id);//
            if (res == 0)
                return BadRequest("Klijent sa ovim id-jem ne postoji!\n");
            else
                return Ok($"Uspesno obrisan klijenta sa id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Route("komentari/VratiSveKomentareKlijenta/{idKlijenta}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetKomentare(int idKlijenta)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveKomentareKlijenta(idKlijenta));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("komentari/DodajKomentar/{idKlijenta}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddKomentar([FromBody]KomentarKlijentaView komentar, int idKlijenta)
    {
        try
        {
            await DataProvider.DodajKomentarKlijenta(komentar, idKlijenta);
            return Ok("Uspesno ste dodali novi komentar klijenta!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("komentari/IzmeniKomentarKlijenta/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeKomentar([FromBody]KomentarKlijentaView komentar, int id)
    {
        try
        {
            komentar.SetId(id);
            int result = DataProvider.IzmeniKomentarKlijenta(komentar);
            if (result == 0)
                return BadRequest($"Komentar Klijenta sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno izmenjen komentar klijenta sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("komentari/IzbrisiKomentarKlijenta/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteKomentar(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiKomentarKlijenta(id);//
            if (res == 0)
                return BadRequest($"Komentar klijenta sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno obrisan komentar klijenta sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
