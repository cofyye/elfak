package Test;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

import java.util.List;

import services.RequestService;
import services.IRequestService;
import models.Request;

import services.PlayerService;
import services.IPlayerService;
import models.Player;

import services.CoachService;
import services.ICoachService;
import models.Coach;

public class TestRequest {
	private static ICoachService coachService;
	private static IPlayerService playerService;
	private static IRequestService requestService;
	
	@BeforeClass
	public static void initTest() {
		coachService = new CoachService();
		playerService = new PlayerService();
		requestService = new RequestService();
		System.out.println("BeforeClass: Test initialized");
	}
	
	@Before
	public void testPrecondition() {
        Request r1 = requestService.getRequestByPlayerIdCoachId(41, 6);
        assertNull(r1);
        Request r2 = requestService.getRequestByPlayerIdCoachId(39, 6);
        assertNotNull(r2);
        
        System.out.println("Before: Request does not exist for Player ID : 41");
        System.out.println("Before: Request exist for Player ID : 39");
	}
	
	@Test
	public void test() {
		requestService.createRequest(41, 6, "IME KLUBA", "Neodobren");
	    requestService.updateRequestStatusById(requestService.getRequestByPlayerIdCoachId(39, 6).getId(), "Odobren");
	    
	    System.out.println("Test initialized");
	}
	
	@After
	public void testPostCondition() {
		Request r1 = requestService.getRequestByPlayerIdCoachId(41, 6);
		assertNotNull(r1);
		Request r2 = requestService.getRequestByPlayerIdCoachId(39, 6);
		assertEquals(r2.getStatus() ,  "Odobren");
		System.out.println("After: Request exist for Player ID : 41");
		System.out.println("After: Request with Player ID : 39 changed status to : Odobren ");
	}
	
	@AfterClass
	public static void clearTest() {
		requestService.deleteRequest(requestService.getRequestByPlayerIdCoachId(41, 6).getId());
		requestService.updateRequestStatusById(requestService.getRequestByPlayerIdCoachId(39, 6).getId(), "Neodobren");
		System.out.println();
		System.out.println("AfterClass: MySQL rolling back");
	}
}
