///Project: SOA-A1-Postal-Code_Validator
///File: PostalCodeService.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Holds the postal code service class.

#pragma once
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#define BOM (char)11
#define EOS (char)13
#define EOM (char)28
#define RESPONSE_BUFFER_SIZE 2000

#include <stdio.h>
#include <ws2tcpip.h>
#include <string>
#include <thread>
#include <winsock2.h>
#include "Registry.h"

#pragma comment(lib,"ws2_32.lib") //Winsock Library
#pragma warning(disable:4996)

/// <summary>
/// Used to read/write to cliens accessing the postal code service.
/// </summary>
class PostalCodeService
{
private:
	std::string _port;
	std::string _ipAddress;
	SOCKET _listenSocket;
	Registry _registry;

public:
	PostalCodeService(const char* serviceIp, const char* port, Registry& registry);
	void CancelOnKeyPress();
	void BuildSocket();
	void Start();


	~PostalCodeService();

};

