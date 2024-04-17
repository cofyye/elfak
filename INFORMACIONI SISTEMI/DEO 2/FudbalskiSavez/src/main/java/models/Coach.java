package models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="coaches") 
public class Coach {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private int id;
 
    @Column(name = "first_name")
    private String firstName;
 
    @Column(name = "last_name")
    private String lastName;
 
    @Column(name = "jmbg")
    private String jmbg;

    @Column(name = "mobile_phone")
    private String mobilePhone;
   
    @Column(name = "fixed_phone")
    private String fixedPhone;
    
    @Column(name = "place_address")
    private String placeAddress;
    
    @Column(name = "username")
    private String username;
    
	@Column(name = "email_address")
    private String emailAddress;
    
    public Coach() {}
    
    public Coach(String firstName, String lastName, String jmbg, String mobilePhone, String fixedPhone,
			String placeAddress, String username, String emailAddress) {
		super();
		this.firstName = firstName;
		this.lastName = lastName;
		this.jmbg = jmbg;
		this.mobilePhone = mobilePhone;
		this.fixedPhone = fixedPhone;
		this.placeAddress = placeAddress;
		this.username = username;
		this.emailAddress = emailAddress;
	}



	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getFirstName() {
		return firstName;
	}

	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}

	public String getLastName() {
		return lastName;
	}

	public void setLastName(String lastName) {
		this.lastName = lastName;
	}

	public String getJmbg() {
		return jmbg;
	}

	public void setJmbg(String jmbg) {
		this.jmbg = jmbg;
	}

	public String getMobilePhone() {
		return mobilePhone;
	}

	public void setMobilePhone(String mobilePhone) {
		this.mobilePhone = mobilePhone;
	}

	public String getFixedPhone() {
		return fixedPhone;
	}

	public void setFixedPhone(String fixedPhone) {
		this.fixedPhone = fixedPhone;
	}

	public String getPlaceAddress() {
		return placeAddress;
	}

	public void setPlaceAddress(String placeAddress) {
		this.placeAddress = placeAddress;
	}

	public String getUsername() {
		return username;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public String getEmailAddress() {
		return emailAddress;
	}

	public void setEmailAddress(String emailAddress) {
		this.emailAddress = emailAddress;
	}
}
