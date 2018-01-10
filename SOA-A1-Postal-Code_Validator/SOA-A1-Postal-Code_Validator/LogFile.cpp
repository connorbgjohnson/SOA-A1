#include "LogFile.h"
#include "split.h"


LogFile::LogFile()
{
}

void LogFile::Log(const std::string& message, const char* path)
{
	std::vector<std::string> splitStr = split(message, '\n');

	FILE* pFile = fopen(path, "a");
	fprintf(pFile, "%s", "-------\n");

	/* Get current timestamp and remove \n from back of string */
	time_t time = std::chrono::system_clock::to_time_t(std::chrono::system_clock::now());
	std::string timeStamp = std::ctime(&time);
	timeStamp.pop_back();

	/* Print out every line that was seperated by a \n */
	for (auto it = splitStr.begin(); it != splitStr.end(); ++it)
	{
		fprintf(pFile, "%s %s\n", timeStamp.c_str(), ((std::string)*it).c_str());
	}

	fprintf(pFile, "%s", "-------\n");
	fclose(pFile);
}

void LogFile::Log(const char * message, const char* path)
{
	std::string messageStr = message;
	Log(messageStr, path);
}

LogFile::~LogFile()
{
}
