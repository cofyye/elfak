package services;

import java.util.List;

import javax.ejb.Remote;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.NoResultException;
import javax.persistence.Persistence;
import javax.persistence.TypedQuery;

import models.Player;

@Remote(IPlayerService.class)
@Stateless
public class PlayerService implements IPlayerService {
	private EntityManager em;
	
	public PlayerService()
	{
		   EntityManagerFactory emf = Persistence
	               .createEntityManagerFactory("FudbalskiSavezPU");
	       em = emf.createEntityManager();
	}
	
	@Override
	public void createPlayer(String firstName, String lastName, String jmbg, String mobilePhone,
			String fixedPhone, String placeAddress, String emailAddress) {

		Player player = new Player(firstName, lastName, jmbg, mobilePhone, fixedPhone, placeAddress, emailAddress);
		em.getTransaction().begin();
		player = em.merge(player);
		em.persist(player);
        em.getTransaction().commit();
	}

	@Override
	public String listAllPlayers() {
		String result = "";
	      
        TypedQuery<Player> query = em.createQuery("SELECT p FROM Player p", Player.class);
        List<Player> players = query.getResultList();
        
        for (Player elem: players) {
        	result += "==============================================================\n";
        	result += "ID: " + elem.getId() + "\n"; 
        	result += "First Name: " + elem.getFirstName() + "\n"; 
        	result += "Last Name: " + elem.getLastName() + "\n"; 
        	result += "Email: " + elem.getEmailAddress() + "\n";
        	result += "Mobile Phone: " + elem.getMobilePhone() + "\n"; 
        	result += "Fixed Phone: " + elem.getFixedPhone() + "\n";
        	result += "JMBG: " + elem.getJmbg() + "\n";
        	result += "Place Address: " + elem.getPlaceAddress() + "\n";
        }
        result += "==============================================================\n";
        
        return result;
	}

	@Override
	public void deletePlayer(int id) {
    	Player player = em.find(Player.class, id);
    	em.getTransaction().begin();
    	em.remove(player);
    	em.getTransaction().commit();
	}

	@Override
	public void updatePlayerMobilePhone(int id, String mobilePhone) {
		Player player = em.find(Player.class, id);
		em.getTransaction().begin();
		player.setMobilePhone(mobilePhone);
		em.getTransaction().commit();
	}

	@Override
	public void updatePlayerFixedPhone(int id, String fixedPhone) {
		Player player = em.find(Player.class, id);
		em.getTransaction().begin();
		player.setFixedPhone(fixedPhone);
		em.getTransaction().commit();
	}

	@Override
	public void updatePlayerPlaceAddress(int id, String placeAddress) {
		Player player = em.find(Player.class, id);
		em.getTransaction().begin();
		player.setPlaceAddress(placeAddress);
		em.getTransaction().commit();
	}

	@Override
	public void updatePlayerEmailAddress(int id, String emailAddress) {
		Player player = em.find(Player.class, id);
		em.getTransaction().begin();
		player.setEmailAddress(emailAddress);
		em.getTransaction().commit();
	}

	@Override
	public Player getPlayerById(int id) {
		Player player = em.find(Player.class, id);
		return player;
	}
	
	@Override
	public Player getPlayerByEmail(String emailAddress) {
		TypedQuery<Player> query = em.createQuery("SELECT p FROM Player p WHERE email_address = :email", Player.class);
		query.setParameter("email", emailAddress);
		
        try {
            return query.getSingleResult();
        } catch (NoResultException e) {
            return null;
        }
	}
	
	@Override
	public Player getPlayerByJmbg(String jmbg) {
		TypedQuery<Player> query = em.createQuery("SELECT p FROM Player p WHERE jmbg = :jmbg", Player.class);
		query.setParameter("jmbg", jmbg);
		
        try {
            return query.getSingleResult();
        } catch (NoResultException e) {
            return null;
        }
	}
}
