using System.Diagnostics.Eventing.Reader;
using DatabaseAccess;
using DatabaseAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ATM_WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class TransakcijaController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiTransakcijeBankomata/bankomat/{bankomatId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetTransakcijeBankomata(int bankomatId)
    {
        try
        {
            return new JsonResult(DataProvider.VratiSveTransakcijeBankomata(bankomatId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("DodajTransakciju/bankomat/{idBankomata}/kartica/{idKartice}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddTransakcija([FromBody] TransakcijaView transakcija, int idBankomata, int idKartice)
    {
        try
        {
            if(DataProvider.VratiBankomat(idBankomata) == null)
            {
                return BadRequest($"Bankomat sa id-jem {idBankomata} ne postoji, probajte transakciju preko nekog drugog bankomata!\n");
            }

            if(DataProvider.VratiKarticu(idKartice)== null)
            {
                return BadRequest($"Kartica sa id-jem {idKartice} ne postoji, probajte transakciju preko neke druge kartice!\n");
            }

            int result = DataProvider.DodajTransakciju(transakcija, idBankomata, idKartice);
            if(result == -1)
            {
                return BadRequest("Tekuci saldo je manji od novca koji zelite da podignete.\n");
            }

            if (result == -2) {
                return BadRequest("Dnevni limit je manji od novca koji zelite da podignete.\n");
            }


            return Ok($"Uspesno ste obavili transakciju preko bankomata sa id-jem {idBankomata} putem kartice {idKartice}\n");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

}

