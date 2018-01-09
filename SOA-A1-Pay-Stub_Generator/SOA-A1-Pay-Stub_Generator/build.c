/*
	* Filename: build.c
	*
	* Description:
	* Builds
	* 
	* 
*/

#include "registry.h"
#include <stdio.h>
#include "paystub.h"
#include "configFileIo.h"

/*
* Function Name: BuildSrvTag(char**, const char* , const char*, const char* argDataTypem const char* mandatory)
*
*
* Description:
* Builds a request from a certain amount of things.

* Parameters:
* char** out: The out buffer
* char* argPosition: The position
* char* argName: The name
* char* dataType: The datatype

* Result:
* void

*/
char* BuildSrvTag(char** out, const char* tagName, const char* serviceName, const char* secLevel, const char* numArgs, const char* numResp, const char* desc)
{
	size_t count = 0;
	count += strlen(tagName) + 1;
	count += strlen(serviceName) + 1;
	count += strlen(secLevel) + 1;
	count += strlen(numArgs) + 1;
	count += strlen(numResp) + 1;
	count += strlen(desc) + 1;
	count += 2;

	*out = (char*)calloc(1, count);

	strcat(*out, "SRV");
	strcat(*out, "|");
	strcat(*out, tagName);
	strcat(*out, "|");
	strcat(*out, serviceName);
	strcat(*out, "|");
	strcat(*out, secLevel);
	strcat(*out, "|");
	strcat(*out, numArgs);
	strcat(*out, "|");
	strcat(*out, numResp);
	strcat(*out, "|");
	strcat(*out, desc);
	out[count - 1] = EOS;
}

/*
* Function Name: BuildArgs(char**, char* , char*, char* argDataTypem char* mandatory)
*
*
* Description:
* Builds a request from a certain amount of things.

* Parameters:
* char** out: The out buffer
* char* argPosition: The position
* char* argName: The name
* char* dataType: The datatype

* Result:
* void

*/
void BuildTeamTag(char** out, char* teamName, char* teamId)
{
	size_t count = 0;
	count += strlen(teamName) + 1;
	count += strlen(teamId) + 1;

	*out = calloc(1, (count * sizeof(char)));

	strcat(*out, teamName);
	strcat(*out, "|");
	strcat(*out, teamId);
	strcat(*out, "|");
}


/*
* Function Name: BuildArgs(char**, char* , char*, char* argDataTypem char* mandatory)
*
*
* Description:
* Builds a request from a certain amount of things.

* Parameters:
* char** out: The out buffer
* char* argPosition: The position
* char* argName: The name
* char* dataType: The datatype

* Result:
* void

*/

void BuildArgs(char** out, char* argPosition, char* argName, char* argDataType, char* mandatory)
{
	size_t count = 0;
	count += strlen(argName) + 1;
	count += strlen(argDataType) + 1;
	count += strlen(argPosition) + 1;
	count += strlen(mandatory) + 1;
	count += 2;

	*out = (char*)calloc(1, (count * sizeof(char)));
	strcat(*out, "ARG");
	strcat(*out, "|");
	strcat(*out, argPosition);
	strcat(*out, "|");
	strcat(*out, argName);
	strcat(*out, "|");
	strcat(*out, argDataType);
	strcat(*out, "|");
	strcat(*out, mandatory);
	out[count - 1] = EOS;
}

/*
* Function Name: BuildResponseTag(char**, char* , char*)
*
*
* Description:
* Builds a request from a certain amount of things.

* Parameters:
* char** out: The out buffer
* char* ip: The position
* char* port: The name
* char* dataType: The datatype

* Result:
* void

*/
void BuildResponseTag(char** out, char* position, char* name, char* dataType)
{
	size_t count = 0;
	count += strlen(position) + 1;
	count += strlen(name) + 1;
	count += strlen(dataType) + 1;
	count += 2;

	*out = malloc(count * sizeof(char));
	strcat(*out, "RESP");
	strcat(*out, "|");
	strcat(*out, position);
	strcat(*out, "|");
	strcat(*out, name);
	strcat(*out, "|");
	strcat(*out, dataType);

}
/*
* Function Name: BuildIpTag(char**, char* , char*)
*
*
* Description:
* Builds a request from a certain amount of things.

* Parameters:
* char** out: The out buffer
* char* ip: The ip 
* char* port: THe port

* Result:
* void
*/
void BuildIpTag(char** out, char* ip, char* port)
{
	size_t count = 0;
	count += strlen(ip) + 1;
	count += strlen(port) + 1;
	*out = calloc(1, (count * sizeof(char)));

	strcat(*out, "MCH");
	strcat(*out, "|");
	strcat(*out, ip);
	strcat(*out, "|");
	strcat(*out, port);
	strcat(*out, "|");
}

/* 
	* Function Name: Build(char**, char* , char*, char*, char*, char*, char*, char*, char*)
	* 
	*
	* Description:
	* Builds a request from a certain amount of things.

	* Parameters:
	* char** out: The buffer to put the string into.
	* char* teamName: The teamname
	* char*  tagName: The tagname
	* char* teamId: The team id.
	* char* tagName: The tag name
	* char* serviceName: The service name
	* char* numArgs: The number of args.
	* char* numResponse: The number response
	* char* description: The description
	* char* ip: The ip 
	* char* port: The port.

	* Result:
	* void
*/
void BuildRequest(char** out, char* teamName, char* teamId, char* tagName, char* serviceName, char* numArgs, char* numResponses, char* description, char* ip, char* port)
{
	char* srvTag = NULL;
	char* teamTag = NULL;

	char* arg1 = NULL;
	char* arg2 = NULL;
	char* arg3 = NULL;
	char* arg4 = NULL;
	char* arg5 = NULL;
	char* responseTag = NULL;
	char* ipTag = NULL;

	const char* BEG_SEG = "\vDRC|PUB-SERVICE|";
	const char* END_SEG = "|||\n\x1c\n";

	BuildTeamTag(&teamTag, teamName, teamId);
	BuildSrvTag(&srvTag, tagName, serviceName, "1", numArgs, numResponses, description);
	BuildArgs(&arg1, "1", "employeeType", "string", "mandatory");
	BuildArgs(&arg2, "2", "totalHours", "int", "mandatory");
	BuildArgs(&arg3, "3", "rateOfPay", "float", "mandatory");
	BuildArgs(&arg4, "4", "seasonalPiecePay", "int", "optional");
	BuildArgs(&arg5, "5", "contractWeeks", "int", "mandatory");
	BuildResponseTag(&responseTag, "1", "Total Pay Value", "float");
	BuildIpTag(&ipTag, ip, port);

	size_t count = 0;
	count += strlen(BEG_SEG) + 1;
	count += strlen(END_SEG) + 1;
	count += strlen(teamTag) + 1;
	count += strlen(srvTag) + 1;
	count += strlen(arg1) + 1;
	count += strlen(arg2) + 1;
	count += strlen(arg3) + 1;
	count += strlen(arg4) + 1;
	count += strlen(arg5) + 1;
	count += strlen(responseTag) + 1;
	count += 500;

	*out = malloc(count * sizeof(char));
	memset(*out, 0, count * sizeof(char));
}
