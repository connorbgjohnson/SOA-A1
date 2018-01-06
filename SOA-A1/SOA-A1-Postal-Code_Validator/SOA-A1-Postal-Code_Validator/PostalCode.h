#include <string>

#pragma once

using namespace std;

namespace PostalCode
{
	string formatToUpper(string province);
	string formatPostalCode(string code);
	bool validateProvince(string province);
	bool validatePostalCodeFormat(string code);
	bool validatePostalCode(string province, string code);
	string getSpecialMessage(string province, string code);
}