package com.gui.controllers;

import java.util.List;

import javax.annotation.PostConstruct;
import javax.ejb.EJB;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;

import com.ejb.services.IRequestService;
import com.jpa.classes.Response;
import com.jpa.classes.RequestAdditional;
import com.jpa.entities.Request;

@ManagedBean
public class RequestController {

	private Request request = new Request();
	private List<RequestAdditional> requests;
    private String message;
    private String messageType;
 
    @ManagedProperty(value = "#{param.id}")
    private int requestId;
    
    @EJB
	private IRequestService requestService;

    @PostConstruct
    public void init() {
        this.requests = this.requestService.getListOfRequests();
        
        if(this.requestId > 0) {
        	this.request = this.requestService.getRequest(this.requestId);
        	if(this.request == null) {
        		this.message = "Ova prijava ne postoji!";
        		this.messageType = "danger";
        	}
        }
    }
    
	public Request getRequest() {
		return this.request;
	}

	public void setRequest(Request r) {
		this.request = r;
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
	
    public int getRequestId() {
        return this.requestId;
    }

    public void setRequestId(int rid) {
        this.requestId = rid;
    }
    
    public List<RequestAdditional> getRequests() {
        return this.requests;
    }

    public void setRequests(List<RequestAdditional> r) {
        this.requests = r;
    }
    
	public void addRequest(Request r) {
		r.setStatus("Neodobren");
		Response response = this.requestService.addRequest(r);
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
	
	public void removeRequest(int id) {
		Response response = this.requestService.removeRequest(id);
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
	
	public void editRequest(Request r) {
		r.setId(this.requestId);
		Response response = this.requestService.editRequest(r);
		this.request = r;
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
}