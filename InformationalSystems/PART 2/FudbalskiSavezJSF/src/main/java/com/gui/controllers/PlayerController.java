package com.gui.controllers;

import java.util.List;

import javax.annotation.PostConstruct;
import javax.ejb.EJB;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;

import com.ejb.services.IPlayerService;
import com.jpa.classes.Response;
import com.jpa.entities.Player;

@ManagedBean
public class PlayerController {

	private Player player = new Player();
	private List<Player> players;
    private String message;
    private String messageType;
    
    @ManagedProperty(value = "#{param.id}")
    private int playerId;
 
    @EJB
	private IPlayerService playerService;
    
    @PostConstruct
    public void init() {
        this.players = this.playerService.getListOfPlayers();
        
        if(this.playerId > 0) {
        	this.player = this.playerService.getPlayer(this.playerId);
        	if(this.player == null) {
        		this.message = "Ovaj igrac ne postoji!";
        		this.messageType = "danger";
        	}
        }
    }

	public Player getPlayer() {
		return this.player;
	}

	public void setPlayer(Player p) {
		this.player = p;
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
    
    public int getPlayerId() {
        return this.playerId;
    }

    public void setPlayerId(int pid) {
        this.playerId = pid;
    }
    
    public List<Player> getPlayers() {
        return this.players;
    }

    public void setPlayers(List<Player> p) {
        this.players = p;
    }
	
	public void addPlayer(Player p) {
		Response response = this.playerService.addPlayer(p);
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
	
	public void removePlayer(int id) {
		Response response = this.playerService.removePlayer(id);
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
	
	public void editPlayer(Player p) {
		p.setId(this.playerId);
		Response response = this.playerService.editPlayer(p);
		this.player = p;
		this.message = response.getMessage();
		this.messageType = response.getStatus();
	}
}