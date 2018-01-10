///Project: SOA-A1-Postal-Code_Validator
///File: main.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Application runs as a service to be queried for canadian postal code validation.

#include "PostalCode.h"
#include <stdio.h>
#include "ConfigReader.h"
#include "Registry.h"
#include "PostalCodeService.h"
#include "LogFile.h"

using namespace std;
using namespace PostalCode;

int main(int, char*[])
{
	/* Initialize winsock */
	WSADATA wsa = { 0 };
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		printf("Failed. Error Code : %d", WSAGetLastError());
		exit(-1);
	}

	LogFile::Log("This\nis\na\ntest", DEFAULT_LOG_PATH);

	/* Get configuration options */
	ConfigReader cfgReader = ConfigReader("config.cfg");

	/* Connect to registry */
	Registry registry = Registry();
	registry.Connect(cfgReader.GetValue("host_ip").c_str(),
		atoi(cfgReader.GetValue("host_port").c_str()));

	/* Publish this service */
	bool published = registry.PublishService(cfgReader);
	registry.Disconnect();

	if (published)
	{
		/* Setup service and start */
		PostalCodeService service = PostalCodeService(cfgReader.GetValue("client_ip").c_str(),
			cfgReader.GetValue("client_port").c_str(), registry);
		service.BuildSocket();
		service.Start();
	}
	else
	{
		printf("Failed to publish with registry.");
	}

	return 0;
}