#include <iostream>
#include <string>
#include <winsock.h>

#define SERVER_PORT 1337
#define BUFFER_SIZE 1024

using namespace std;

int main() {
    WSADATA wsaData;

    SOCKET ListenSocket;
    SOCKET ClientSocket;

    // Starting a socket
    if(WSAStartup(0x0202, &wsaData) != 0) {
        cout << "[ERROR] Error while starting a Socket" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    // Creating a listening socket
    if((ListenSocket = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET) {
        cout << "[ERROR] Error while creating a listening socket" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    // Define server address to listening socket
    sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_addr.s_addr = INADDR_ANY;
    serverAddr.sin_port = htons(SERVER_PORT);

    // Binding ip address to listening socket
    if(bind(ListenSocket, (struct sockaddr *)&serverAddr, sizeof(serverAddr)) == SOCKET_ERROR) {
        cout << "[ERROR] Error while binding a ip address to socket" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    // Set listening connections on socket
    if((listen(ListenSocket, 3)) < 0) {
        cout << "[ERROR] Error while listening a connections" << endl;
        WSACleanup();
        system("pause");
        exit(EXIT_FAILURE);
    }

    cout << "[INFO] Socket server is started" << endl;

    // Listening connections in progress
    while(true) {
            // Define client address to listening socket
            sockaddr_in clientAddr;
            int clientAddrLen = sizeof(struct sockaddr_in);
            if((ClientSocket = accept(ListenSocket, (struct sockaddr *)&clientAddr, &clientAddrLen)) < 0) {
                cout << "[ERROR] Error while accepting client socket" << endl;
                continue;
            }

            // Accept data from client
            char buff[BUFFER_SIZE];
            int receivedMsgSize = recv(ClientSocket, buff, sizeof(buff), 0);

            if(receivedMsgSize < 0) {
                cout << "[ERROR] Message from client does not received" << endl;
                continue;
            }

            cout << "[FROM_CLIENT] ";
            for(int i = 0; i < receivedMsgSize; i++) {
                cout << buff[i] << " ";
            }

            cout << endl;

            while(receivedMsgSize > 0) {
                if(send(ClientSocket, buff, receivedMsgSize, 0) != receivedMsgSize) {
                    cout << "[ERROR] Error while sending message to client" << endl;
                    continue;
                }

                if((receivedMsgSize = recv(ClientSocket, buff, sizeof(buff), 0)) < 0) {
                    cout << "[ERROR] Message from client does not received" << endl;
                    continue;
                }

                cout << "[FROM_CLIENT] ";
                for(int i = 0; i < receivedMsgSize; i++) {
                    cout << buff[i] << " ";
                }

                cout << endl;
            }
            closesocket(ClientSocket);
    }

    closesocket(ListenSocket);
    WSACleanup();

    return 0;
}