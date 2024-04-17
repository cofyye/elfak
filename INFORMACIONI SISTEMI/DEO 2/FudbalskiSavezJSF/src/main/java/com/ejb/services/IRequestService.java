package com.ejb.services;

import java.util.List;

import com.jpa.classes.Response;
import com.jpa.classes.RequestAdditional;
import com.jpa.entities.Request;

public interface IRequestService {
	public Response addRequest(Request r);
	public List<RequestAdditional> getListOfRequests();
	public Response removeRequest(int id);
	public Request getRequest(int id);
	public Response editRequest(Request r);
}

