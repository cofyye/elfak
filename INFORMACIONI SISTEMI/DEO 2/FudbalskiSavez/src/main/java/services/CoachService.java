package services;

import java.util.List;

import javax.ejb.Remote;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.NoResultException;
import javax.persistence.Persistence;
import javax.persistence.TypedQuery;

import models.Coach;

@Remote(ICoachService.class)
@Stateless
public class CoachService implements ICoachService {
	private EntityManager em;
	
	public CoachService()
	{
		   EntityManagerFactory emf = Persistence
	               .createEntityManagerFactory("FudbalskiSavezPU");
	       em = emf.createEntityManager();
	}
	
	@Override
	public void createCoach(String firstName, String lastName, String jmbg, String mobilePhone, String fixedPhone,
			String placeAddress, String username, String emailAddress) {

		Coach coach = new Coach(firstName, lastName, jmbg, mobilePhone, fixedPhone, placeAddress, username, emailAddress);
		em.getTransaction().begin();
		em.persist(coach);
        em.getTransaction().commit();
	}

	@Override
	public String listAllCoaches() {
		String result = "";
	      
        TypedQuery<Coach> query = em.createQuery("SELECT c FROM Coach c", Coach.class);
        List<Coach> coaches = query.getResultList();
        
        for (Coach elem: coaches) {
        	result += "==============================================================\n";
        	result += "ID: " + elem.getId() + "\n"; 
        	result += "First Name: " + elem.getFirstName() + "\n"; 
        	result += "Last Name: " + elem.getLastName() + "\n"; 
        	result += "Email: " + elem.getEmailAddress() + "\n"; 
        	result += "Username: " + elem.getUsername() + "\n"; 
        	result += "Mobile Phone: " + elem.getMobilePhone() + "\n"; 
        	result += "Fixed Phone: " + elem.getFixedPhone() + "\n";
        	result += "JMBG: " + elem.getJmbg() + "\n";
        	result += "Place Address: " + elem.getPlaceAddress() + "\n";
        }
        result += "==============================================================\n";
        
        return result;
	}

	@Override
	public void deleteCoach(int id) {
    	Coach coach = em.find(Coach.class, id);
    	em.getTransaction().begin();
    	em.remove(coach);
    	em.getTransaction().commit();
	}

	@Override
	public void updateCoachMobilePhone(int id, String mobilePhone) {
		Coach coach = em.find(Coach.class, id);
		em.getTransaction().begin();
		coach.setMobilePhone(mobilePhone);
		em.getTransaction().commit();
	}

	@Override
	public void updateCoachFixedPhone(int id, String fixedPhone) {
		Coach coach = em.find(Coach.class, id);
		em.getTransaction().begin();
		coach.setFixedPhone(fixedPhone);
		em.getTransaction().commit();
	}

	@Override
	public void updateCoachPlaceAddress(int id, String placeAddress) {
		Coach coach = em.find(Coach.class, id);
		em.getTransaction().begin();
		coach.setPlaceAddress(placeAddress);
		em.getTransaction().commit();
	}

	@Override
	public void updateCoachEmailAddress(int id, String emailAddress) {
		Coach coach = em.find(Coach.class, id);
		em.getTransaction().begin();
		coach.setEmailAddress(emailAddress);
		em.getTransaction().commit();
	}

	@Override
	public void updateCoachUsername(int id, String username) {
		Coach coach = em.find(Coach.class, id);
		em.getTransaction().begin();
		coach.setUsername(username);
		em.getTransaction().commit();
	}
	
	@Override
	public Coach getCoachById(int id) {
		Coach coach = em.find(Coach.class, id);
		return coach;
	}
	
	@Override
	public Coach getCoachByEmail(String emailAddress) {
		TypedQuery<Coach> query = em.createQuery("SELECT c FROM Coach c WHERE email_address = :email", Coach.class);
		query.setParameter("email", emailAddress);
		
        try {
            return query.getSingleResult();
        } catch (NoResultException e) {
            return null;
        }
	}
	
	@Override
	public Coach getCoachByUsername(String username) {
		TypedQuery<Coach> query = em.createQuery("SELECT c FROM Coach c WHERE username = :username", Coach.class);
		query.setParameter("username", username);
		
        try {
            return query.getSingleResult();
        } catch (NoResultException e) {
            return null;
        }
	}
	
	@Override
	public Coach getCoachByJmbg(String jmbg) {
		TypedQuery<Coach> query = em.createQuery("SELECT c FROM Coach c WHERE jmbg = :jmbg", Coach.class);
		query.setParameter("jmbg", jmbg);
		
        try {
            return query.getSingleResult();
        } catch (NoResultException e) {
            return null;
        }
	}

}
