
#ifndef __REGISTRY_H__
#define __REGISTRY_H__

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#define BOM (char)11
#define EOS (char)13
#define EOM (char)28
#define RESPONSE_BUFFER_SIZE 2000

#include<stdio.h>
#include<winsock2.h>
#include <stdbool.h>

#pragma comment(lib,"ws2_32.lib") //Winsock Library
#pragma warning(disable:4996)

SOCKET connectToRegistry(const char* registryIp, int registryPort);
bool sendMessageToRegistry(SOCKET* socket, char* message);
void registryRegister(SOCKET* socket, const char* teamName);


#endif