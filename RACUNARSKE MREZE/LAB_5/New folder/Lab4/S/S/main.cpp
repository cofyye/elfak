#include <iostream>
#include <WS2tcpip.h> //WS2tcpip.h
#pragma comment (lib, "ws2_32.lib") //ws2_32.lib
using namespace std;

void main() {
	WSADATA data;
	WORD version = MAKEWORD(2, 2);
	int wsOk = WSAStartup(version, &data);
	if (wsOk != 0) {
		cout << "Ne moze se pokrenuti Winsock!" << wsOk << endl;
	}
	SOCKET in = socket(AF_INET, SOCK_DGRAM, 0);
	sockaddr_in serverHint;
	serverHint.sin_addr.S_un.S_addr = ADDR_ANY;
	serverHint.sin_family = AF_INET;
	serverHint.sin_port = htons(4000);

	if (bind(in, (sockaddr*)&serverHint, sizeof(serverHint)) == SOCKET_ERROR) {
		cout << "Ne moze se odraditi bind za soket!" << WSAGetLastError() << endl;
		return;
	}

	sockaddr_in client;
	int clientLenght = sizeof(client);
	ZeroMemory(&client, clientLenght);

	char buf[1024];

	while (true) {
		ZeroMemory(buf, 1024);
		int bytesin = recvfrom(in, buf, 1024, 0, (sockaddr*)&client, &clientLenght);
		if (bytesin == SOCKET_ERROR) {
			cout << "Greska od strane klijenta!" << WSAGetLastError() << endl;
			continue;
		}

		char clientIp[256];
		ZeroMemory(clientIp, 256);

		inet_ntop(AF_INET, &client.sin_addr, clientIp, 256);
		cout << "Poruka primljena od " << clientIp << " : " << buf << endl;
	}
	closesocket(in);
	WSACleanup();
}