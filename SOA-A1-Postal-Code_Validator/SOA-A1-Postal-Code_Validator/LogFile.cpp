///Project: SOA-A1-Postal-Code_Validator
///File: LogFile.cpp
///Date: 2018/01/04
///Author: Kyle Kreutzer
///Static class methods for writing to a log file.

#include "LogFile.h"
#include "split.h"

/// <summary>
/// Hidden constructor.
/// </summary>
LogFile::LogFile()
{
}

/// <summary>
/// Logs the specifed string to a log files with each message being seperated
/// by "------- and a new line". If the specified log file doesn't exist, it will
/// be created.
/// </summary>
/// <param name="message">The message to log.</param>
/// <param name="path">Path of the file to log to.</param>
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

/// <summary>
/// Logs the specifed string to a log files with each message being seperated
/// by "------- and a new line". If the specified log file doesn't exist, it will
/// be created.
/// </summary>
/// <param name="message">The message to log.</param>
/// <param name="path">Path of the file to log to.</param>
void LogFile::Log(const char * message, const char* path)
{
	std::string messageStr = message;
	Log(messageStr, path);
}

/// <summary>
/// Destructs this instance.
/// </summary>
LogFile::~LogFile()
{
}
