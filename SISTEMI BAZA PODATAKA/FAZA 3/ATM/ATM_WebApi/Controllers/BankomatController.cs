using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class BankomatController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiBankomate")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBankomati()
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveBankomate());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiBankomat/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBankomat(int id)
    {
        try
        {
            return new JsonResult(DataProvider.VratiBankomat(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiBankomateOdFilijale/{filijalaId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBankomatiFilijale(int filijalaId)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveBankomateOdFilijale(filijalaId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("DodajBankomat/{rbrFilijale}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddBankomat([FromBody] BankomatView bankomat, int rbrFilijale)
    {
        try
        {
            await DataProvider.DodajBankomat(bankomat, rbrFilijale);
            return Ok("Uspesno ste dodali novi bankomat!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("IzmeniBankomat/{idBankomata}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeBankomat([FromBody] BankomatView bankomat, int idBankomata)
    {
        try
        {
            bankomat.SetId(idBankomata); // Ensure the ID from the route is set in the BankaView object
            DataProvider.IzmeniBankomat(bankomat);
            return Ok("Uspesno ste izmenili bankomat!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("IzbrisiBankomat/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteBankomat(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiBankomat(id);//
            if (res == 0)
                return BadRequest("Bankomat sa ovim id-jem ne postoji!\n");
            else
                return Ok($"Uspesno obrisan bankomat sa id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Route("komentari/VratiSveKomentareBankomata/{idBankomata}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetKomentare(int idBankomata)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveKomentareBankomata(idBankomata));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("komentari/DodajKomentar/{idBankomata}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddKomentar([FromBody]BankomatKomentariView komentar, int idBankomata)
    {
        try
        {
            await DataProvider.DodajKomentarBankomata(komentar, idBankomata);
            return Ok("Uspesno ste dodali novi komentar bankomata!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("komentari/IzmeniKomentarBankomata/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeKomentar([FromBody]BankomatKomentariView komentar, int id)
    {
        try
        {
            komentar.SetId(id);
            int result = DataProvider.IzmeniKomentarBankomata(komentar);
            if (result == 0)
                return BadRequest($"Komentar Bankomata sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno izmenjen komentar bankomata sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("komentari/IzbrisiKomentarBankomata/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteKomentar(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiKomentarBankomata(id);//
            if (res == 0)
                return BadRequest($"Komentar bankomata sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno obrisan komentar bankomata sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}

