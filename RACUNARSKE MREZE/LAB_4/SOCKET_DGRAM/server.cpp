#include <iostream>
#include <string>
#include <winsock.h>

#define SERVER_PORT 1337
#define BUFFER_SIZE 1024

using namespace std;

int main() {
    WSADATA wsData;

    SOCKET ListenSocket;

    if(WSAStartup(0x0202, &wsData) != 0) {
        cout << "[ERROR] Error while call function WSAStartup()" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    if((ListenSocket = socket(AF_INET, SOCK_DGRAM, 0)) == INVALID_SOCKET) {
        cout << "[ERROR] Error while call function socket()" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    sockaddr_in server;
    server.sin_addr.s_addr = INADDR_ANY;
    server.sin_family = AF_INET;
    server.sin_port = htons(SERVER_PORT);

    if(bind(ListenSocket, (struct sockaddr *)&server, sizeof(server)) == SOCKET_ERROR) {
        cout << "[ERROR] Error while call function bind()" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    sockaddr_in client;
    int clientLen = sizeof(sockaddr);

    cout << "[INFO] Server is started" << endl;

    while(true) {
        char buff[BUFFER_SIZE];
        int receivedMsgSize = recvfrom(ListenSocket, buff, sizeof(buff), 0, (struct sockaddr *)&client, &clientLen);

        if(receivedMsgSize < 0) {
            cout << "[ERROR] Message from client isn't received" << endl;
            break;
        }

        cout << "[" << inet_ntoa(client.sin_addr) << ":" << client.sin_port << "] ";
        
        int i;
        for(i = 0; i < receivedMsgSize; i++) {
            cout << buff[i];
        }
        cout << endl;

        if(sendto(ListenSocket, buff, receivedMsgSize, 0, (struct sockaddr *)&client, sizeof(sockaddr)) != receivedMsgSize) {
            cout << "[ERROR] Message from server isn't sended" << endl;
            break;
        }
    }

    closesocket(ListenSocket);
    WSACleanup();

    return 0;
}