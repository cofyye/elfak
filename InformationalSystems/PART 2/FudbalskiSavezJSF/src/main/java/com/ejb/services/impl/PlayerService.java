package com.ejb.services.impl;

import java.util.ArrayList;
import java.util.List;

import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;

import com.ejb.services.IPlayerService;
import com.jpa.classes.Response;
import com.jpa.entities.*;

@Stateless
public class PlayerService implements IPlayerService {
	@PersistenceContext(name = "FudbalskiSavezJSF")
	private EntityManager em;

	@Override
	public Response addPlayer(Player p) {
		try {
			
			if(p.getFirstName() == null || p.getFirstName().isEmpty()) {
				return new Response("Polje 'Ime' ne sme biti prazno!", "danger");
			}
			
			if(p.getLastName() == null || p.getLastName().isEmpty()) {
				return new Response("Polje 'Prezime' ne sme biti prazno!", "danger");
			}

			if(p.getJmbg() == null || p.getJmbg().isEmpty()) {
				return new Response("Polje 'JMBG' ne sme biti prazno!", "danger");
			}
			
			if(p.getMobilePhone() == null || p.getMobilePhone().isEmpty()) {
				return new Response("Polje 'Mobilni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(p.getFixedPhone() == null || p.getFixedPhone().isEmpty()) {
				return new Response("Polje 'Fiksni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(p.getPlaceAddress() == null || p.getPlaceAddress().isEmpty()) {
				return new Response("Polje 'Adresa Stanovanja' ne sme biti prazno!", "danger");
			}
			
			if(p.getEmailAddress() == null || p.getEmailAddress().isEmpty()) {
				return new Response("Polje 'E-Mail Adresa' ne sme biti prazno!", "danger");
			}
			
			em.persist(p);
			
			return new Response("Uspesno ste dodali igraca!", "success");
		} catch (Exception e) {
			return new Response("Dogodila se greska prilikom dodavanja igraca u bazi podataka!", "danger");
		}
	}
	
	@Override
	public List<Player> getListOfPlayers() {
		try {
	        TypedQuery<Player> query = em.createQuery("SELECT p FROM Player p", Player.class);
	        List<Player> players = query.getResultList();
	       
	        return players;
		} catch (Exception e) {
			return new ArrayList<Player>();
		}
	}
	
	@Override
	public Response removePlayer(int id) {
		try {
			Player p = em.find(Player.class, id);
			
			if(p == null) {
				return new Response("Ovaj igrac ne postoji!!", "danger");
			}
			
			em.remove(p);
			return new Response("Uspesno ste izbrisali igraca!", "success");
		} catch (Exception e) {
			return new Response("Dogodila se greska prilikom brisanja igraca iz baze podataka!", "danger");
		}
	}
	
	@Override
	public Player getPlayer(int id) {
		try {
			Player p = em.find(Player.class, id);
			
			return p;
		} catch (Exception e) {
			return null;
		}
	}
	
	@Override
	public Response editPlayer(Player p) {
		try {
			
			if(p.getFirstName() == null || p.getFirstName().isEmpty()) {
				return new Response("Polje 'Ime' ne sme biti prazno!", "danger");
			}
			
			if(p.getLastName() == null || p.getLastName().isEmpty()) {
				return new Response("Polje 'Prezime' ne sme biti prazno!", "danger");
			}

			if(p.getMobilePhone() == null || p.getMobilePhone().isEmpty()) {
				return new Response("Polje 'Mobilni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(p.getFixedPhone() == null || p.getFixedPhone().isEmpty()) {
				return new Response("Polje 'Fiksni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(p.getPlaceAddress() == null || p.getPlaceAddress().isEmpty()) {
				return new Response("Polje 'Adresa Stanovanja' ne sme biti prazno!", "danger");
			}
			
			 em.createQuery("UPDATE Player SET firstName = :firstName, lastName = :lastName, mobilePhone = :mobilePhone, fixedPhone = :fixedPhone, placeAddress = :placeAddress WHERE id = :id")
			 				.setParameter("firstName", p.getFirstName())
			 				.setParameter("lastName", p.getLastName())
			 				.setParameter("mobilePhone", p.getMobilePhone())
			 				.setParameter("fixedPhone", p.getFixedPhone())
			 				.setParameter("placeAddress", p.getPlaceAddress())
			 				.setParameter("id", p.getId())
			 				.executeUpdate();

			return new Response("Uspesno ste izmenili igraca!", "success");
		} catch (Exception e) {
			System.out.println(e.getMessage());
			return new Response("Dogodila se greska prilikom izmene igraca u bazi podataka!", "danger");
		}
	}
}