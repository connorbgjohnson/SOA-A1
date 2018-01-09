///Project: SOA-A1-Postal-Code_Validator
///File: PostalCode.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Holds the PostalCode class.

#pragma once
#pragma warning(disable:4996)

#include <stdio.h>
#include <stdlib.h>
#include <string>

typedef struct KeyValuePair
{
	char* key;
	char* value;
}KeyValuePair;

/// <summary>
/// Used for reading from a config file.
/// </summary>
class ConfigReader
{
private:
	int _arraySize;
	KeyValuePair* _configValues;

	int GetCharacterCount(FILE* file, const char character);
	void LoadConfigFile(const char* filePath, int* keyValueCount);

public:
	std::string GetValue(const char* key);

	ConfigReader::ConfigReader(const char* filePath);
	ConfigReader();
	~ConfigReader();

};

