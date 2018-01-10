///Project: SOA-A1-Postal-Code_Validator
///File: LogFile.cpp
///Date: 2018/01/04
///Author: Kyle Kreutzer
///Holds class definition for the log file.

#pragma once
#pragma warning(disable:4996)

#include <String>
#include <ctime>
#include <iostream>
#include <chrono>

#define DEFAULT_LOG_PATH "log.txt"

/// <summary>
/// Static class used for writing string content to a log file.
/// </summary>
class LogFile
{
private:
	LogFile();
public:
	static void Log(const std::string& message, const char* path);
	static void Log(const char* message, const char* path);
	~LogFile();
};

