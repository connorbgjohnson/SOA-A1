///Project: SOA-A1-Postal-Code_Validator
///File: ConfigReader.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Methods for reading from a config file.


#include "ConfigReader.h"


/// <summary>
/// Starts a new intance of the config reader.
/// </summary>
ConfigReader::ConfigReader(const char* filePath)
{
	_arraySize = 0;
	LoadConfigFile(filePath, &_arraySize);
}

ConfigReader::ConfigReader()
{
}

/// <summary>
/// Finalizes an instance of the <see cref="ConfigReader"/> class.
/// </summary>
ConfigReader::~ConfigReader()
{
}

//
// Name: getConfigValues
// Parameters: const char* filePath -> Path to the config file
// Returns: KeyValuePair* -> Array of key/value pairs
//                        -> The count of key/value retreived from the file.
// Description:
// Reads a config file delimeted with key value/pairs delimted by
// '=' and with each pair seperated by a newline character.
//
void ConfigReader::LoadConfigFile(const char* filePath, int* keyValueCount)
{
	FILE* pFile = fopen(filePath, "r");
	if (pFile == NULL)
	{
		printf("Failed to open configuration file.");
		exit(-1);
	}

	/* Get count of equal count to get key/value pair amount */
	int equalCount = GetCharacterCount(pFile, '=');
	_configValues = (KeyValuePair*)calloc(equalCount, sizeof(KeyValuePair));

	/* Get size of the file  */
	fseek(pFile, 0, SEEK_END);
	int fileSize = ftell(pFile);
	*keyValueCount = fileSize;
	fseek(pFile, 0, SEEK_SET);

	/* Read in all file contents */
	char* fileText = (char*)calloc(fileSize + 1, sizeof(char*));
	fread(fileText, 1, fileSize, pFile);
	fclose(pFile);

	/* Get initial key/value pair values */
	char* key = strtok(fileText, "=");
	char* value = strtok(NULL, "\n");
	_configValues[0].key = (char*)calloc(strlen(key + 1), sizeof(char));
	_configValues[0].value = (char*)calloc(strlen(value + 1), sizeof(char));
	strcpy(_configValues[0].key, key);
	strcpy(_configValues[0].value, value);

	/* Get remaining key/value pair values */
	for (int i = 1; i != equalCount; ++i)
	{
		key = strtok(NULL, "=");
		value = strtok(NULL, "\n");
		_configValues[i].key = (char*)calloc(strlen(key + 1), sizeof(char));
		_configValues[i].value = (char*)calloc(strlen(value + 1), sizeof(char));
		strcpy(_configValues[i].key, key);
		strcpy(_configValues[i].value, value);
	}

	fclose(pFile);
	free(fileText);
}

//
// Name: getCharacterCount
// Parameters: File* pFile       -> The file to count character in
// Returns: const char character -> The character to count
// Description:
// Gets the count of the occurance of a character in an open txt file.
//
int ConfigReader::GetCharacterCount(FILE* pFile, const char character)
{
	int count = 0;
	rewind(pFile);

	/* Find each character in the file */
	while (!feof(pFile))
	{
		int fileChar = getc(pFile);
		if (fileChar == character)
		{
			++count;
		}
	}

	rewind(pFile);
	return count;
}

//
// Name: getConfigValue
// Parameters: const KeyValuePair* keyValuePairs -> Key/Value pairs from the config file.
//			   const int arraySize			     -> Size of the key/value pair array
//			   const char* key					 -> Key to lookup and get value for.
// Returns: char* -> The value from the key
// Description:
// Gets the value for the specified key from the KeyValuePair array.
//
std::string ConfigReader::GetValue(const char* key)
{
	std::string value = "";

	for (int i = 0; i != _arraySize; ++i)
	{
		if (strcmp((const char*)_configValues[i].key, key) == 0)
		{
			value = _configValues[i].value;
			break;
		}
	}

	return value;
}

