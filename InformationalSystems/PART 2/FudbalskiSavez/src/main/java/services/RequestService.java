package services;

import java.util.Date;
import java.util.List;

import javax.ejb.Remote;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.NoResultException;
import javax.persistence.Persistence;
import javax.persistence.TypedQuery;

import models.Request;
import models.Coach;
import models.Player;

@Remote(ICoachService.class)
@Stateless
public class RequestService implements IRequestService {
	private EntityManager em;
	
	public RequestService()
	{
		   EntityManagerFactory emf = Persistence
	               .createEntityManagerFactory("FudbalskiSavezPU");
	       em = emf.createEntityManager();
	}
	
	@Override
	public void createRequest(int playerId, int coachId, String clubName, String status) {
	
		Player player = em.find(Player.class, playerId);
		
		if(player == null) {
			System.out.println("Player not found");
			return;
		}
		
		Coach coach = em.find(Coach.class, coachId);
		
		if(coach == null) {
			System.out.println("Coach not found");
			return;
		}
		
		TypedQuery<Request> query = em.createQuery("SELECT r FROM Request r WHERE player_id = :pid AND coach_id = :cid", Request.class);
		query.setParameter("pid", playerId);
		query.setParameter("cid", coachId);
		
		try {
			query.getSingleResult();
			System.out.println("This player and coach are avaliable and in progress...");
			return;
		} catch(NoResultException e) {}
		
		Request request = new Request(playerId, coachId, clubName, status);
		em.getTransaction().begin();
		em.persist(request);
        em.getTransaction().commit();
	}

	@Override
	public void deleteRequest(int id) {
    	Request request = em.find(Request.class, id);
    	
    	if(request == null) {
			System.out.println("Request not found");
			return;
    	}
    	
    	em.getTransaction().begin();
    	em.remove(request);
    	em.getTransaction().commit();
	}
	
	@Override
	public void updateRequestStatusById(int id, String status) {
		Request request = em.find(Request.class, id);
		
    	if(request == null) {
			System.out.println("Request not found");
			return;
    	}
		
		em.getTransaction().begin();
		request.setStatus(status);
		em.getTransaction().commit();
		
	}
	
	@Override
	public Request getRequest(int id) {
		Request request = em.find(Request.class, id);
		return request;
	}
	
	@Override
	public List<Request> getRequestsByPlayerId(int playerId) {
		TypedQuery<Request> query = em.createQuery("SELECT r FROM Request r WHERE player_id = :pid", Request.class);
		query.setParameter("pid", playerId);
        List<Request> requests = query.getResultList();	
        
        return requests;
	}
	
	@Override
	public List<Request> getRequestsByCoachId(int coachId) {
		TypedQuery<Request> query = em.createQuery("SELECT r FROM Request r WHERE coach_id = :cid", Request.class);
		query.setParameter("cid", coachId);
        List<Request> requests = query.getResultList();	
        
        return requests;
	}
	
	@Override
	public List<Request> getRequestsByStatus(String status) {
		TypedQuery<Request> query = em.createQuery("SELECT r FROM Request r WHERE status = :status", Request.class);
		query.setParameter("status", status);
        List<Request> requests = query.getResultList();	
        
        return requests;
	}
	
	@Override
	public Request getRequestByPlayerIdCoachId(int pid, int cid) {
		TypedQuery<Request> query = em.createQuery("SELECT r FROM Request r WHERE player_id = :pid AND coach_id = :cid", Request.class);
		query.setParameter("pid", pid);
		query.setParameter("cid", cid);
		
		try {
			return query.getSingleResult();
		} catch(Exception e) {
			return null;
		}
	}
}
