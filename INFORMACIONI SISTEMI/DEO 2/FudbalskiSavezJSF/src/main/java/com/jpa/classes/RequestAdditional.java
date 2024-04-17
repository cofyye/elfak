package com.jpa.classes;

import com.jpa.entities.*;

public class RequestAdditional {
    private Request request;
    private Player player;
    private Coach coach;
    
    public RequestAdditional() {}
    
	public RequestAdditional(Request request, Player player, Coach coach) {
		super();
		this.request = request;
		this.player = player;
		this.coach = coach;
	}

	public Request getRequest() {
		return request;
	}

	public void setRequest(Request request) {
		this.request = request;
	}

	public Player getPlayer() {
		return player;
	}

	public void setPlayer(Player player) {
		this.player = player;
	}

	public Coach getCoach() {
		return coach;
	}

	public void setCoach(Coach coach) {
		this.coach = coach;
	}
}
