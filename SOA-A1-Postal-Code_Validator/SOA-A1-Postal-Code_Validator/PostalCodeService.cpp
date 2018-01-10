///Project: SOA-A1-Postal-Code_Validator
///File: PostalCodeService.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Contains methods for dealing with the postal service.


#include "PostalCodeService.h"


/// <summary>
/// Creates a new instance of the PostalCodeService class.
/// </summary>
PostalCodeService::PostalCodeService(const char* serviceIp, const char* port, Registry& registry)
{
	_port = port;
	_ipAddress = serviceIp;
	_listenSocket = INVALID_SOCKET;
	_registry = registry;
	
}

/// <summary>
/// Builds the socket so it is ready to accept connections, but don't
/// accept connections until Start() is called.
/// </summary>
void PostalCodeService::BuildSocket()
{
	struct addrinfo *result = NULL;
	struct addrinfo *ptr = NULL;
	struct addrinfo hints;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	// Resolve the local address and port to be used by the server
	int iResult = getaddrinfo(NULL, _port.c_str(), &hints, &result);
	if (iResult != 0)
	{
		LogFile::Log("Failed to resolve local address and port. Error Code:"
			+ WSAGetLastError(), DEFAULT_LOG_PATH);
		WSACleanup();
		exit(-1);
	}

	_listenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
	if (_listenSocket == INVALID_SOCKET)
	{
		LogFile::Log("Error assigning socket. Error Code: " +
			WSAGetLastError(), DEFAULT_LOG_PATH);
		freeaddrinfo(result);
		WSACleanup();
		exit(-1);
	}

	iResult = bind(_listenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR) {
		LogFile::Log("Failed to bind to socket. Error Code: "
			+ WSAGetLastError(), DEFAULT_LOG_PATH);
		freeaddrinfo(result);
		closesocket(_listenSocket);
		WSACleanup();
		exit(-1);
	}

	freeaddrinfo(result);

	if (listen(_listenSocket, SOMAXCONN) == SOCKET_ERROR)
	{
		LogFile::Log("Failed to listen on socket. Error Code: "
			+ WSAGetLastError(), DEFAULT_LOG_PATH);
		closesocket(_listenSocket);
		WSACleanup();
		exit(-1);
	}
}

/// <summary>
/// Cancels the on key press.
/// </summary>
void PostalCodeService::CancelOnKeyPress()
{
	getchar();
	closesocket(_listenSocket);
	exit(0);
	
}

/// <summary>
/// Starts the service litening on the socket.
/// </summary>
void PostalCodeService::Start()
{
	char receivedMessge[3000] = "";
	std::string responseMessage = "";
	std::thread t(&PostalCodeService::CancelOnKeyPress, this);

	while (true)
	{
		SOCKET clientSocket = accept(_listenSocket, NULL, NULL);
		if (clientSocket == INVALID_SOCKET)
		{
			LogFile::Log("Accept Failed. Error Code:" +
				WSAGetLastError(), DEFAULT_LOG_PATH);
			closesocket(_listenSocket);
			WSACleanup();
			exit(-1);
		}

		recv(clientSocket, receivedMessge, 3000, 0);
		LogFile::Log("Receiving Service Request:\n" + std::string(receivedMessge), DEFAULT_LOG_PATH);

		bool querySuccess = _registry.QueryTeam(receivedMessge, responseMessage);
		if (!querySuccess)
		{
			std::vector<std::string> errStrings = split(responseMessage, '|');
			std::string errString = "\vPUB|NOT-OK|" + errString.at(2) + '|' + errString.at(3) + '|' + '|' + EOS + EOM + EOS;
			send(clientSocket, errString.c_str(), (int)errString.length(), 0);
			LogFile::Log("Responding to Service Request:\n" + errString, DEFAULT_LOG_PATH);
		}
		else
		{
			std::vector<std::string> successStrings = split(receivedMessge, '|');
			std::string specialNote = "";
			bool validPc = PostalCode::validate(successStrings.at(16), successStrings.at(22), specialNote);
			std::string validStr = (validPc) ? ("VALID|\r") : ("NOT-VALID|\r");

			std::string goodResponseStr = "\vPUB|OK|||2|\rRSP|1|PostalCodeValid|STRING|" + validStr +
			"RSP|2|SpecialNotes|STRING|" + specialNote + '|' + EOS + EOM + EOS + '\0';

			send(clientSocket, goodResponseStr.c_str(), (int)goodResponseStr.length(), 0);
			LogFile::Log("Responding to Service Request:\n" + goodResponseStr, DEFAULT_LOG_PATH);
		}


		closesocket(clientSocket);
	}
	closesocket(_listenSocket);
}

/// <summary>
/// Finalizes an instance of the <see cref="PostalCodeService"/> class.
/// </summary>
PostalCodeService::~PostalCodeService()
{
}
