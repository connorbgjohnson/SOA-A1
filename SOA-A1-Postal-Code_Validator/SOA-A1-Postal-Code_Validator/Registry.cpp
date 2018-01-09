///Project: SOA-A1-Postal-Code_Validator
///File: Registry.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Contains methods to connect to the registry.

#pragma once

#include "Registry.h"
#include <string.h>
#include "split.h"
#include "PostalCodeService.h"

/// <summary>
/// Creates the registry object.
/// </summary>
Registry::Registry()
{
	_configReader;
}

/// <summary>
/// Connects the specified registry ip.
/// </summary>
/// <param name="registryIp">The registry ip.</param>
/// <param name="registryPort">The registry port.</param>
void Registry::Connect(const char* registryIp, int registryPort)
{
	struct sockaddr_in server = { 0 };

	/* Create socket */
	if ((_socket = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET)
	{
		printf("Could not create socket : %d", WSAGetLastError());
		exit(-1);
	}

	/* Assign IP address and port of registry, run on TCP */
	server.sin_addr.s_addr = inet_addr(registryIp);
	server.sin_family = AF_INET;
	server.sin_port = htons(registryPort);

	/* Attempt connection to server */
	if (connect(_socket, (struct sockaddr*)&server, sizeof(server)) < 0)
	{
		printf("Failed to connect to registry.");
		exit(-1);
	}
}

/// <summary>
/// Queries the team.
/// </summary>
/// <param name="message">The message.</param>
/// <param name="responseMessage">The response message.</param>
/// <returns></returns>
bool Registry::QueryTeam(const char* message, std::string& responseMessage)
{
	bool success = true;

	std::string messageStr = message;
	std::vector<std::string> strList = split(messageStr, '|');
	
	std::string queryString = "\vDRC|QUERY-TEAM|" + _configReader.GetValue("teamName") + '|' + _configReader.GetValue("teamId") + '|' + EOS +
		"INF|" + strList.at(2) + '|' + strList.at(3) + '|' + _configReader.GetValue("tagName") + '|' + EOS + EOM + EOS + '\0';
	
	Connect(_configReader.GetValue("host_ip").c_str(),
		atoi(_configReader.GetValue("host_port").c_str()));
	SendMessageToRegistry(queryString.c_str());
	responseMessage = GetResponse();
	Disconnect();

	if (responseMessage.find("NOT-OK") != std::string::npos)
	{
		success = false;
	}

	return success;
}

/// <summary>
/// Publishes the service.
/// </summary>
/// <param name="configReader">The configuration reader.</param>
/// <returns></returns>
bool Registry::PublishService(ConfigReader& configReader)
{
	bool success = false;
	_configReader = configReader;

	std::string teamName = configReader.GetValue("teamName");
	std::string teamId = configReader.GetValue("teamId");
	std::string tagName = configReader.GetValue("tagName");
	std::string serviceName = configReader.GetValue("serviceName");
	std::string securityLevel = configReader.GetValue("securityLevel");
	std::string description = configReader.GetValue("description");
	std::string clientIp = configReader.GetValue("client_ip");
	std::string clientPort = configReader.GetValue("client_port");

	std::string publishMessageStr = "\vDRC|PUB-SERVICE|" + teamName + "|" + teamId + "|" + EOS +
		"SRV|" + tagName + "|" + serviceName + "|" + securityLevel + "|2|2|" + description + "|" + EOS +
		"ARG|1|ProvinceCode|string|mandatory||" + EOS +
		"ARG|2|PostalCode|string|mandatory||" + EOS +
		"RSP|1|PostalCodeValid|string||" + EOS +
		"RSP|2|SpecialNotes|string||" + EOS +
		"MCH|" + clientIp + "|" + clientPort + "|" + EOS + EOM + EOS + '\0';

	SendMessageToRegistry(publishMessageStr.c_str());

	std::string response = GetResponse();
	if (response.find("has already published service") != std::string::npos)
	{
		success = true;
	}
	else if (response.find("SOA|OK|") != std::string::npos)
	{
		success = true;
	}

	return success;
}

/// <summary>
/// Sends the message to registry.
/// </summary>
/// <param name="message">The message.</param>
/// <returns></returns>
bool Registry::SendMessageToRegistry(const char* message)
{
	bool success = true;

	if (send(_socket, message, (int)strlen(message), 0) < 0)
	{
		printf("Failed to send message to registry.");
		success = false;
	}

	return success;
}

/// <summary>
/// Gets the response.
/// </summary>
/// <returns></returns>
std::string Registry::GetResponse()
{
	int responseSize = 0;
	char* responseMessage = (char*)calloc(RESPONSE_BUFFER_SIZE, sizeof(char));

	/* Get the response from the server */
	if ((responseSize = recv(_socket, responseMessage, RESPONSE_BUFFER_SIZE, 0)) == SOCKET_ERROR)
	{
		printf("Failed to receive message from registry.");
	}

	std::string responseStr = std::string(responseMessage);
	free(responseMessage);
	
	return responseStr;
}

/// <summary>
/// Disconnects this instance.
/// </summary>
void Registry::Disconnect()
{
	closesocket(_socket);
}

/// <summary>
/// Finalizes an instance of the <see cref="Registry"/> class.
/// </summary>
Registry::~Registry()
{
	closesocket(_socket);
	WSACleanup();
}