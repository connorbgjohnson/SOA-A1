#pragma once
#pragma warning(disable:4996)

#include <String>
#include <ctime>
#include <iostream>
#include <chrono>

#define DEFAULT_LOG_PATH "log.txt"

/// <summary>
/// Used to read/write to a log file.
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

