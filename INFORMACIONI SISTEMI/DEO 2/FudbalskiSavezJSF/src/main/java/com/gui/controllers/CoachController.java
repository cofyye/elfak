package com.gui.controllers;

import java.util.List;

import javax.annotation.PostConstruct;
import javax.ejb.EJB;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;

import com.ejb.services.ICoachService;
import com.jpa.classes.Response;
import com.jpa.entities.Coach;

@ManagedBean
public class CoachController {

	private Coach coach = new Coach();
	private List<Coach> coaches;
    private String message;
    private String messageType;
 
    @ManagedProperty(value = "#{param.id}")
    private int coachId;
    
    @EJB
	private ICoachService coachService;
    
    @PostConstruct
    public void init() {
        this.coaches = this.coachService.getListOfCoaches();
        
        if(this.coachId > 0) {
        	this.coach = this.coachService.getCoach(this.coachId);
        	if(this.coach == null) {
        		this.message = "Ovaj trener ne postoji!";
        		this.messageType = "danger";
        	}
        }
    }
    
	public Coach getCoach() {
		return this.coach;
	}

	public void setCoach(Coach c) {
		this.coach = c;
	}
	
    public String getMessage() {
        return this.message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
    
    public String getMessageType() {
        return this.messageType;
    }

    public void setMessageType(String messageType) {
        this.messageType = messageType;
    }
    
    public int getCoachId() {
        return this.coachId;
    }

    public void setCoachId(int cid) {
        this.coachId = cid;
    }
    
    public List<Coach> getCoaches() {
        return this.coaches;
    }

    public void setCoaches(List<Coach> c) {
        this.coaches = c;
    }
	
	public void addCoach(Coach c) {
		Response response = this.coachService.addCoach(c);
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
	
	public void removeCoach(int id) {
		Response response = this.coachService.removeCoach(id);
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
	
	public void editCoach(Coach c) {
		c.setId(this.coachId);
		Response response = this.coachService.editCoach(c);
		this.coach = c;
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
}