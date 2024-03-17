package services;

import java.util.Date;
import java.util.List;

import models.Request;

public interface IRequestService {
	public void createRequest(int playerId, int coachId, String clubName, String status);
	public void updateRequestStatusById(int id, String status);
	public void deleteRequest(int id);
	public Request getRequest(int id);
	public List<Request> getRequestsByPlayerId(int id);
	public List<Request> getRequestsByCoachId(int id);
	public Request getRequestByPlayerIdCoachId(int pid, int cid);
	public List<Request> getRequestsByStatus(String status);
}
