package Test;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

import services.PlayerService;
import services.IPlayerService;
import models.Player;

public class TestPlayer {
	private static IPlayerService playerService;
	
	@BeforeClass
	public static void initTest() {
		playerService = new PlayerService();
		System.out.println("BeforeClass: Test initialized");
	}
	
	@Before
	public void testPrecondition() {
        Player player1 = playerService.getPlayerByJmbg("45453124321");
        assertNull(player1);
        
        Player player2 = playerService.getPlayerById(41);
        assertNotNull(player2);
        
        System.out.println("Before: Player does not exist with JMBG : 45453124321");
        System.out.println("Before: Player exist with ID : 41");
	}
	
	@Test
	public void test() {
		playerService.createPlayer("Filip", "Lakicevic", "45453124321", "06411243824", "027-3274395", "moja adresa", "mail1@a.com");
		playerService.updatePlayerFixedPhone(41, "028-348938903");
		System.out.println("Test success");
	}
	
	@After
	public void testPostCondition() {
		 Player player1 = playerService.getPlayerByJmbg("45453124321");
	     assertNotNull(player1);
	     Player player2 = playerService.getPlayerById(41);
	     assertEquals(player2.getFixedPhone(), "028-348938903");
	     System.out.println("After: Player exist with ID : 45453124321");
	     System.out.println("After: Player's fixed phone with ID 41 is : 028-348938903");
	}
	
	@AfterClass
	public static void clearTest() {
		playerService.deletePlayer(playerService.getPlayerByJmbg("45453124321").getId());
		playerService.updatePlayerFixedPhone(41, "027-3274395");
		System.out.println("AfterClass: MySQL rolling back");
	}
}
