#include <iostream>
#include <WS2tcpip.h> //TCP transmission control protocol

#pragma comment (lib, "ws2_32.lib") //Ukljucivanje winsock v2 32bit-ne biblioteke

using namespace std;

void main()
{
	// Initialize winsock
	WSADATA wsData;
	WORD ver = MAKEWORD(2, 2); //Version 2.2

	int wsOk = WSAStartup(ver, &wsData);
	if (wsOk != 0)
	{
		cerr << "Can't Initialize winsock! Quitting" << endl;
		return;
	}

	// Create a socket
	SOCKET listening = socket(AF_INET, SOCK_STREAM, 0); //Zato sto smo stavili 0, protokol se bira na osnovu prethodnog parametra
	if (listening == INVALID_SOCKET)
	{
		cerr << "Can't create a socket! Quitting" << endl;
		return;
	}
	// Bind an ip address and port to the socket 
	sockaddr_in hint;
	hint.sin_family = AF_INET; //v4
	hint.sin_port = htons(54000); //host to network short. Number of port that is being listened to
	hint.sin_addr.S_un.S_addr = INADDR_ANY; //0.0.0.0//

	bind(listening, (sockaddr*)&hint, sizeof(hint));

	// Tell Winsock the socket is for listening
	listen(listening, SOMAXCONN); //SOMAXCONN maskimalna vrednost, maksimalna duzina reda u koji se smestaju konekcije koje cekaju da budu prihvacene

	// Wait for a connection

	sockaddr_in client;
	int clientSize = sizeof(client);

	SOCKET clientSocket = accept(listening, (sockaddr*)&client, &clientSize);
	if (clientSocket == INVALID_SOCKET)
	{
		cerr << "Can't create a client socket! Quitting" << endl;
		return;
	}

	char host[NI_MAXHOST]; //Client's remote name
	char service[NI_MAXHOST]; //Service (i.e port) the client is connect on

	ZeroMemory(host, NI_MAXHOST);
	ZeroMemory(service, NI_MAXHOST);

	if (getnameinfo((sockaddr*)&client, sizeof(client), host, NI_MAXHOST, service, NI_MAXSERV, 0) == 0)
	{
		cout << host << " connected on port " << service << endl;
	}
	else
	{
		inet_ntop(AF_INET, &client.sin_addr, host, NI_MAXHOST);
		cout << host << " connected on port " << ntohs(client.sin_port) << endl;
	}

	// Close listening socket

	closesocket(listening);

	// While loop: accpet and echo message back to client

	char buf[4096];

	while (true)
	{
		ZeroMemory(buf, 4096);

		// Wait for client to send data

		int bytesReceived = recv(clientSocket, buf, 4096, 0);
		if (bytesReceived == SOCKET_ERROR)
		{
			cerr << "Error in recv(). Quitting" << endl;
			break;
		}

		if (bytesReceived == 0)
		{
			cout << "Cliend disconnected " << endl;
			break;
		}

		// Echo message back to client

		send(clientSocket, buf, bytesReceived + 1, 0);

	}

	// Close the socket
	closesocket(clientSocket);
	// Cleanup winsock
	WSACleanup();
}