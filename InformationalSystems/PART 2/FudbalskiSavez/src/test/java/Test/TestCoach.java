package Test;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

import services.CoachService;
import services.ICoachService;
import models.Coach;

public class TestCoach {
	private static ICoachService coachService;
	
	@BeforeClass
	public static void initTest() {
		coachService = new CoachService();
		System.out.println("BeforeClass: Test initialized");
	}
	
	@Before
	public void testPrecondition() {
        Coach coach1 = coachService.getCoachByJmbg("45453124321");
        assertNull(coach1);
        
        Coach coach2 = coachService.getCoachById(6);
        assertNotNull(coach2);
        
        System.out.println("Before: Coach does not exist with JMBG : 45453124321");
        System.out.println("Before: Coach exist with ID : 6");
	}
	
	@Test
	public void test() {
		coachService.createCoach("Filip", "Lakicevic", "45453124321", "06411243824", "027-3274395", "moja adresa", "cofyye", "mail1@a.com");
		coachService.updateCoachFixedPhone(6, "028-348938903");
		System.out.println("Test success");
	}
	
	@After
	public void testPostCondition() {
		 Coach coach1 = coachService.getCoachByJmbg("45453124321");
	     assertNotNull(coach1);
	     Coach coach2 = coachService.getCoachById(6);
	     assertEquals(coach2.getFixedPhone(), "028-348938903");
	     System.out.println("After: Coach exist with ID : 45453124321");
	     System.out.println("After: Coach's fixed phone with ID 6 is : 028-348938903");
	}
	
	@AfterClass
	public static void clearTest() {
		coachService.deleteCoach(coachService.getCoachByJmbg("45453124321").getId());
		coachService.updateCoachFixedPhone(6, "027-3274395");
		System.out.println("AfterClass: MySQL rolling back");
	}
}
