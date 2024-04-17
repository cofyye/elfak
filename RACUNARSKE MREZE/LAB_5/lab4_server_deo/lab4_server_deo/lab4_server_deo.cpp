#include <iostream>
#include <string>
#include <winsock.h>
#pragma comment(lib,"wsock32.lib")	//Linkujemo projekat i biblioteku
using namespace std;
void main()
{
	//Inicijalizacija Winsock strukture
	WSAData wsa;
	int wso = WSAStartup(0x0202, &wsa);	//Inicijalizacija WSA, verzija 2

	if (wso != 0)
	{
		cout << "Neuspesna inicijalizacija Winsocketa." << endl;
		return;
	}

	//Inicijalizujemo soket
	SOCKET sock;
	int addr_len, bytes_read;
	char recv_data[1024];	//Ono sto smo primili
	string send_data;	//Ono sto saljemo
	sockaddr_in server_addr, client_addr; //Pamte se adrese servera i klijenta

	//Proveravamo da li smo uspesno kreirali soket
	if ((sock = socket(AF_INET, SOCK_DGRAM, 0)) == SOCKET_ERROR)
	{
		exit(1);
	}

	//Povezujemo ip adresu servera i port za soket
	server_addr.sin_family = AF_INET;
	server_addr.sin_port = htons(50000); //Bilo koji port mozemo da stavimo da je veci od 1024
	server_addr.sin_addr.s_addr = htonl(INADDR_ANY);
	if (bind(sock, (sockaddr*)&server_addr, sizeof(sockaddr_in)) == SOCKET_ERROR)
	{
		exit(1);
	}

	addr_len = sizeof(sockaddr);

	while (true)
	{
		bytes_read = recvfrom(sock, recv_data, 1024, 0, (sockaddr*)&client_addr, &addr_len);
		//Ako ne primimo nista onda se nista i ne stampa
		if (bytes_read != 0)
		{
			cout << "klijent -> " << string(recv_data, 0, bytes_read) << endl;
		}

		cout << "server -> ";
		getline(cin, send_data);	//Cita se string sa tastature

		sendto(sock, send_data.c_str(), strlen(send_data.c_str()) + 1, 0, (sockaddr*)&client_addr, sizeof(sockaddr));   //Salje podatke klijentu
	}

	//Zatvaramo serverski soket
	closesocket(sock);

	//Gasimo WSA
	WSACleanup();

}