///Project: SOA-A1-Pay-Stub_Generator
///File: main.c
///Date: 2018/01/06
///Author: Lauchlin Morrison
///

#include "registry.h"
#include <stdio.h>
#include "paystub.h"
#include "configFileIo.h"


//==================================================//
//					SOCKET METHODS					//
//==================================================//


SOCKET connectToRegistry(const char* registryIp, int registryPort)
{
	WSADATA wsa = { 0 };
	SOCKET sock = { 0 };
	struct sockaddr_in server = { 0 };

	/* Initialize windsock */
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		printf("Failed. Error Code : %d", WSAGetLastError());
		exit(-1);
	}

	/* Create socket */
	if ((sock = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET)
	{
		printf("Could not create socket : %d", WSAGetLastError());
		exit(-1);
	}

	/* Assign IP address and port of registry, run on TCP */
	server.sin_addr.s_addr = inet_addr(registryIp);
	server.sin_family = AF_INET;
	server.sin_port = htons(registryPort);

	/* Attempt connection to server */
	if (connect(sock, (struct sockaddr*)&server, sizeof(server)) < 0)
	{
		printf("Failed to connect to registry.");
		exit(-1);
	}

	return sock;
}

bool sendMessageToRegistry(SOCKET* socket, char* message)
{
	bool success = true;

	if (send(*socket, message, strlen(message), 0) < 0)
	{
		printf("Failed to send message to registry.");
		success = false;
	}
	return success;
}


char* getResponseFromRegistry(SOCKET* socket)
{
	int responseSize = 0;
	char* responseMessage = calloc(RESPONSE_BUFFER_SIZE, sizeof(char));

	/* Get the response from the server */
	if ((responseSize = recv(*socket, responseMessage, RESPONSE_BUFFER_SIZE, 0)) == SOCKET_ERROR)
	{
		printf("Failed to receive message from registry.");
		free(responseMessage);
	}

	return responseMessage;
}

//==========================================================//
//					COMMUNICATION METHODS					//
//==========================================================//

int main(int argc, char* argv)
{
	char* tagName = "PAYROLL";
	
	char d = EOM;

	/* Get configuration file key/value pairs */
	int keyValueCount = 0;
	KeyValuePair* configValues = loadConfigFile("config.cfg", &keyValueCount);

	/* Get host IP and port from the loaded configuration pairs and attempt connection */
	const char* ip = getConfigValue(configValues, keyValueCount, "host_ip");
	const char* port = getConfigValue(configValues, keyValueCount, "host_port");
	const char* teamId = getConfigValue(configValues, keyValueCount, "teamId");
	const char* serviceName = getConfigValue(configValues, keyValueCount, "serviceName");
	const char* numArgs = getConfigValue(configValues, keyValueCount, "numArgs");
	const char* numResponses = getConfigValue(configValues, keyValueCount, "numResponses");
	const char* description = getConfigValue(configValues, keyValueCount, "description");

	SOCKET socket = connectToRegistry(ip, atoi(port));

	/* Register the service with the teamName on the registry */
	const char* teamName = getConfigValue(configValues, keyValueCount, "teamName");
	char* messageToSend = NULL;

	

	closesocket(socket);
	WSACleanup();

	//sendMessageToRegistry(&sock, "")
	float generatedPay = 0;
	printf("Error code: %d \n", payStubGenerate("HOUR", 36.0f, 15.23f, 0, 0, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	printf("Error code: %d \n", payStubGenerate("FULL", 40.0f, 56000.00f, 0, 0, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	printf("Error code: %d \n", payStubGenerate("SEASON", 36.0f, 2.30f, 360, 0, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	printf("Error code: %d \n", payStubGenerate("CONTRACT", 40.0f, 9699.99f, 0, 4, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	return 0;
}
