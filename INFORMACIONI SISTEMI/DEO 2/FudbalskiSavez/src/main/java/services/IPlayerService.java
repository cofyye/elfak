package services;

import models.Player;

public interface IPlayerService {
	public void createPlayer(
				String firstName, String lastName, 
				String jmbg, String mobilePhone, String fixedPhone,
				String placeAddress, String emailAddress
			);
	public String listAllPlayers();
	public void deletePlayer(int id);
	public void updatePlayerMobilePhone(int id, String mobilePhone);
	public void updatePlayerFixedPhone(int id, String fixedPhone);
	public void updatePlayerPlaceAddress(int id, String placeAddress);
	public void updatePlayerEmailAddress(int id, String emailAddress);
	public Player getPlayerById(int id);
	public Player getPlayerByEmail(String emailAddress);
	public Player getPlayerByJmbg(String jmbg);
}
