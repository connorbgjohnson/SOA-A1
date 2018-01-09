///Project: SOA-A1-Postal-Code_Validator
///File: Registry.h
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Contains the class definition for Registry.


#pragma once

#include <string>
#include "ConfigReader.h"

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#define BOM (char)11
#define EOS (char)13
#define EOM (char)28
#define RESPONSE_BUFFER_SIZE 2000

#include <stdio.h>
#include <winsock2.h>
#include "ConfigReader.h"

#pragma comment(lib,"ws2_32.lib") //Winsock Library
#pragma warning(disable:4996)

/// <summary>
/// Controls access to the registry, allowing this service to
/// register itself.
/// </summary>
class Registry
{
private:
	SOCKET _socket;
	ConfigReader _configReader;
	bool Registry::SendMessageToRegistry(const char* message);
    std::string GetResponse();
	

public:
	Registry();
	~Registry();

	void Registry::Disconnect();
	void Connect(const char* registryIp, int registryPort);
	bool PublishService(ConfigReader& configReader);
	bool Registry::QueryTeam(const char* message, std::string& responseMessage);
};

