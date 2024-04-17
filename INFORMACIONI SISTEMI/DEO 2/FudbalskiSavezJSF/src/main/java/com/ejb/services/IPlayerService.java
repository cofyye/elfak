package com.ejb.services;

import java.util.List;

import com.jpa.classes.Response;
import com.jpa.entities.Player;

public interface IPlayerService {
	public Response addPlayer(Player p);
	public List<Player> getListOfPlayers();
	public Response removePlayer(int id);
	public Player getPlayer(int id);
	public Response editPlayer(Player p);
}

