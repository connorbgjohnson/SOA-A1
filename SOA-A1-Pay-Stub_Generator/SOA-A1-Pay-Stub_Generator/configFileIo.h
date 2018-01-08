#ifndef __CONFIG_FILE_IO_H__
#define __CONFIG_FILE_IO_H__

#include <string.h>
#include <stdio.h>
#include <stdlib.h>

typedef struct KeyValuePair
{
	 char* key;
     char* value;
}KeyValuePair;

int getCharacterCount(FILE* file, const char character);
const char* getConfigValue(const KeyValuePair* keyValuePairs, const int arraySize, const char* key);
KeyValuePair* loadConfigFile(const char* filePath, int* keyValueCount);

#endif
