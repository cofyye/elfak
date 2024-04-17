#include <iostream>
#include <string>
#include <winsock.h>

#define SERVER_ADDRESS "127.0.0.1"
#define SERVER_PORT 1337

using namespace std;

int main() {
    WSADATA wsaData;

    SOCKET ClientSocket;

    // Starting a socket
    if(WSAStartup(0x0202, &wsaData) != 0) {
        cout << "[ERROR] Error while starting a Socket" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }
    
    // Creating a listening socket
    if((ClientSocket = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET) {
        cout << "[ERROR] Error while creating a listening socket" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    // Define server address to connecting on socket
    sockaddr_in serverIp;
    serverIp.sin_addr.s_addr = inet_addr(SERVER_ADDRESS);
    serverIp.sin_family = AF_INET;
    serverIp.sin_port = htons(SERVER_PORT);

    // Connect to server socket
    if(connect(ClientSocket, (struct sockaddr *)&serverIp, sizeof(serverIp)) == SOCKET_ERROR) {
        cout << "[ERROR] Error while connecting to socket" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    // Transfering data
    while(true) {
        string message;
        cout << "Message : ";
        getline(cin, message);
        cout << endl;
        if(send(ClientSocket, message.c_str(), message.length(), 0) != message.length()) {
            cout << "[ERROR] Error while sending a message" << endl;
            continue;
        }
    }

    closesocket(ClientSocket);
    WSACleanup();

    return 0;
}