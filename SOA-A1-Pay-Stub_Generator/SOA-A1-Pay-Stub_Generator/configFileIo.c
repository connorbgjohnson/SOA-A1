//
// Name: configFileIo.c
// Date: 1/8/2018
// Programmers: Kyle Kreutzer, Lauchlin Morrison, Colin Mills, Kyle Kreutzer
// Description:
// Holds methods for reading from a key/value pair configuration file.
//

#include "configFileIo.h"

#pragma warning (disable:4996)

//
// Name: getConfigValues
// Parameters: const char* filePath -> Path to the config file
// Returns: KeyValuePair* -> Array of key/value pairs
//                        -> The count of key/value retreived from the file.
// Description:
// Reads a config file delimeted with key value/pairs delimted by
// '=' and with each pair seperated by a newline character.
//
KeyValuePair* loadConfigFile(const char* filePath, int* keyValueCount)
{
	FILE* pFile = fopen(filePath, "r");
	if (pFile == NULL)
	{
		printf("Failed to open configuration file.");
		exit(-1);
	}

	/* Get count of equal count to get key/value pair amount */
	int equalCount = getCharacterCount(pFile, '=');
	KeyValuePair* pConfigValues = (KeyValuePair*)calloc(equalCount, sizeof(KeyValuePair));

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
	pConfigValues[0].key = (char*)calloc(strlen(key + 1), sizeof(char));
	pConfigValues[0].value = (char*)calloc(strlen(value + 1), sizeof(char));
	strcpy(pConfigValues[0].key, key);
	strcpy(pConfigValues[0].value, value);

	/* Get remaining key/value pair values */
	for (int i = 1; i != equalCount; ++i)
	{
		key = strtok(NULL, "=");
		value = strtok(NULL, "\n");
		pConfigValues[i].key = (char*)calloc(strlen(key + 1), sizeof(char));
		pConfigValues[i].value = (char*)calloc(strlen(value + 1), sizeof(char));
		strcpy(pConfigValues[i].key, key);
		strcpy(pConfigValues[i].value, value);
	}

	fclose(pFile);
	free(fileText);

	return pConfigValues;
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
const char* getConfigValue(const KeyValuePair* keyValuePairs, const int arraySize, const char* key)
{
	const char* value = NULL;

	for (int i = 0; i != arraySize; ++i)
	{
		if (strcmp((const char*)keyValuePairs[i].key, key) == 0)
		{
			value = (const char*)keyValuePairs[i].value;
			break;
		}
	}

	return value;
}

//
// Name: getCharacterCount
// Parameters: File* pFile       -> The file to count character in
// Returns: const char character -> The character to count
// Description:
// Gets the count of the occurance of a character in an open txt file.
//
int getCharacterCount(FILE* pFile, const char character)
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

