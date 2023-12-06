#include <iostream>
#include <string>
#include <winsock.h>

#define SERVER_IP "127.0.0.1"
#define SERVER_PORT 1337
#define BUFFER_SIZE 1024

using namespace std;

int main() {
    WSADATA wsData;

    SOCKET ClientSocket;

    if(WSAStartup(0x0202, &wsData) != 0) {
        cout << "[ERROR] Error while call function WSAStartup()" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    if((ClientSocket = socket(AF_INET, SOCK_DGRAM, 0)) == INVALID_SOCKET) {
        cout << "[ERROR] Error while call function socket()" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    sockaddr_in server;
    server.sin_family = AF_INET;
    server.sin_port = htons(SERVER_PORT);
    server.sin_addr.s_addr = inet_addr(SERVER_IP);
    int serverLen = sizeof(sockaddr);

    string buff;

    while(true) {
        cout << "Message : ";
        getline(cin, buff);
        if(sendto(ClientSocket, buff.c_str(), buff.length(), 0, (struct sockaddr *)&server, serverLen) != buff.length()) {
            cout << "[ERROR] Message from client isn't sended" << endl;
            break;
        }
    }

    closesocket(ClientSocket);
    WSACleanup();

    return 0;
}