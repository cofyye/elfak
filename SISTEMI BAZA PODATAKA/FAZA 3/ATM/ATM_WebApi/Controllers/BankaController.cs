using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class BankaController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiBanke")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBanke()
    {
        try 
        {
            return new JsonResult(DataProvider.VratiSveBanke());
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiBanku/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBank(int id)
    {
        try
        {
            return new JsonResult(DataProvider.VratiBanku(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("DodajBanku")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddBanka([FromBody]BankaView banka)
    {
        try
        {
            DataProvider.DodajBanku(banka);
            return Ok("Uspesno ste dodali novu banku!");

        }
        catch(Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("IzmeniBanku/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeBanku([FromBody]BankaView banka, int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Ne validan id banke.");
            }

            banka.SetId(id);
            int res = DataProvider.IzmeniBanku(banka);

            if (res == 0) return BadRequest($"Banka sa id-jem {id} ne postoji!");

            return Ok("Uspesno ste izmenili banku!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("IzbrisiBanku/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteBanku(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiBanku(id);
            if (res == 0)
                return BadRequest($"Banka sa id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno obrisana banka sa id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Route("brojeviTelefona/VratiSveBrojeve/{idBanke}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBrTelefona(int idBanke)
    {
        try
        {
            return new JsonResult(DataProvider.VratiBrojeveTelefona(idBanke));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("brojeviTelefona/DodajBrTelefona/{idBanke}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddBrTelefona([FromBody]BankaBrTelefonaView telefon, int idBanke)
    {
        try
        {
            int res = DataProvider.DodajBrTelefonaBanke(telefon, idBanke);

            if(res == 0)
            {
                return BadRequest($"Probali ste da dodate broj telefona u banku ciji je id {idBanke}, ali takva banka ne postoji");
            }

            return Ok("Uspesno ste dodali novi broj telefona!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("brojeviTelefona/IzmeniBrTelefona/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeBrTelefona([FromBody]BankaBrTelefonaView telefon, int id)
    {
        try
        {
            telefon.SetId(id);
            int result = DataProvider.IzmeniBrojTelefonaBanke(telefon);
            if (result == 0)
                return BadRequest($"Broj telefona sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno izmenjen broj telefona sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete]
    [Route("brojeviTelefona/IzbrisiBrTelefona/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteBrTelefona(int id)
    {
        try
        {
            int res = DataProvider.IzbrisiBrojTelefonaBanke(id);
            if (res == 0)
                return BadRequest($"Broj telefona sa Id-jem {id} ne postoji!\n");
            else
                return Ok($"Uspesno obrisan broj telefona sa Id-jem {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
