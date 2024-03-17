package services;

import models.Coach;

public interface ICoachService {
	public void createCoach(
				String firstName, String lastName, 
				String jmbg, String mobilePhone, String fixedPhone,
				String placeAddress, String username, String emailAddress
			);
	public String listAllCoaches();
	public void deleteCoach(int id);
	public void updateCoachMobilePhone(int id, String mobilePhone);
	public void updateCoachFixedPhone(int id, String fixedPhone);
	public void updateCoachPlaceAddress(int id, String placeAddress);
	public void updateCoachEmailAddress(int id, String emailAddress);
	public void updateCoachUsername(int id, String username);
	public Coach getCoachById(int id);
	public Coach getCoachByEmail(String emailAddress);
	public Coach getCoachByUsername(String username);
	public Coach getCoachByJmbg(String jmbg);
}

