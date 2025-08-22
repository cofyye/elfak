using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class FilijalaController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiFilijale")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetFilijale()
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveFilijale());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiFilijaleOdBanke/{bankaId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetFilijaleBanke(int bankaId)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveFilijaleOdBanke(bankaId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("PreuzmiFilijalu/{rbr}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetFilijala(int rbr)
    {
        try
        {
            return new JsonResult(DataProvider.VratiFilijalu(rbr));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    [Route("DodajFilijalu/{idBanke}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddFilijala([FromBody] FilijalaView filijala, int idBanke)
    {
        try
        {
            int res = DataProvider.DodajFilijalu(filijala, idBanke);

            if(res == 0)
            {
                return BadRequest($"Banka sa id-jem {idBanke} ne postoji, ne moze se dodati filijala!");
            }
            return Ok($"Uspesno ste dodali novu filijalu u banku sa id-jem {idBanke}!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut]
    [Route("IzmeniFilijalu/{rbr}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ChangeFilijalu([FromBody] FilijalaView filijala, int rbr)
    {
        try
        {
            if (rbr <= 0)
            {
                return BadRequest("Ne validan redni broj filijale.");
            }

            filijala.SetRbr(rbr); 
            int rez = DataProvider.IzmeniFilijalu(filijala);

            if (rez == 0) return BadRequest($"Filijala sa rednim brojem {rbr} ne postoji!");

            return Ok($"Uspesno ste izmenili filijalu sa rednim brojem {rbr}!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }


    [HttpDelete]
    [Route("IzbrisiFilijalu/{redni_broj}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteFilijalu(int redni_broj)
    {
        try
        {
            int res = DataProvider.IzbrisiFilijalu(redni_broj);
            if (res == 0)
                return BadRequest("Filijala sa ovim rednim brojem ne postoji!\n");
            else
                return Ok($"Uspesno obrisana filijala sa id-jem {redni_broj}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }



}
