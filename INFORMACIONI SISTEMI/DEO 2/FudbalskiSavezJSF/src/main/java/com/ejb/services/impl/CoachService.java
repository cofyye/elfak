package com.ejb.services.impl;

import java.util.ArrayList;
import java.util.List;

import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;

import com.ejb.services.ICoachService;
import com.jpa.classes.Response;
import com.jpa.entities.*;

@Stateless
public class CoachService implements ICoachService {
	@PersistenceContext(name = "FudbalskiSavezJSF")
	private EntityManager em;

	@Override
	public Response addCoach(Coach c) {
		try {
			
			if(c.getFirstName() == null || c.getFirstName().isEmpty()) {
				return new Response("Polje 'Ime' ne sme biti prazno!", "danger");
			}
			
			if(c.getLastName() == null || c.getLastName().isEmpty()) {
				return new Response("Polje 'Prezime' ne sme biti prazno!", "danger");
			}

			if(c.getJmbg() == null || c.getJmbg().isEmpty()) {
				return new Response("Polje 'JMBG' ne sme biti prazno!", "danger");
			}
			
			if(c.getUsername() == null || c.getUsername().isEmpty()) {
				return new Response("Polje 'Korisnicko Ime' ne sme biti prazno!", "danger");
			}
			
			if(c.getJmbg() == null || c.getJmbg().isEmpty()) {
				return new Response("Polje 'JMBG' ne sme biti prazno!", "danger");
			}
			
			if(c.getMobilePhone() == null || c.getMobilePhone().isEmpty()) {
				return new Response("Polje 'Mobilni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(c.getFixedPhone() == null || c.getFixedPhone().isEmpty()) {
				return new Response("Polje 'Fiksni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(c.getPlaceAddress() == null || c.getPlaceAddress().isEmpty()) {
				return new Response("Polje 'Adresa Stanovanja' ne sme biti prazno!", "danger");
			}
			
			if(c.getEmailAddress() == null || c.getEmailAddress().isEmpty()) {
				return new Response("Polje 'E-Mail Adresa' ne sme biti prazno!", "danger");
			}
			
			em.persist(c);
			
			return new Response("Uspesno ste dodali trenera!", "success");
		} catch (Exception e) {
			return new Response("Dogodila se greska prilikom dodavanja trenera u bazi podataka!", "danger");
		}
	}

	@Override
	public List<Coach> getListOfCoaches() {
		try {
	        TypedQuery<Coach> query = em.createQuery("SELECT c FROM Coach c", Coach.class);
	        List<Coach> coaches = query.getResultList();
	       
	        return coaches;
		} catch (Exception e) {
			return new ArrayList<Coach>();
		}
	}

	@Override
	public Response removeCoach(int id) {
		try {
			Coach c = em.find(Coach.class, id);
			
			if(c == null) {
				return new Response("Ovaj trener ne postoji!!", "danger");
			}
			
			em.remove(c);
			return new Response("Uspesno ste izbrisali trenera!", "success");
		} catch (Exception e) {
			return new Response("Dogodila se greska prilikom brisanja trenera iz baze podataka!", "danger");
		}
	}

	@Override
	public Coach getCoach(int id) {
		try {
			Coach c = em.find(Coach.class, id);
			
			return c;
		} catch (Exception e) {
			return null;
		}
	}

	@Override
	public Response editCoach(Coach c) {
		try {
			
			if(c.getFirstName() == null || c.getFirstName().isEmpty()) {
				return new Response("Polje 'Ime' ne sme biti prazno!", "danger");
			}
			
			if(c.getLastName() == null || c.getLastName().isEmpty()) {
				return new Response("Polje 'Prezime' ne sme biti prazno!", "danger");
			}

			if(c.getMobilePhone() == null || c.getMobilePhone().isEmpty()) {
				return new Response("Polje 'Mobilni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(c.getFixedPhone() == null || c.getFixedPhone().isEmpty()) {
				return new Response("Polje 'Fiksni Telefon' ne sme biti prazno!", "danger");
			}
			
			if(c.getPlaceAddress() == null || c.getPlaceAddress().isEmpty()) {
				return new Response("Polje 'Adresa Stanovanja' ne sme biti prazno!", "danger");
			}
			
			 em.createQuery("UPDATE Coach SET firstName = :firstName, lastName = :lastName, mobilePhone = :mobilePhone, fixedPhone = :fixedPhone, placeAddress = :placeAddress WHERE id = :id")
			 				.setParameter("firstName", c.getFirstName())
			 				.setParameter("lastName", c.getLastName())
			 				.setParameter("mobilePhone", c.getMobilePhone())
			 				.setParameter("fixedPhone", c.getFixedPhone())
			 				.setParameter("placeAddress", c.getPlaceAddress())
			 				.setParameter("id", c.getId())
			 				.executeUpdate();

			return new Response("Uspesno ste izmenili trenera!", "success");
		} catch (Exception e) {
			System.out.println(e.getMessage());
			return new Response("Dogodila se greska prilikom izmene trenera u bazi podataka!", "danger");
		}
	}
}