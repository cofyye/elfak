package com.ejb.services.impl;

import java.util.ArrayList;
import java.util.List;

import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.NoResultException;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;

import com.ejb.services.IRequestService;
import com.jpa.classes.Response;
import com.jpa.classes.RequestAdditional;
import com.jpa.entities.*;

@Stateless
public class RequestService implements IRequestService {
	@PersistenceContext(name = "FudbalskiSavezJSF")
	private EntityManager em;

	@Override
	public Response addRequest(Request r) {
		try {
			
			Player player = em.find(Player.class, r.getPlayerId());
			
			if(player == null) {
				return new Response("Ovaj igrac ne postoji!", "danger");
			}
			
			Coach coach = em.find(Coach.class, r.getCoachId());
			
			if(coach == null) {
				return new Response("Ovaj trener ne postoji!", "danger");
			}
			
			TypedQuery<Request> query = em.createQuery("SELECT r FROM Request r WHERE player_id = :pid AND coach_id = :cid", Request.class);
			query.setParameter("pid", r.getPlayerId());
			query.setParameter("cid", r.getCoachId());
			
			try {
				query.getSingleResult();
				return new Response("Prijava za ovog igraca i trenera vec postoji, izmenite status!", "danger");
			} catch(NoResultException e) {}
			
			if(r.getClubName() == null || r.getClubName().isEmpty()) {
				return new Response("Polje 'Ime Kluba' ne sme biti prazno!", "danger");
			}
			
			em.persist(r);
			
			return new Response("Uspesno ste prijavili igraca!", "success");
		} catch (Exception e) {
			return new Response("Dogodila se greska prilikom prijave igrace!", "danger");
		}
	}

	@Override
	public List<RequestAdditional> getListOfRequests() {
		try {
	        TypedQuery<Request> query = em.createQuery("SELECT r FROM Request r", Request.class);
	        List<Request> requests = query.getResultList();
	        
	        List<RequestAdditional> rtnRequest = new ArrayList<>();
	        
	        for (Request elem: requests) {
	        	TypedQuery<Player> queryPlayer = em.createQuery("SELECT p FROM Player p WHERE id = :pid", Player.class);
	        	queryPlayer.setParameter("pid", elem.getPlayerId());
	        	Player p = queryPlayer.getSingleResult();
	        	
	        	TypedQuery<Coach> queryCoach = em.createQuery("SELECT c FROM Coach c WHERE id = :cid", Coach.class);
	        	queryCoach.setParameter("cid", elem.getCoachId());
	        	Coach c = queryCoach.getSingleResult();        	
	        	
	        	RequestAdditional reqAdt = new RequestAdditional();
	        	reqAdt.setRequest(elem);
	        	reqAdt.setPlayer(p);
	        	reqAdt.setCoach(c);

	        	rtnRequest.add(reqAdt);
	        }
	       
	        return rtnRequest;
		} catch (Exception e) {
			return new ArrayList<RequestAdditional>();
		}
	}

	@Override
	public Response removeRequest(int id) {
		try {
			Request r = em.find(Request.class, id);
			
			if(r == null) {
				return new Response("Ova prijava ne postoji!!", "danger");
			}
			
			em.remove(r);
			return new Response("Uspesno ste izbrisali prijavu za igraca!", "success");
		} catch (Exception e) {
			return new Response("Dogodila se greska prilikom brisanja prijave iz baze podataka!", "danger");
		}
	}

	@Override
	public Request getRequest(int id) {
		try {
			Request r = em.find(Request.class, id);
			
			return r;
		} catch (Exception e) {
			return null;
		}
	}

	@Override
	public Response editRequest(Request r) {
		try {
			
			if(r.getClubName() == null || r.getClubName().isEmpty()) {
				return new Response("Polje 'Ime Kluba' ne sme biti prazno!", "danger");
			}
			
			if(r.getStatus() == null || r.getStatus().isEmpty()) {
				return new Response("Polje 'Status' ne sme biti prazno!", "danger");
			}
			
			 em.createQuery("UPDATE Request SET clubName = :clubName, status = :status WHERE id = :id")
			 				.setParameter("clubName", r.getClubName())
			 				.setParameter("status", r.getStatus())
			 				.setParameter("id", r.getId())
			 				.executeUpdate();

			return new Response("Uspesno ste izmenili prijavu!", "success");
		} catch (Exception e) {
			System.out.println(e.getMessage());
			return new Response("Dogodila se greska prilikom izmene prijave u bazi podataka!", "danger");
		}
	}
}