package com.ejb.services;

import java.util.List;

import com.jpa.classes.Response;
import com.jpa.entities.Coach;

public interface ICoachService {
	public Response addCoach(Coach c);
	public List<Coach> getListOfCoaches();
	public Response removeCoach(int id);
	public Coach getCoach(int id);
	public Response editCoach(Coach c);
}

