
import java.util.Date;
import java.util.List;

import models.Coach;
import models.Player;
import models.Request;
import services.CoachService;
import services.PlayerService;
import services.RequestService;

public class App {
	public static void main(String[] args) {
		PlayerService ps = new PlayerService();
		CoachService cs = new CoachService();
		RequestService rs = new RequestService();
		
		// Kreiranje igraca
		ps.createPlayer("Filip", "Lakicevic", "45453124321", "06411243824", "027-3274395", "moja adresa", "mail1@a.com");
		//ps.createPlayer("Filip", "Lakicevic", "45453124322", "06411243824", "027-3274395", "moja adresa", "mail2@a.com");
		//ps.createPlayer("Filip", "Lakicevic", "45453124323", "06411243824", "027-3274395", "moja adresa", "mail3@a.com");
		//ps.createPlayer("Filip", "Lakicevic", "45453124324", "06411243824", "027-3274395", "moja adresa", "mail4@a.com");
		
		// Vracanje igraca
		//Player p = ps.getPlayerByEmail("mail4@a.com");
		//if(p != null) {
			//System.out.println(p.getEmailAddress());			
		//}
		//if(p != null) System.out.println(p.getId() + " " + p.getFirstName() + " " + p.getLastName());
		
		// Brisanje igraca
		// ps.deletePlayer(p.getId());
		
		// Izmena emaila
		// ps.updatePlayerEmailAddress(ps.getPlayerByJmbg("45453124322").getId(), "novi@mail.net");
		
		// Vracanje liste igraca
		// System.out.println(ps.listAllPlayers());
		
		
		
		
		// Kreiranje trenera
		//cs.createCoach("Trener", "Trenerovic", "45453124321", "06411243824", "027-3274395", "moja adresa", "trener1", "mail1@a.com");
		//cs.createCoach("Trener", "Trenerovic", "45453124322", "06411243824", "027-3274395", "moja adresa", "trener2", "mail2@a.com");
		//cs.createCoach("Trener", "Trenerovic", "45453124323", "06411243824", "027-3274395", "moja adresa", "trener3", "mail3@a.com");
		//cs.createCoach("Trener", "Trenerovic", "45453124324", "06411243824", "027-3274395", "moja adresa", "trener4", "mail4@a.com");
		
		// Vracanje trenera
		//Coach c = cs.getCoachByUsername("trener2");
		//if(c != null) {
			//System.out.println(c.getEmailAddress() + " " + c.getUsername() + " " + c.getId());			
		//}
		
		// Brisanje trenera
		//cs.deleteCoach(c.getId());
		
		// Izmena usernamea
		//cs.updateCoachUsername(cs.getCoachByJmbg("45453124323").getId(), "novi@mail.net");
		
		// Vracanje liste trenera
		//System.out.println(cs.listAllCoaches());
		
		
		
		
		
		
		// Kreiranje zahteva za klub
		//rs.createRequest(13, 3, "JAO MAMA FK", "Neodobren", new Date());
		//rs.createRequest(15, 3, "JAO MAMA FK", "Neodobren", new Date());
		//rs.createRequest(13, 1, "JAO MAMA FK", "Neodobren", new Date());
		//rs.createRequest(15, 4, "JAO MAMA FK", "Neodobren", new Date());
		
		// Vracanje zahteva
		//Request r = rs.getRequest(3);
		//if(r != null) {
			//System.out.println(r.getStatus());			
		//}
		
		// Brisanje zahteva
		//rs.deleteRequest(r.getId());
		
		// Izmena zahteva
		//rs.updateRequestStatusById(4, "Odobren");
		
		// Vracanje detaljnog zahteva
		//List<Request> req = rs.getRequestsByStatus("Odobren");
		//for(Request obj : req) {
			//System.out.println(obj.getCoachId() + " " + obj.getPlayerId() + "\n");	
		//}
	}
}
